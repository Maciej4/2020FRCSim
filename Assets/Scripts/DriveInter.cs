using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveInter : MonoBehaviour
{
    public ZMQClient zmqClient;
    public DrivebaseController drivebaseController;

    public bool simpleDriveEnabled = true;
    private bool motorsCached = false;

    private List<Motor> motors = new List<Motor>();

    void FixedUpdate()
    {
        if (zmqClient.isComms())
        {
            if (!motorsCached)
            {
                motors.Clear();
                List<Hardware> hardware = zmqClient.zmqThread.unityPacket.hardware;
                for (int i = 0; i < 4; i++)
                {
                    motors.Add((Motor)hardware[i]);
                }
                motorsCached = true;
            }

            //Debug.Log("0: " + motors[0].GetPower() + ", 1: " + motors[1].GetPower() + ", 2: " + motors[2].GetPower() + ", 3: " + motors[3].GetPower());

            float leftPower = (float)(motors[0].GetPower() + motors[1].GetPower()) / 2.0f;
            float rightPower = (float)(motors[2].GetPower() + motors[3].GetPower()) / 2.0f;

            //Debug.Log("l: " + leftPower + ", r: " + rightPower);

            drivebaseController.TankDrive(leftPower, rightPower);

            motors[0].SetEncoderPos(drivebaseController.lx);
            motors[1].SetEncoderPos(drivebaseController.lx);

            motors[2].SetEncoderPos(drivebaseController.rx);
            motors[3].SetEncoderPos(drivebaseController.rx);

            //Debug.Log("Motor powers: " + zmqClient.zmqThread.robotPacket.motorPowers);
        }
        else if (simpleDriveEnabled)
        {
            float linearPower = drivebaseController.maxMotorTorque * Input.GetAxis("Vertical");
            float turnPower = drivebaseController.maxSteeringPower * Input.GetAxis("Horizontal");

            drivebaseController.ArcadeDrive(linearPower, turnPower);
        }
        else
        {
            drivebaseController.TankDrive(0f, 0f);
        }
    }
}
