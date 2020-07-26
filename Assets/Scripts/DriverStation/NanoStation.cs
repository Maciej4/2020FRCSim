using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NanoStation : MonoBehaviour
{
    public ZMQClient zmqClient;

    public Transform robotTransform;
    public Text elapsedTime;

    public RobotEnable robotEnable;
    public RobotMode robotMode;

    private Rigidbody robotRigidbody;

    private float offsetTime;
    private float timeNow;

    public bool lockRobotMode = false;
    public bool enableWithoutComms = false;

    private bool robotState = false;
    private bool pastRobotState = false;
    private bool pastZMQIsComms = false;
    private bool stopRobotComms = false;

    // Start is called before the first frame update
    void Start()
    {
        robotRigidbody = robotTransform.GetComponent<Rigidbody>();

        robotRigidbody.velocity = Vector3.zero;
        robotRigidbody.angularVelocity = Vector3.zero;
        robotRigidbody.isKinematic = true;
        robotTransform.position = new Vector3(0.0f, 0.35f, 5.0f);
        robotTransform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
        offsetTime = Time.time;

        robotMode.SetDropdownLockState(lockRobotMode);
    }

    // Update is called once per frame
    void Update()
    {
        robotEnable.IsStateLocked = !zmqClient.isComms() && !enableWithoutComms;

        if (pastZMQIsComms != zmqClient.isComms())
        {
            robotEnable.SetRobotState(false);
        }

        robotState = robotEnable.RobotState;

        if (robotState)
        {
            if (!pastRobotState)
            {
                robotMode.SetDropdownLockState(true);
                robotRigidbody.isKinematic = false;
            }

            SetPacketRobotMode(robotMode.RobotState + 1);
        }
        else
        {
            offsetTime = Time.time;

            if (pastRobotState)
            {
                if (!lockRobotMode)
                {
                    robotMode.SetDropdownLockState(false);
                }
                softReset();
            }

            if (zmqClient.isComms())
            {
                zmqClient.zmqThread.unityPacket.robotMode = 0;
            }

            SetPacketRobotMode(0);
        }
        
        pastRobotState = robotState;
        pastZMQIsComms = zmqClient.isComms();
        
        timeNow = Time.time - offsetTime;

        elapsedTime.text = "T+" + timeNow.ToString("000.0");
    }

    public void softReset()
    {
        robotRigidbody.velocity = Vector3.zero;
        robotRigidbody.angularVelocity = Vector3.zero;
        robotRigidbody.isKinematic = true;
        robotTransform.position = new Vector3(0.0f, 0.35f, 5.0f);
        robotTransform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
        offsetTime = Time.time;
        foreach (GameObject goal in GameObject.FindGameObjectsWithTag("goal2"))
        {
            //if (!(goal.GetComponent<PosGoalController>() is null))
            //{
            ((Goal)goal.GetComponent<MonoBehaviour>()).ResetGoal();
            //}
        }
    }

    public void KillRobotComms()
    {
        stopRobotComms = true;
    }

    private void SetPacketRobotMode(int NewRobotMode)
    {
        if (!zmqClient.isComms())
        {
            stopRobotComms = false;
            return;
        }

        if (stopRobotComms)
        {
            zmqClient.zmqThread.unityPacket.robotMode = -1;
            return;
        }

        zmqClient.zmqThread.unityPacket.robotMode = NewRobotMode;
    }
}
