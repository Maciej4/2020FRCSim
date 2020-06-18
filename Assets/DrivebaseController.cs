using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrivebaseController : MonoBehaviour
{
    public ZMQClient zmqClient;
    public Rigidbody robotRigidbody;
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public Transform lfw;
    public Transform rfw;
    public Transform lbw;
    public Transform rbw;
    private Vector3 pastLeftPos = new Vector3();
    private Vector3 pastRightPos = new Vector3();
    private Vector3 lfp = new Vector3();
    private Vector3 rfp = new Vector3();
    private Vector3 lbp = new Vector3();
    private Vector3 rbp = new Vector3();
    public float maxMotorTorque = 100f;
    public float maxSteeringPower = 100f;
    public float leftPower = 0f;
    public float rightPower = 0f;
    public float linearPower = 0f;
    public float turnPower = 0f;
    public double lx = 0.0;
    public double rx = 0.0;
    private float ldx = 0f;
    private float rdx = 0f;
    private bool alterantor = false;

    public void Start()
    {
        pastLeftPos = leftWheel.transform.position;
        pastRightPos = rightWheel.transform.position;
    }

    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    public void FixedUpdate()
    {
        if (!(zmqClient.zmqThread is null) && !(zmqClient.zmqThread.robotPacket is null) && zmqClient.zmqThread.connectionStatus)
        {
            float leftPower = (float)(zmqClient.zmqThread.robotPacket.leftDriveMotor1Power + zmqClient.zmqThread.robotPacket.leftDriveMotor2Power) / 2.0f;
            float rightPower = (float)(zmqClient.zmqThread.robotPacket.rightDriveMotor1Power + zmqClient.zmqThread.robotPacket.rightDriveMotor2Power) / 2.0f;

            leftPower = Mathf.Clamp((float)leftPower, -1.0f, 1.0f);
            rightPower = Mathf.Clamp((float)rightPower, -1.0f, 1.0f);

            linearPower = (leftPower - rightPower) * 0.5f * maxMotorTorque;
            turnPower = -(leftPower + rightPower) * 0.5f * maxSteeringPower;

            zmqClient.zmqThread.unityPacket.navXHeading = this.transform.rotation.eulerAngles.y - 180f;

            zmqClient.unityPacket.leftDriveEncoderInterfacePosition = 10.0 * lx;
            zmqClient.unityPacket.leftDriveMotor1Position = lx;
            zmqClient.unityPacket.leftDriveMotor2Position = lx;

            zmqClient.unityPacket.rightDriveEncoderInterfacePosition = 10.0 * rx;
            zmqClient.unityPacket.rightDriveMotor1Position = rx;
            zmqClient.unityPacket.rightDriveMotor2Position = rx;
        }
        else
        {
            linearPower = maxMotorTorque * Input.GetAxis("Vertical");
            turnPower = maxSteeringPower * Input.GetAxis("Horizontal");
        }

        if (alterantor)
        {
            leftPower = linearPower;
            rightPower = linearPower;
        }
        else
        {
            leftPower = turnPower;
            rightPower = -turnPower;
        }

        leftWheel.motorTorque = leftPower;
        rightWheel.motorTorque = rightPower;

        ApplyLocalPositionToVisuals(leftWheel);
        ApplyLocalPositionToVisuals(rightWheel);


        // Encoder simulation

        ldx = Vector3.Distance(leftWheel.transform.position, pastLeftPos);
        rdx = Vector3.Distance(rightWheel.transform.position, pastRightPos);

        if (Vector3.Distance(lbp, leftWheel.transform.position) < Vector3.Distance(lfp, leftWheel.transform.position)) 
        {
            ldx = -ldx;
        }

        if (Vector3.Distance(rbp, rightWheel.transform.position) < Vector3.Distance(rfp, rightWheel.transform.position))
        {
            rdx = -rdx;
        }

        lx += ldx * Time.fixedDeltaTime;
        rx += rdx * Time.fixedDeltaTime;

        lfp = lfw.position;
        lbp = lbw.position;
        rfp = rfw.position;
        rbp = rbw.position;

        pastLeftPos = leftWheel.transform.position;
        pastRightPos = rightWheel.transform.position;

        alterantor = !alterantor;
    }
}