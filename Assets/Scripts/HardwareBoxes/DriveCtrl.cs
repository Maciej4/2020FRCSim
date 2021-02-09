using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DriveCtrl : MonoBehaviour, Subsystem
{
    public ArticulationBody robotRigidbody;

    private readonly float stiffness = 1000;
    private readonly float powerLimit = 0.3f;
    private readonly float maxMotorTorque = 1f;
    private readonly float maxSteeringPower = 1f;

    private bool pastImmovable = true;
    private bool alterantor = false;

    public ArticulationBody[] wheels;

    public void Start()
    {
        //wheels[0].SetJointPositions();
    }

    private List<Hardware> hardware = new List<Hardware> {
        new TalonFX(1),
        new TalonFX(2),
        new TalonFX(3),
        new TalonFX(4)
    };

    public void ArcadeDrive(float linearPower, float turnPower)
    {
        linearPower = Mathf.Clamp((float)linearPower, -1.0f, 1.0f);
        turnPower = Mathf.Clamp((float)turnPower, -1.0f, 1.0f);

        TankDrive(linearPower + turnPower, linearPower - turnPower);
    }

    public void TankDrive(float leftPower, float rightPower)
    {
        ((Motor)hardware[0]).SetPower(leftPower);
        ((Motor)hardware[1]).SetPower(leftPower);
        ((Motor)hardware[2]).SetPower(rightPower);
        ((Motor)hardware[3]).SetPower(rightPower);
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

        DirectInternalTankDrive(leftPower, rightPower);
    }

    private void DirectInternalTankDrive(float leftTorque, float rightTorque)
    {
        leftTorque = Mathf.Clamp(leftTorque, -powerLimit, powerLimit);
        rightTorque = Mathf.Clamp(rightTorque, -powerLimit, powerLimit);

        ArticulationDrive leftDrive = newDrive(leftTorque);
        ArticulationDrive rightDrive = newDrive(rightTorque);

        wheels[0].xDrive = leftDrive;
        wheels[1].xDrive = leftDrive;
        wheels[2].xDrive = leftDrive;
        wheels[3].xDrive = rightDrive;
        wheels[4].xDrive = rightDrive;
        wheels[5].xDrive = rightDrive;
    }

    public ArticulationDrive newDrive(float force)
    {
        ArticulationDrive drive = new ArticulationDrive();
        drive.stiffness = stiffness;
        drive.damping = 0;
        drive.forceLimit = Mathf.Abs(force);
        //drive.targetVelocity = (Mathf.Sign(force) == 1) ? 100f : -100f;
        drive.target = (Mathf.Sign(force) == 1) ? 100000 : -100000;
        drive.targetVelocity = 0;

        return drive;
    }

    public void FixedUpdate()
    {
        float leftPower = (float)(((Motor)hardware[0]).GetPower() + ((Motor)hardware[1]).GetPower()) / 2.0f;
        float rightPower = (float)(((Motor)hardware[2]).GetPower() + ((Motor)hardware[3]).GetPower()) / 2.0f;

        DirectInternalTankDrive(leftPower, rightPower);

        SetEncoders(wheels[1].jointPosition[0], wheels[4].jointPosition[0]);
        Debug.Log(wheels[1].jointPosition[0]);

        if (robotRigidbody.immovable && !pastImmovable)
        {
            ResetWheels();
        }

        pastImmovable = robotRigidbody.immovable;

        alterantor = !alterantor;
    }

    private void ResetWheels()
    {
        Debug.Log(wheels[0].dofCount);
        wheels[0].SetJointPositions(new List<float>() { 0.0f });
        wheels[1].SetJointPositions(new List<float>() { 0.0f });
        wheels[2].SetJointPositions(new List<float>() { 0.0f });
        wheels[3].SetJointPositions(new List<float>() { 0.0f });
        wheels[4].SetJointPositions(new List<float>() { 0.0f });
        wheels[5].SetJointPositions(new List<float>() { 0.0f });
    }

    private void SetEncoders(double leftEncoderValue, double rightEncoderValue)
    {
        ((Motor)hardware[0]).SetEncoderPos(leftEncoderValue);
        ((Motor)hardware[1]).SetEncoderPos(leftEncoderValue);
        ((Motor)hardware[2]).SetEncoderPos(rightEncoderValue);
        ((Motor)hardware[3]).SetEncoderPos(rightEncoderValue);
    }

    public List<Hardware> GetHardware()
    {
        return hardware;
    }
}