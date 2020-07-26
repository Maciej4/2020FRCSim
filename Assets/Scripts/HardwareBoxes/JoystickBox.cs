using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickBox : MonoBehaviour, Subsystem
{
    private List<Hardware> hardware = new List<Hardware> {
        new Joystick(0)
    };

    void Update()
    {
        ((Joystick)hardware[0]).Loop();
    }

    public List<Hardware> GetHardware()
    {
        return hardware;
    }
}
