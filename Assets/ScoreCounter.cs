using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public int score = 0;
    public Text m_MyText;
    private string baseString = "Score: ";
    private string gamePeriod = "Auto";
    private void Start()
    {
        m_MyText = this.transform.GetComponent<Text>();
    }

    private void Update()
    {
        findPeriod();

        m_MyText.text = baseString + score + "\nTime: " + Time.time.ToString("00.##") + "\nGame Period: " + gamePeriod;

        if (Input.GetKeyDown(KeyCode.Y))
        {
            resetArena();
        }
    }
    private void findPeriod()
    {
        if (Time.time <= 15.0)
        {
            gamePeriod = "Auto";
        }
        else if (Time.time <= 135)
        {
            gamePeriod = "Teleop";
        }
        else if (Time.time <= 205)
        {
            gamePeriod = "Endgame";
        }
        else 
        {
            gamePeriod = "Disabled";
        }
    }
    public void addToScore(int value)
    {
        score += value;
    }

    public void resetScore()
    {
        score = 0;
    }

    public void resetArena()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
