using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalonSRX : Hardware, Motor
{
    public TalonSRX(int canID)
    {
        type = "TalonSRX";
        booleans = new bool[0];
        integers = new int[] { canID };
        doubles = new double[] { 0.0, 0.0 };
        strings = new string[0];
    }

    public override void CopyRelValues(Hardware other)
    {
        doubles[0] = other.doubles[0];
    }

    public double GetEncoderPos()
    {
        return doubles[1];
    }

    public int GetMotorID()
    {
        return integers[0];
    }

    public double GetPower()
    {
        return doubles[0];
    }

    public void SetEncoderPos(double encoderPosition)
    {
        doubles[1] = encoderPosition;
    }

    public void SetPower(double motorPower)
    {
        doubles[0] = motorPower;
    }
}
