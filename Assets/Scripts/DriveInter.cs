using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveInter : MonoBehaviour
{
    private ZMQClient zmqClient;
    public DrivebaseController drivebaseController;
    public DriveCtrl driveCtrl;
    public bool isDriveCtrl;
    public RobotEnable robotEnable;

    public bool simpleDriveEnabled = true;

    private void Start()
    {
        zmqClient = GetComponent<ZMQClient>();
    }

    private void Update()
    {
        robotEnable.YellowMode = !zmqClient.isComms();

        if (zmqClient.isComms())
        {
            return;
        }

        if (simpleDriveEnabled && robotEnable.RobotState)
        {

            if (isDriveCtrl)
            {
                float linearPower = 1f * Input.GetAxis("Vertical");
                float turnPower = 2f * Input.GetAxis("Horizontal");
                driveCtrl.ArcadeDrive(linearPower, turnPower);
                return;
            }
            else
            {
                float linearPower = drivebaseController.maxMotorTorque * Input.GetAxis("Vertical");
                float turnPower = 0.5f * drivebaseController.maxSteeringPower * Input.GetAxis("Horizontal");

                drivebaseController.ArcadeDrive(linearPower, turnPower);
            }
        }
        else
        {
            if (isDriveCtrl)
            {
                driveCtrl.TankDrive(0f, 0f);
                return;
            }
            drivebaseController.TankDrive(0f, 0f);
        }
    }
}
