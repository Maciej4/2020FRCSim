using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hardware
{
    public string type;
    public bool[] booleans;
    public int[] integers;
    public double[] doubles;
    public string[] strings;

    public abstract string GetHardwareType();

    public void CopyValues(Hardware other)
    {
        this.type = other.type;
        this.booleans = other.booleans;
        this.integers = other.integers;
        this.doubles = other.doubles;
        this.strings = other.strings;
    }
}
