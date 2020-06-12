using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class ZMQClient : MonoBehaviour
{
    public UnityPacket unityPacket;
    public ZMQThread zmqThread;

    private void Start()
    {
        unityPacket = new UnityPacket();
        zmqThread = new ZMQThread();
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
}