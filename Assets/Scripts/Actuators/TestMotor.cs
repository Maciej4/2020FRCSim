using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMotor : Hardware
{
    public TestMotor()
    { 
        type = "TestMotor";
        booleans = new bool[]{ true, false };
        integers = new int[]{ 1, 2, 3, 4 };
        doubles = new double[]{ 1.0, 2.0, 3.0, 4.0 };
        strings = new string[]{ "A", "B", "C", "D" };
    }

    public override string GetHardwareType()
    {
        return type;
    }
}
