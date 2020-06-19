using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PosGoalController : MonoBehaviour
{
    public Text goodJobText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Robot")) 
        {
            goodJobText.text = "Good Job!";
        }
    }
}
