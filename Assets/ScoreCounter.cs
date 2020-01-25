using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public int redScore = 0;
    public int blueScore = 0;
    public Text m_MyText;
    private string gamePeriod = "Auto";
    private float offsetTime;
    private float timeNow;
    private void Start()
    {
        m_MyText = this.transform.GetComponent<Text>();
        offsetTime = Time.time;
    }

    private void Update()
    {
        timeNow = Time.time - offsetTime;
        findPeriod();

        m_MyText.text = "Red Score:" + redScore + "\nBlue Score:" + blueScore + "\nTime: " + timeNow.ToString("00.##") + "\nGame Period: " + gamePeriod;

        if (Input.GetKeyDown(KeyCode.Y))
        {
            resetArena();
        }
    }
    private void findPeriod()
    {
        if (timeNow <= 15.0)
        {
            gamePeriod = "Auto";
        }
        else if (timeNow <= 135)
        {
            gamePeriod = "Teleop";
        }
        else if (timeNow <= 205)
        {
            gamePeriod = "Endgame";
        }
        else 
        {
            gamePeriod = "Disabled";
            redScore = 0;
        }
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

    public void resetArena()
    {
       
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
       
    }
}
