using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mirror;

public class DrivebaseControllerNet : NetworkBehaviour, Subsystem
{
    //public ZMQClient zmqClient;
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
    //public float leftPower = 0f;
    //public float rightPower = 0f;
    //public float linearPower = 0f;
    //public float turnPower = 0f;
    public double lx = 0.0;
    public double rx = 0.0;
    private float ldx = 0f;
    private float rdx = 0f;
    private bool alterantor = false;

    private List<Hardware> hardware = new List<Hardware> {
        new TalonFX(1),
        new TalonFX(2),
        new TalonFX(3),
        new TalonFX(4)
    };

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

    public void TankDrive(float leftPower, float rightPower)
    {
        ((Motor)hardware[0]).SetPower(leftPower);
        ((Motor)hardware[1]).SetPower(leftPower);
        ((Motor)hardware[2]).SetPower(rightPower);
        ((Motor)hardware[3]).SetPower(rightPower);
    }
    public void ArcadeDrive(float linearPower, float turnPower)
    {
        linearPower = Mathf.Clamp((float)linearPower, -1.0f, 1.0f);
        turnPower = -Mathf.Clamp((float)turnPower, -1.0f, 1.0f);

        TankDrive(turnPower + linearPower, turnPower - linearPower);
    }

    private void InternalTankDrive(float leftPower, float rightPower)
    {
        leftPower = Mathf.Clamp((float)leftPower, -1.0f, 1.0f);
        rightPower = Mathf.Clamp((float)rightPower, -1.0f, 1.0f);

        float linearPower = (leftPower - rightPower) * 0.5f * maxMotorTorque;
        float turnPower = -(leftPower + rightPower) * 0.5f * maxSteeringPower;

        InternalArcadeDrive(linearPower, turnPower);
    }

    private void InternalArcadeDrive(float linearPower, float turnPower)
    {
        linearPower = maxMotorTorque * Mathf.Clamp((float)linearPower, -1.0f, 1.0f);
        turnPower = maxSteeringPower * Mathf.Clamp((float)turnPower, -1.0f, 1.0f);

        float leftPower;
        float rightPower;

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
    }

    public void FixedUpdate()
    {
        //if (isServer)
        //    return;

        float leftPower = (float)(((Motor)hardware[0]).GetPower() + ((Motor)hardware[1]).GetPower()) / 2.0f;
        float rightPower = (float)(((Motor)hardware[2]).GetPower() + ((Motor)hardware[3]).GetPower()) / 2.0f;

        InternalTankDrive(leftPower, rightPower);

        // Encoder simulation
        // TODO: Only measure forward / backward movement.
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

        ((Motor)hardware[0]).SetEncoderPos(lx);
        ((Motor)hardware[1]).SetEncoderPos(lx);
        ((Motor)hardware[2]).SetEncoderPos(rx);
        ((Motor)hardware[3]).SetEncoderPos(rx);
    }

    public List<Hardware> GetHardware()
    {
        return hardware;
    }
}