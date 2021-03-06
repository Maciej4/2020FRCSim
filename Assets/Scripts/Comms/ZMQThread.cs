﻿using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ZMQThread : RunAbleThread
{
    public UnityPacket unityPacket;
    public RobotPacket robotPacket;
    public bool killProcess = false;
    public bool attemptReconnect = false;
    public bool connectionStatus = false;
    public double receiveStartTime = 0;
    public double deadTime = 0;
    private readonly double giveUpTime = 1;
    
    public string jsonify()
    {
        unityPacket.hardwareString.Clear();

        foreach (Hardware hardware in unityPacket.hardware)
        {
            unityPacket.hardwareString.Add(JsonUtility.ToJson(hardware));
        }

        return JsonUtility.ToJson(unityPacket);
    }

    public void decodeMessage(string jsonMessage)
    {
        robotPacket = (RobotPacket)JsonUtility.FromJson(jsonMessage, typeof(RobotPacket));

        if (robotPacket.hardware.Count == robotPacket.hardwareString.Count)
        {
            for (int i = 0; i < robotPacket.hardwareString.Count; i++)
            {
                string hardwareJson = robotPacket.hardwareString[i];
                TempHardwareBox tempHardwareBox = (TempHardwareBox)JsonUtility.FromJson(hardwareJson, typeof(TempHardwareBox));
                robotPacket.hardware[i].CopyValues(tempHardwareBox);
            }
        }
        else
        {
            robotPacket.hardware.Clear();

            for (int i = 0; i < robotPacket.hardwareString.Count; i++)
            {
                robotPacket.hardware.Add(decodeHardware(robotPacket.hardwareString[i]));
            }
        }
    }

    public Hardware decodeHardware(string hardwareJson)
    {
        TempHardwareBox tempHardwareBox = (TempHardwareBox)JsonUtility.FromJson(hardwareJson, typeof(TempHardwareBox));

        Hardware tempHardware = (Hardware)Activator.CreateInstance(Type.GetType(tempHardwareBox.type));

        tempHardware.CopyValues(tempHardwareBox);

        return tempHardware;
    }
    
    protected override void Run()
    {
        while (Running && !killProcess)
        {
            connectionStatus = false;
            attemptReconnect = false;
            ForceDotNet.Force(); // this line is needed to prevent unity freeze after one use, not sure why yet

            using (RequestSocket client = new RequestSocket())
            {
                client.Connect("tcp://localhost:5555");
                //client.Connect("tcp://10.9.72.2:5555"); // for connecting to roborio

                while (Running && !killProcess)
                {
                    client.SendFrame(jsonify());

                    string message = null;
                    bool gotMessage = false;
                    receiveStartTime = unityPacket.heartbeat;

                    while (Running && !killProcess)
                    {
                        gotMessage = client.TryReceiveFrameString(out message); // this returns true if it's successful

                        deadTime = unityPacket.heartbeat - receiveStartTime;

                        // Debug.Log("Time until attemp reconnect: " + deadTime);

                        if (giveUpTime < deadTime)
                        {
                            attemptReconnect = true;
                            break;
                        }

                        if (gotMessage)
                        {
                            break;
                        }
                    }

                    if (attemptReconnect)
                    {
                        break;
                    }

                    if (gotMessage)
                    {
                        //Debug.Log("Received " + message);
                        decodeMessage(message);
                    }

                    connectionStatus = true;
                }
            }

            NetMQConfig.Cleanup();
        }
    }
}