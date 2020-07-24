using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSolenoid : Hardware
{
    public DoubleSolenoid()
    {
        type = "DoubleSolenoid";
        booleans = new bool[0];
        integers = new int[] { 0, 0, 0 };
        doubles = new double[0];
        strings = new string[0];
    }

    public DoubleSolenoid(int channelA, int channelB)
    {
        type = "DoubleSolenoid";
        booleans = new bool[0];
        integers = new int[] { channelA, channelB, 0 };
        doubles = new double[0];
        strings = new string[0];
    }

    public override void CopyRelValues(Hardware other)
    {
        integers[2] = other.integers[2];
    }

    public int GetSolenoidState()
    {
        return integers[2];
    }
}
