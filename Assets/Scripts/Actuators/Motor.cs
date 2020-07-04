using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Motor
{
    void SetPower(float power);
    float GetPosition();
    string GetType();
}
