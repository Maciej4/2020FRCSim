using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Motor
{
    void SetPower(double motorPower);
    double GetPower();
    void SetEncoderPos(double encoderPosition);
    double GetEncoderPos();
    int GetMotorID();
}
