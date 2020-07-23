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

    public string GetHardwareType() 
    {
        return type;
    }

    public void CopyValues(Hardware other)
    {
        this.type = other.type;
        this.booleans = other.booleans;
        this.integers = other.integers;
        this.doubles = other.doubles;
        this.strings = other.strings;
    }

    public void CopyValues(TempHardwareBox other)
    {
        this.type = other.type;
        this.booleans = other.booleans;
        this.integers = other.integers;
        this.doubles = other.doubles;
        this.strings = other.strings;
    }

    public abstract void CopyRelValues(Hardware other);
}
