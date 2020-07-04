using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalonFX : Motor
{
    private Func<float, bool> setPowFunc;
    private Func<float> getPosFunc;

    public TalonFX(Func<float, bool> setPowFunc, Func<float> getPosFunc)
    {
        this.setPowFunc = setPowFunc;
        this.getPosFunc = getPosFunc;
    }

    public void SetPower(float power)
    {
        setPowFunc(power);
    }

    public float GetPosition()
    {
        return getPosFunc();
    }

    public new string GetType()
    {
        return "TalonFX";
    }
}
