using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedController : MonoBehaviour
{
    public IntakeController intakeController;

    void OnTriggerEnter(Collider other)
    {
        if (other.name.StartsWith("Ball") && (Input.GetKey(KeyCode.Z) || Input.GetKeyDown("joystick button 0")))
        {
            intakeController.shoot(other.transform);
        }
    }
}
