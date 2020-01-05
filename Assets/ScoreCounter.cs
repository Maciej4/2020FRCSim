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

    private void Start()
    {
        m_MyText = this.transform.GetComponent<Text>();
    }

    private void Update()
    {
        m_MyText.text = baseString + score + "\nTime: " + Time.time.ToString("00.##");

        if (Input.GetKeyDown(KeyCode.Y))
        {
            resetArena();
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
