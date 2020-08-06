using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveInterNet : NetworkBehaviour
{
    private ZMQClient zmqClient;
    public DrivebaseControllerNet drivebaseController;
    public RobotEnableNet robotEnable;

    public bool simpleDriveEnabled = true;

    private void Start()
    {
        zmqClient = GetComponent<ZMQClient>();
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;
        //robotEnable.YellowMode = !zmqClient.isComms();

        //if (zmqClient.isComms())
        //{
        //    return;
        //}

        if (simpleDriveEnabled && robotEnable.RobotState)
        {
            float linearPower = drivebaseController.maxMotorTorque * Input.GetAxis("Vertical");
            float turnPower = 0.5f * drivebaseController.maxSteeringPower * Input.GetAxis("Horizontal");

            Debug.Log("Forward Power: " + linearPower);

            drivebaseController.ArcadeDrive(linearPower, turnPower);
        }
        else
        {
            drivebaseController.TankDrive(0f, 0f);
        }
    }
}
