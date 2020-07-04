using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : MonoBehaviour
{
    public int pointValue = 0;
    public ScoreCounter mainScoreboard;
    public bool isRed = false;

   
    private void OnTriggerEnter(Collider other)
    {
        if (isRed)
        {
            mainScoreboard.addToScoreRed(pointValue);
        }
        else
        {
            mainScoreboard.addToScoreBlue(pointValue);
        }
       
        other.GetComponent<BallController>().teleport();
    }
}
