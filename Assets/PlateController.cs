using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : MonoBehaviour
{
    public int pointValue = 0;
    public ScoreCounter mainScoreboard;

    private void OnTriggerEnter(Collider other)
    {
        mainScoreboard.addToScore(pointValue);
        other.GetComponent<BallController>().teleport();
    }
}
