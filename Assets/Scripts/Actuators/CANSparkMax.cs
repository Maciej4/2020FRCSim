using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CANSparkMax : Motor
{
    public int motorID = 0;
    public double motorPower = 0;
    public double encoderPosition = 0;
    public string motorType = "CANSparkMax";

    public CANSparkMax(int motorID)
    {
        this.motorID = motorID;
    }

    public double GetEncoderPos()
    {
        return encoderPosition;
    }

    public double GetPower()
    {
        return motorPower;
    }

    public void SetEncoderPos(double encoderPosition)
    {
        this.encoderPosition = encoderPosition;
    }

    public void SetPower(double motorPower)
    {
        this.motorPower = motorPower;
    }

    public int GetMotorID()
    {
        return motorID;
    }

    public string GetHardwareType()
    {
        return motorType;
    }

    public void CopyValues(hardware1 hardware)
    {
        if (hardware.GetHardwareType().Equals(this.GetHardwareType()))
        {
            CANSparkMax hardwareCast = (CANSparkMax)hardware;

            this.SetEncoderPos(hardwareCast.GetEncoderPos());
            this.SetPower(hardwareCast.GetPower());

            if (!hardwareCast.GetMotorID().Equals(this.GetMotorID()))
            {
                Debug.LogError("CAN ID Mismatch on: " + this.GetMotorID());
            }
        }
    }
}
