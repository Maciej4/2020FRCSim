﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DriverStation : MonoBehaviour
{
    public ZMQClient zmqClient;

    public Transform robotTransform;
    public Rigidbody robotRigidbody;
    public Dropdown modeDropdown;
    public RawImage modeCover;
    public RawImage arrowTop;
    public RawImage commsStatusImage;
    public RawImage joyStatusImage;
    public RawImage enableArea;
    public Text enableText;
    public Text elapsedTime;
    public FloatingText instructions;
    public DrivebaseController drivebase;

    private Color red = new Color(255, 0, 0);
    private Color green = new Color(0, 255, 0);

    private float offsetTime;
    private float timeNow;

    public int robotMode = 0;

    public bool robotState = false;
    private bool pastRobotState = false;
    public bool modeChangingEnabled = true;
    public bool modeChangingLock = false;

    // Start is called before the first frame update
    void Start()
    {
        softReset();
        offsetTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (zmqClient.isComms())
        {
            commsStatusImage.color = green;
        }
        else
        {
            commsStatusImage.color = red;
        }

        if (Input.GetJoystickNames().Length == 0 || Input.GetJoystickNames()[0].Equals(""))
        {
            joyStatusImage.color = red;
        }
        else
        {
            joyStatusImage.color = green;
        }

        if (Input.GetKeyDown(KeyCode.Space) && (zmqClient.isComms() || drivebase.simpleDriveEnabled))
        {
            robotState = !robotState;
        }

        if (robotState)
        {
            if (!pastRobotState)
            {
                robotRigidbody.isKinematic = false;
            }
            enableArea.color = green;
            enableText.text = "Disable";
            instructions.hideText = true;
            if (zmqClient.isComms())
            {
                zmqClient.zmqThread.unityPacket.robotMode = robotMode + 1;
            }
        }
        else
        {
            offsetTime = Time.time;
            if (pastRobotState)
            {
                softReset();
            }
            enableArea.color = red;
            enableText.text = "Enable";
            instructions.hideText = false;
            if (zmqClient.isComms())
            {
                zmqClient.zmqThread.unityPacket.robotMode = 0;
            }
        }

        if (modeChangingEnabled)
        {
            arrowTop.enabled = true;
            modeCover.enabled = false;
            modeDropdown.interactable = true;
        }
        else
        {
            arrowTop.enabled = false;
            modeCover.enabled = true;
            modeDropdown.interactable = false;
        }

        if (!modeChangingLock) 
        {
            modeChangingEnabled = !robotState;
        }

        pastRobotState = robotState;
        
        timeNow = Time.time - offsetTime;

        elapsedTime.text = "Elapsed Time " + timeNow.ToString("000.0");
    }

    private void softReset()
    {
        robotRigidbody.velocity = Vector3.zero;
        robotRigidbody.angularVelocity = Vector3.zero;
        robotRigidbody.isKinematic = true;
        robotTransform.position = new Vector3(0.0f, 0.35f, 5.0f);
        robotTransform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
    }

    public void toggleState() 
    {
        if (!Input.GetKey(KeyCode.Space) && (zmqClient.isComms() || drivebase.simpleDriveEnabled))
        {
            robotState = !robotState;
        }
    }

    public void modeChanged() 
    {
        robotMode = modeDropdown.value;
    }
}