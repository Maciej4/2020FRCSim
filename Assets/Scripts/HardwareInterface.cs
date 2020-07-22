using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HardwareInterface : MonoBehaviour
{
    public ZMQClient zmqClient;
    public List<MonoBehaviour> subsystems;
    public List<Hardware> hardware = new List<Hardware>();

    void Start()
    {
        ExtractHardware();
    }

    private void FixedUpdate()
    {
        if (zmqClient.isComms())
        {
            if (zmqClient.zmqThread.robotPacket.hardware.Count != hardware.Count)
            {
                zmqClient.zmqThread.robotPacket.hardware.Clear();

                zmqClient.zmqThread.robotPacket.hardware.AddRange(hardware);
            }

            /*for (int i = 0; i < hardware.Count; i++)
            {
                try
                {
                    hardware[i].CopyValues(zmqClient.zmqThread.robotPacket.hardware[i]);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Debug.Log(e);
                    zmqClient.zmqThread.unityPacket.hardware = this.hardware;
                    return;
                }
            }*/

            zmqClient.zmqThread.unityPacket.hardware = this.hardware;

            Debug.Log("Hardware length: " + zmqClient.zmqThread.unityPacket.hardware.Count);
        }
    }

    public void ExtractHardware()
    {
        hardware.Clear();

        foreach (MonoBehaviour subsystem in subsystems)
        {
            hardware.AddRange(((Subsystem)subsystem).GetHardware());
        }
    }
}
