using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOnStart : MonoBehaviour
{
    public DriverStation driverStation;

    private int count = 0;

    private void Update()
    {
        if (count <= 50)
        {
            driverStation.robotState = true;
        }
        else
        {
            count++;
        }
    }
}
