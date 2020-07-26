using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AHRSBox : MonoBehaviour, Subsystem
{
    public Transform robot;

    private List<Hardware> hardware = new List<Hardware> {
        new AHRS()
    };

    void Update()
    {
        ((AHRS)hardware[0]).doubles[0] = robot.rotation.eulerAngles.y - 180f;
    }

    public List<Hardware> GetHardware()
    {
        return hardware;
    }
}
