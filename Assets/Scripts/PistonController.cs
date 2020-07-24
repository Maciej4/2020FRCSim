using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonController : MonoBehaviour, Subsystem
{
    public ConfigurableJoint pistonJoint;

    private List<Hardware> hardware = new List<Hardware> {
        new DoubleSolenoid(0, 1)
    };

    public int pistonState = 0;

    private Vector3 pistonIn = new Vector3(0f, 0.12f, 0f);
    private Vector3 pistonOut = new Vector3(0f, 0.6f, 0f);

    void Update()
    {
        pistonState = ((DoubleSolenoid)hardware[0]).GetSolenoidState();

        if (pistonState == 0 || pistonState == 2)
        {
            pistonJoint.targetPosition = pistonIn;
        }
        else
        {
            pistonJoint.targetPosition = pistonOut;
        }
    }

    public List<Hardware> GetHardware()
    {
        return hardware;
    }
}
