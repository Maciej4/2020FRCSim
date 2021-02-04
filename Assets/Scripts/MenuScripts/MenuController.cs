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
    public int cursorPos = 2;
    public bool isScrollable = false;
    public int scrollPos = 0;

    private readonly float scrollThreshold = 0.5f;

    public List<MenuItem> root = new List<MenuItem>() {
        new MenuFolder("TRAINING",
            new List<MenuItem>() {
            new MenuFolder(),
            new MenuFile("level1.exe", MenuFile.FileAction.SCENE).setScene("level1")
                .setDescription("Some important controls:\n" +
                "- ESC opens the level menu\n" +
                "- W and S mock joystick axis 0\n" +
                "- A and D mock joystick axis 1\n" +
                "- P changes the perspective\n" +
                "  + Some perspectives allow for you to move your camera with the LEFT MOUSE BUTTON\n" +
                "- You MUST make an edit to your robot code or else it will not connect to the simulator"),
            new MenuFile("level2.exe", MenuFile.FileAction.SCENE).setScene("level2"),
            new MenuFile("level3.exe", MenuFile.FileAction.SCENE).setScene("level3")
                .setDescription("In this level you have to use a piston to press a button."),
            new MenuFile("sandbox.exe", MenuFile.FileAction.SCENE).setScene("sandbox")
                .setDescription("This level is a flat sandbox."),
        }),
        new MenuFolder("CHALLENGES",
            new List<MenuItem>() {
            new MenuFolder(),
            new MenuFile("FRC2020.exe", MenuFile.FileAction.SCENE).setScene("FRC2020").setDescription(
                "The FRC game in 2020. The goal of this game is generally " +
                "to shoot the yellow balls into the large hexagon."),
        }),
        new MenuFile("readme.txt", MenuFile.FileAction.TEXT).setDescription(
            "This game is designed to simulate a robot running on Java code. " +
            "TRAINING holds several tutorials in robot programming concepts. " +
            "CHALLENGES holds a simulator for the 2020 FRC game."),
        new MenuFile("quit.exe", MenuFile.FileAction.QUIT),
        new MenuFile("", MenuFile.FileAction.DIVIDER),
        new MenuFolder("SETTINGS",
            new List<MenuItem>() {
            new MenuFolder(),
            new MenuSetting(MenuSetting.Option.QUALITY, new String[]{"LOWEST >", "< LOW  >", "<MEDIUM>", "< HIGH >", "<HIGHER>", "< ULTRA "})
                .setDescription(
                "The visual quality, affects the textures, shadows, " +
                "anti-aliasing, etc. Generally lower " +
                "quality means higher performance.\n\n" +
                "Use the LEFT/RIGHT ARROW KEYS to change this option."),
            new MenuSetting(MenuSetting.Option.FULLSCREEN, new String[]{" FULL  >", "< WINDOW"})
                .setDescription(
                "Whether the game is in fullscreen or windowed mode.\n\n" +
                "Use the LEFT/RIGHT ARROW KEYS to change this option.")
        }),
        new MenuFile("github.url", MenuFile.FileAction.URL).setUrl("https://github.com/Maciej4/2020FRCSim")
            .setDescription("This URL links to: \n\nhttps://github.com/Maciej4/2020FRCSim\n\n" +
            "Which is the github repository that stores the source " +
            "code for this project."),
    };

    public List<MenuItem> currentDirectory;

    public List<int> directoryPositions = new List<int>();
    public List<string> directoryNames = new List<string>();

    private MenuFile blankMenuFile = new MenuFile();

    private void Start()
    {
        currentDirectory = root;
        RenderMenuText();
        RenderDescriptionArea();
    }

    void Update()
    {
        if (currentDirectory is null)
        {
            currentDirectory = root;
            directoryPositions = new List<int>();
            directoryNames = new List<string>();
        }

        bool scroll = scrollThreshold < Mathf.Abs(Input.mouseScrollDelta.y);
        bool scrollDown = Mathf.Sign(Input.mouseScrollDelta.y) < 0f;

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || (scroll && scrollDown))
        {
            moveCursorDown();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || (scroll && !scrollDown))
        {
            moveCursorUp();
        }

        clock.text = "<mspace=68>─" + DateTime.Now.ToString("HH:mm") + "─</mspace>";

        if (Input.GetKeyDown(KeyCode.Return))
        {
            currentDirectory[cursorPos + scrollPos].activate(this);
            scrollPos = 0;
            RenderMenuText();
        }

        if (currentDirectory[cursorPos + scrollPos].getTitle() == "SETTING")
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                ((MenuSetting)currentDirectory[cursorPos + scrollPos]).moveOptionLeft();
                RenderMenuText();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                ((MenuSetting)currentDirectory[cursorPos + scrollPos]).moveOptionRight();
                RenderMenuText();
            }
        }
    }

    private void RenderMenuText()
    {
        for (int i = 0; i < textLines.Count; i++)
        {
            textLines[i].text = "";
        }

        if (currentDirectory.Count <= 15)
        {
            isScrollable = false;

            for (int i = 0; i < currentDirectory.Count; i++)
            {
                textLines[i].text = currentDirectory[i].getItemString();
            }
        }
        else
        {
            isScrollable = true;

            for (int i = 0; i < 14; i++)
            {
                textLines[i].text = currentDirectory[i].getItemString();
            }

            textLines[14].text = "<mspace=68>     \\/         \\/</mspace>";
        }

        string directoryText = "C:\\";

        for (int i = 0; i < directoryNames.Count; i++)
        {
            directoryText += directoryNames[i] + "\\";
        }

        textLines[15].text = "<mspace=68>" + directoryText + "</mspace>";
    }

    private void RenderMenuTextScroll()
    {
        if (currentDirectory.Count - scrollPos == 15)
        {
            for (int i = scrollPos; i < scrollPos + 15; i++)
            {
                textLines[i - scrollPos].text = currentDirectory[i].getItemString();
            }
        }
        else
        {
            for (int i = scrollPos; i < scrollPos + 14; i++)
            {
                textLines[i - scrollPos].text = currentDirectory[i].getItemString();
            }

            textLines[14].text = "<mspace=68>     \\/         \\/</mspace>";
        }

        if (scrollPos != 0)
        {
            textLines[0].text = "<mspace=68>     /\\         /\\</mspace>";
        }
    }

    private void RenderDescriptionArea()
    {
        infoLine.text = generateInfoLine(currentDirectory[cursorPos + scrollPos]);
        description.text = currentDirectory[cursorPos + scrollPos].getDescription();
    }

    public void moveCursorUp()
    {
        if (isScrollable && scrollPos != 0 && cursorPos == 1)
        {
            scrollPos--;
            RenderDescriptionArea();
            RenderMenuTextScroll();
            return;
        }

        if (cursorPos == 0)
        {
            squareYPos = 815f;
            red.localPosition = new Vector3(-773f, squareYPos, 0);
            RenderDescriptionArea();
            return;
        }

        squareYPos += 100f;
        cursorPos--;

        red.localPosition = new Vector3(-773f, squareYPos, 0);

        RenderDescriptionArea();
    }

    public void moveCursorDown()
    {
        if (isScrollable && scrollPos != currentDirectory.Count - 15 && cursorPos == 13)
        {
            scrollPos++;
            RenderDescriptionArea();
            RenderMenuTextScroll();
            return;
        }

        if (cursorPos == 14)
        {
            return;
        }

        if (IsInvalidCursorPos(cursorPos + 1))
        {
            return;
        }

        squareYPos -= 100f;
        cursorPos++;

        red.localPosition = new Vector3(-773f, squareYPos, 0);

        RenderDescriptionArea();
    }

    public void setCursorPos(int cursorPos_)
    {
        cursorPos = cursorPos_;
        squareYPos = 815f;
        squareYPos = squareYPos - cursorPos_ * 100f;
        red.localPosition = new Vector3(-773f, squareYPos, 0);
        RenderDescriptionArea();
    }

    public void clickOnItem(int cursorPos_)
    {
        if (Input.GetKey(KeyCode.Return))
        {
            return;
        }

        if (IsInvalidCursorPos(cursorPos_))
        {
            return;
        }

        if (cursorPos == cursorPos_)
        {
            currentDirectory[cursorPos + scrollPos].activate(this);
            scrollPos = 0;
            RenderMenuText();
            return;
        }

        setCursorPos(cursorPos_);
    }

    private bool IsInvalidCursorPos(int cursorPos_)
    {
        if (currentDirectory.Count < cursorPos_ + 1 || currentDirectory[cursorPos_] is null)
        {
            return true;
        }

        return currentDirectory[cursorPos_].GetType() == blankMenuFile.GetType() &&
            ((MenuFile)currentDirectory[cursorPos_]).getFileAction() == MenuFile.FileAction.NONE &&
            currentDirectory[cursorPos_].getTitle().Equals("");
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
        else if (menuItem.getTitle() == "SETTING")
        {
            return "config";
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
