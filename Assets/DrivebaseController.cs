using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrivebaseController : MonoBehaviour
{
    public ZMQClient zmqClient;
    public Rigidbody robotRigidbody;
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public WheelCollider lfwc;
    public WheelCollider rfwc;
    public WheelCollider lbwc;
    public WheelCollider rbwc;
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
    public bool simpleDriveEnabled = true;

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
        if (zmqClient.isComms())
        {
            float leftPower = (float)(zmqClient.zmqThread.robotPacket.motorPowers[0] + zmqClient.zmqThread.robotPacket.motorPowers[1]) / 2.0f;
            float rightPower = (float)(zmqClient.zmqThread.robotPacket.motorPowers[2] + zmqClient.zmqThread.robotPacket.motorPowers[3]) / 2.0f;

            leftPower = Mathf.Clamp((float)leftPower, -1.0f, 1.0f);
            rightPower = Mathf.Clamp((float)rightPower, -1.0f, 1.0f);

            linearPower = (leftPower - rightPower) * 0.5f * maxMotorTorque;
            turnPower = -(leftPower + rightPower) * 0.5f * maxSteeringPower;

            zmqClient.zmqThread.unityPacket.navXHeading = this.transform.rotation.eulerAngles.y - 180f;

            zmqClient.unityPacket.motorPositions[12] = 10.0 * lx;
            zmqClient.unityPacket.motorPositions[0] = lx;
            zmqClient.unityPacket.motorPositions[1] = lx;

            zmqClient.unityPacket.motorPositions[13] = 10.0 * rx;
            zmqClient.unityPacket.motorPositions[2] = rx;
            zmqClient.unityPacket.motorPositions[3] = rx;

            //Debug.Log("Motor powers: " + zmqClient.zmqThread.robotPacket.motorPowers);
        }
        else if (simpleDriveEnabled)
        {
            linearPower = maxMotorTorque * Input.GetAxis("Vertical");
            turnPower = maxSteeringPower * Input.GetAxis("Horizontal");
        }
        else
        {
            linearPower = 0;
            turnPower = 0;
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
        ApplyLocalPositionToVisuals(lfwc);
        ApplyLocalPositionToVisuals(rfwc);
        ApplyLocalPositionToVisuals(lbwc);
        ApplyLocalPositionToVisuals(rbwc);


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