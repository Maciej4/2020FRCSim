using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveInter : MonoBehaviour
{
    public ZMQClient zmqClient;
    public DrivebaseController drivebaseController;

    public bool simpleDriveEnabled = true;

    void FixedUpdate()
    {
        if (zmqClient.isComms())
        {
            float leftPower = (float)(zmqClient.zmqThread.robotPacket.motorPowers[0] + zmqClient.zmqThread.robotPacket.motorPowers[1]) / 2.0f;
            float rightPower = (float)(zmqClient.zmqThread.robotPacket.motorPowers[2] + zmqClient.zmqThread.robotPacket.motorPowers[3]) / 2.0f;

            drivebaseController.TankDrive(leftPower, rightPower);

            zmqClient.zmqThread.unityPacket.navXHeading = this.transform.rotation.eulerAngles.y - 180f;

            zmqClient.unityPacket.motorPositions[12] = 10.0 * drivebaseController.lx;
            zmqClient.unityPacket.motorPositions[0] = drivebaseController.lx;
            zmqClient.unityPacket.motorPositions[1] = drivebaseController.lx;

            zmqClient.unityPacket.motorPositions[13] = 10.0 * drivebaseController.rx;
            zmqClient.unityPacket.motorPositions[2] = drivebaseController.rx;
            zmqClient.unityPacket.motorPositions[3] = drivebaseController.rx;

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
