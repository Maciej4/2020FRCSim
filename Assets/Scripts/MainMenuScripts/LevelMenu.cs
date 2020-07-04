using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public List<TextMeshProUGUI> textLines = new List<TextMeshProUGUI>();

    public Transform red;
    public TextMeshProUGUI clock;

    public float squareYPos = 400f;
    public int cursorPos = 0;
    public string levelDir = "C:\\";
    public string sceneName = "";
    public string nextLevelName = "";

    public bool isHidden = false;

    public List<MenuFile> menuList = new List<MenuFile>()
    {
            new MenuFile("resume.exe", MenuFile.FileAction.NONE),
            new MenuFile("help.url", MenuFile.FileAction.URL).setUrl("https://github.com/iron-claw-972/Curriculum2020"),
            new MenuFile("restart.exe", MenuFile.FileAction.SCENE),
            new MenuFile("main_menu.exe", MenuFile.FileAction.SCENE).setScene("MainMenu"),
            new MenuFile(),
            new MenuFile("C:\\", MenuFile.FileAction.NONE)
    };

    private void Start()
    {
        menuList[menuList.Count - 1].setTitle(levelDir);
        textLines[menuList.Count - 1].text = levelDir;
        menuList[2].setScene(sceneName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            setCursorPos(0);
            toggleMenu();
        }

        if (isHidden)
        {
            this.transform.localPosition = new Vector3(8000f, 0f, 0f);
            return;
        }
        else
        {
            this.transform.localPosition = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            moveCursorDown();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            moveCursorUp();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (cursorPos == 0)
            {
                toggleMenu();
            }
            else
            {
                /**if (!menuList[cursorPos].getSceneToLoad().Equals(SceneManager.GetActiveScene().name))
                {
                    Destroy(GameObject.FindGameObjectsWithTag("test")[0]);
                }*/

                menuList[cursorPos].activate(this);
            }
        }

        clock.text = "<mspace=68>─" + DateTime.Now.ToString("HH:mm") + "─</mspace>";
    }

    public void moveCursorUp()
    {
        if (cursorPos == 0)
        {
            squareYPos = 400f;
            red.localPosition = new Vector3(0f, squareYPos, 0);
            return;
        }

        squareYPos += 100f;
        cursorPos--;

        red.localPosition = new Vector3(0f, squareYPos, 0);
    }

    public void moveCursorDown()
    {
        if (cursorPos == 14)
        {
            return;
        }

        if (menuList[cursorPos + 1].getFileAction() == MenuFile.FileAction.NONE &&
            menuList[cursorPos + 1].getTitle().Equals(""))
        {
            return;
        }

        squareYPos -= 100f;
        cursorPos++;

        red.localPosition = new Vector3(0f, squareYPos, 0);
    }
    
    public void setCursorPos(int cursorPos_)
    {
        this.cursorPos = cursorPos_;
        squareYPos = 400f;
        squareYPos = squareYPos - cursorPos_ * 100f;
        red.localPosition = new Vector3(0f, squareYPos, 0);
    }

    public void toggleMenu()
    {
        isHidden = !isHidden;
    }

    public void loadNextLevel()
    {
        if (isHidden)
        {
            /**if (1 < GameObject.FindGameObjectsWithTag("test").Length)
            {
                Destroy(GameObject.FindGameObjectsWithTag("test")[0]);
            }*/
            SceneManager.LoadScene(nextLevelName, LoadSceneMode.Single);
        }
    }
}
