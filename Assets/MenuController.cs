using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public List<TextMeshProUGUI> textLines = new List<TextMeshProUGUI>();
    public TextMeshProUGUI infoLine;
    public TextMeshProUGUI description;
    public Transform red;
    public float squareYPos = 759f;
    public int cursorPos = 0;

    public List<MenuItem> root = new List<MenuItem>() {
        new MenuItem("TRAINING", MenuItem.FileType.FOLDER),
        new MenuItem("sandbox.exe", MenuItem.FileType.FILE),
        new MenuItem("readme.txt", MenuItem.FileType.FILE),
        new MenuItem("quit.exe", MenuItem.FileType.FILE),
        new MenuItem("", MenuItem.FileType.DIVIDER),
        new MenuItem("SETTINGS", MenuItem.FileType.FOLDER),
        new MenuItem("TEST", MenuItem.FileType.FOLDER),
        new MenuItem("github.url", MenuItem.FileType.LINK).setUrl("https://github.com/Maciej4/2020FRCSim"),
        new MenuItem(),
        new MenuItem(),
        new MenuItem(),
        new MenuItem(),
        new MenuItem(),
        new MenuItem(),
        new MenuItem("TEST1", MenuItem.FileType.FOLDER),
        new MenuItem("C:\\", MenuItem.FileType.DIR)
    };
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            moveCursorDown();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            moveCursorUp();
        }

        for (int i = 0; i < textLines.Count; i++)
        {
            textLines[i].text = root[i].getItemString();
        }

        infoLine.text = generateInfoLine(root[cursorPos]);

        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            root[cursorPos].activate();

            switch (cursorPos)
            {
                case 1:
                    {
                        SceneManager.LoadScene("Main", LoadSceneMode.Single);
                        break;
                    }
                case 3:
                    {
                        Application.Quit();
                        break;
                    }
            }
        }
    }

    public void moveCursorDown()
    {
        if (cursorPos == 14)
        {
            return;
        }

        switch (root[cursorPos + 1].getItemType())
        {
            case MenuItem.FileType.DIVIDER:
                {
                    squareYPos -= 200f;
                    cursorPos += 2;
                    break;
                }
            case MenuItem.FileType.BLANK:
                {
                    break;
                }
            default:
                {
                    squareYPos -= 100f;
                    cursorPos++;
                    break;
                }
        }

        red.localPosition = new Vector3(-773f, squareYPos, 0);
    }

    public void moveCursorUp() 
    {
        if (cursorPos == 0)
        {
            squareYPos = 815f;
            red.localPosition = new Vector3(-773f, squareYPos, 0);
            return;
        }

        switch (root[cursorPos - 1].getItemType()) 
        {
            case MenuItem.FileType.DIVIDER:
                {
                    squareYPos += 200f;
                    cursorPos -= 2;
                    break;
                }
            case MenuItem.FileType.BLANK:
                {
                    break;
                }
            default:
                {
                    squareYPos += 100f;
                    cursorPos--;
                    break;
                }
        }

        red.localPosition = new Vector3(-773f, squareYPos, 0);
    }

    public string generateInfoLine(MenuItem menuItem)
    {
        string outputString = "<mspace=50>";//68

        switch (menuItem.getItemType()) 
        {
            case MenuItem.FileType.FOLDER:
                {
                    outputString += "directory: ";
                    return outputString + menuItem.getTitle() + "</mspace>";
                }
            case MenuItem.FileType.FILE:
                {
                    outputString += "executable: "; 
                    return outputString + menuItem.getTitle() + "</mspace>";
                }
            case MenuItem.FileType.LINK:
                {
                    outputString += "links to: ";
                    return outputString + menuItem.getTitle() + "</mspace>";
                }
        }

        return "";
    }
}
