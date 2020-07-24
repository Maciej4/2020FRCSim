using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSolenoid : Hardware
{
    public DoubleSolenoid()
    {
        type = "DoubleSolenoid";
        booleans = new bool[0];
        integers = new int[] { 0 };
        doubles = new double[] { 0.0 };
        strings = new string[0];
    }

    public override void CopyRelValues(Hardware other)
    { 
        return;
    }
}
