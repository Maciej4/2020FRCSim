using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class ZMQClient : MonoBehaviour
{
    public UnityPacket unityPacket = new UnityPacket();
    public ZMQThread zmqThread = new ZMQThread();

    private void Start()
    {
        zmqThread.unityPacket = unityPacket;
        zmqThread.Start();
    }

    private void Update()
    {
        if (zmqThread is null)
        {
            unityPacket = new UnityPacket();
            zmqThread = new ZMQThread();
            zmqThread.unityPacket = unityPacket;
            zmqThread.Start();
        }

        unityPacket.heartbeat = Time.realtimeSinceStartup;

        zmqThread.unityPacket = unityPacket;
    }

    private void OnDestroy()
    {
        zmqThread.killProcess = true;
        zmqThread.Stop();
    }

    public bool isComms()
    {
        return !(zmqThread is null) && !(zmqThread.robotPacket is null) && zmqThread.connectionStatus;
    }
}