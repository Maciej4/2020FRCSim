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
    public TextMeshProUGUI clock;
    public Transform red;
    public float squareYPos = 759f;
    public int cursorPos = 0;

    public List<MenuItem> root = new List<MenuItem>() {
        new MenuFolder("TRAINING",
            new List<MenuItem>() {
            new MenuFolder(),
            new MenuFile("level1.exe", MenuFile.FileAction.SCENE).setScene("level1")
                .setDescription("Some important controls:\n" +
                "- W and S mock joystick axis 0\n" +
                "- A and D mock joystick axis 1\n" +
                "- Buttons are not yet implemented\n" +
                "- Press P to change perspective\n" +
                "  + Some perspectives allow for you to move your camera with the LEFT MOUSE BUTTON\n" +
                "- You MUST make an edit to your robot code or else it will not connect to the simulator"),
            new MenuFile("level2.exe", MenuFile.FileAction.SCENE).setScene("level2"),
            new MenuFile("level3.exe", MenuFile.FileAction.SCENE).setScene("level3")
                .setDescription("In this level you have to use a piston to press a button."),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile("C:\\TRAINING\\", MenuFile.FileAction.NONE)
        }),
        new MenuFolder("CHALLENGES",
            new List<MenuItem>() {
            new MenuFolder(),
            new MenuFile("FRC2020.exe", MenuFile.FileAction.SCENE).setScene("FRC2020").setDescription(
                "The FRC game in 2020. The goal of this game is generally " +
                "to shoot the yellow balls into the large hexagon."),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile("C:\\CHALLENGES\\", MenuFile.FileAction.NONE)
        }),
        new MenuFile("readme.txt", MenuFile.FileAction.TEXT).setDescription(
            "This game is designed to simulate a robot running on Java code. " +
            "TRAINING holds several tutorials in robot programming concepts. " +
            "CHALLENGES holds a simulator for the 2020 FRC game. " +
            "Use the UP and DOWN arrow keys and ENTER to navigate this menu."),
        new MenuFile("quit.exe", MenuFile.FileAction.QUIT),
        new MenuFile("", MenuFile.FileAction.DIVIDER),
        new MenuFolder("SETTINGS",
            new List<MenuItem>() {
            new MenuFolder(),
            new MenuFolder("CONTROLS",
                new List<MenuItem>() {
                new MenuFolder(),
                new MenuFile("temp.url", MenuFile.FileAction.URL).setUrl("https://www.youtube.com/watch?v=dQw4w9WgXcQ"),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile("C:\\SETTINGS\\CONTROLS\\", MenuFile.FileAction.NONE)
            }),
            new MenuFolder("GRAPHICS",
                new List<MenuItem>() {
                new MenuFolder(),
                new MenuFile("temp.url", MenuFile.FileAction.URL).setUrl("https://www.youtube.com/watch?v=dQw4w9WgXcQ"),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile("C:\\SETTINGS\\GRAPHICS\\", MenuFile.FileAction.NONE)
            }),
            new MenuFolder("AUDIO",
                new List<MenuItem>() {
                new MenuFolder(),
                new MenuFile("temp.url", MenuFile.FileAction.URL).setUrl("https://www.youtube.com/watch?v=dQw4w9WgXcQ"),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile("C:\\SETTINGS\\AUDIO\\", MenuFile.FileAction.NONE)
            }),
            new MenuFolder("GAMEPLAY",
                new List<MenuItem>() {
                new MenuFolder(),
                new MenuFile("temp.url", MenuFile.FileAction.URL).setUrl("https://www.youtube.com/watch?v=dQw4w9WgXcQ"),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile(),
                new MenuFile("C:\\SETTINGS\\GAMEPLAY\\", MenuFile.FileAction.NONE)
            }),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile(),
            new MenuFile("C:\\SETTINGS\\", MenuFile.FileAction.NONE)
        }),
        new MenuFile("github.url", MenuFile.FileAction.URL).setUrl("https://github.com/Maciej4/2020FRCSim")
            .setDescription("This URL links to: \n\nhttps://github.com/Maciej4/2020FRCSim\n\n" +
            "Which is the github repository that stores the source " +
            "code for this project."),
        new MenuFile(),
        new MenuFile(),
        new MenuFile(),
        new MenuFile(),
        new MenuFile(),
        new MenuFile(),
        new MenuFile(),
        new MenuFile(),
        new MenuFile("C:\\", MenuFile.FileAction.NONE)
    };

    public List<MenuItem> currentDirectory;

    public List<int> directoryPositions = new List<int> { 0 };

    private MenuFile blankMenuFile = new MenuFile();

    void Update()
    {
        if (currentDirectory is null)
        {
            currentDirectory = root;
        }

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
            textLines[i].text = currentDirectory[i].getItemString();
        }

        infoLine.text = generateInfoLine(currentDirectory[cursorPos]);
        description.text = currentDirectory[cursorPos].getDescription();

        clock.text = "<mspace=68>─" + DateTime.Now.ToString("HH:mm") + "─</mspace>";

        if (Input.GetKeyDown(KeyCode.Return))
        {
            currentDirectory[cursorPos].activate(this);
        }
    }

    public void moveCursorUp()
    {
        if (cursorPos == 0)
        {
            squareYPos = 815f;
            red.localPosition = new Vector3(-773f, squareYPos, 0);
            return;
        }

        squareYPos += 100f;
        cursorPos--;

        red.localPosition = new Vector3(-773f, squareYPos, 0);
    }

    public void moveCursorDown()
    {
        if (cursorPos == 14)
        {
            return;
        }

        if (currentDirectory[cursorPos + 1].GetType() == blankMenuFile.GetType() &&
            ((MenuFile)currentDirectory[cursorPos + 1]).getFileAction() == MenuFile.FileAction.NONE &&
            currentDirectory[cursorPos + 1].getTitle().Equals(""))
        {
            return;
        }

        squareYPos -= 100f;
        cursorPos++;

        red.localPosition = new Vector3(-773f, squareYPos, 0);
    }

    public void setCursorPos(int cursorPos_)
    {
        cursorPos = cursorPos_;
        squareYPos = 815f;
        squareYPos = squareYPos - cursorPos_ * 100f;
        red.localPosition = new Vector3(-773f, squareYPos, 0);
    }

    public void clickOnItem(int cursorPos_)
    {
        if (Input.GetKey(KeyCode.Return))
        {
            return;
        }

        if (currentDirectory[cursorPos_].GetType() == blankMenuFile.GetType() &&
            ((MenuFile)currentDirectory[cursorPos_]).getFileAction() == MenuFile.FileAction.NONE &&
            currentDirectory[cursorPos_].getTitle().Equals(""))
        {
            return;
        }

        if (cursorPos == cursorPos_)
        {
            currentDirectory[cursorPos].activate(this);
            return;
        }

        setCursorPos(cursorPos_);
    }

    public string generateInfoLine(MenuItem menuItem)
    {
        string outputString = "<mspace=50>";//68

        if (menuItem.isBlank())
        {
            return "";
        }
        else if (menuItem.isFolder())
        {
            outputString += "directory: ";
            return outputString + menuItem.getTitle() + "</mspace>";
        }

        switch (((MenuFile)menuItem).getFileAction())
        {
            case MenuFile.FileAction.NONE:
                {
                    outputString += "text: ";
                    break;
                }
            case MenuFile.FileAction.TEXT:
                {
                    outputString += "text: ";
                    break;
                }
            case MenuFile.FileAction.SCENE:
                {
                    outputString += "app: ";
                    break;
                }
            case MenuFile.FileAction.QUIT:
                {
                    outputString += "app: ";
                    break;
                }
            case MenuFile.FileAction.URL:
                {
                    outputString += "link: ";
                    break;
                }
        }

        return outputString + menuItem.getTitle() + "</mspace>";
    }
}
