using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public Text BlueScoreText;
    public Text RedScoreText;
    public int redScore = 0;
    public int blueScore = 0;

    private void Update()
    {
        BlueScoreText.text = blueScore.ToString("000");
        RedScoreText.text = redScore.ToString("000");
    }

    public void addToScoreRed(int value)
    {
        redScore += value;
    }

    public void addToScoreBlue(int value)
    {
        blueScore += value;
    }

    public void resetScore()
    {
        redScore = 0;
        blueScore = 0;
    }
}
