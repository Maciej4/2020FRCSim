using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public int redScore = 0;
    public int blueScore = 0;
    private Text m_MyText;
    public ZMQClient zmqClient;
    private string gamePeriod = "Auto";
    private string controlledBy = "";
    private float offsetTime;
    private float timeNow;

    private void Start()
    {
        m_MyText = this.transform.GetComponent<Text>();
        offsetTime = Time.time;
    }

    private void Update()
    {
        if (m_MyText is null) 
        { 
            m_MyText = this.transform.GetComponent<Text>();
        }

        timeNow = Time.time - offsetTime;
        findPeriod();
        findControlMode();

        m_MyText.text = "Red Score:" + redScore + "\nBlue Score:" + blueScore + "\nTime: " + timeNow.ToString("00.##") + "\nGame Period: " + gamePeriod + controlledBy;

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

    public void findControlMode()
    {
        if (zmqClient.zmqThread.connectionStatus)
        {
            controlledBy = "\nExternal Java!";
        }
        else
        {
            controlledBy = "";
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
