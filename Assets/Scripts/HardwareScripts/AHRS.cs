using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AHRS : Hardware
{
    public AHRS()
    {
        type = "AHRS";
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
