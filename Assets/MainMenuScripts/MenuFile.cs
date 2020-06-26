using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFile : MenuItem
{
    private string title = "";
    private string itemString = "";
    private string description = "";

    public enum FileAction
    {
        NONE,
        TEXT,
        SCENE,
        URL,
        QUIT,
        DIVIDER
    }

    private FileAction fileAction = FileAction.NONE;
    private string url = "";
    private string sceneToOpen = "";

    public MenuFile()
    {
        this.title = "";
        this.itemString = "";
    }

    public MenuFile(string title, FileAction fileAction)
    {
        this.title = title;
        this.fileAction = fileAction;
        itemString = findItemString();
    }

    public MenuFile setUrl(string url)
    {
        this.url = url;
        return this;
    }

    public MenuFile setScene(string sceneToOpen)
    {
        this.sceneToOpen = sceneToOpen;
        return this;
    }

    public string getUrl()
    {
        return url;
    }

    public FileAction getFileAction()
    {
        return fileAction;
    }

    public override void activate(MenuController menuController)
    {
        switch (fileAction)
        {
            case (FileAction.SCENE):
                {
                    SceneManager.LoadScene(sceneToOpen, LoadSceneMode.Single);
                    break;
                }
            case (FileAction.URL):
                {
                    Application.OpenURL(url);
                    break;
                }
            case (FileAction.QUIT):
                {
                    Application.Quit();
                    break;
                }
        }
    }

    public void activate(LevelMenu menuController)
    {
        switch (fileAction)
        {
            case (FileAction.SCENE):
                {
                    SceneManager.LoadScene(sceneToOpen, LoadSceneMode.Single);
                    break;
                }
            case (FileAction.URL):
                {
                    Application.OpenURL(url);
                    break;
                }
            case (FileAction.QUIT):
                {
                    Application.Quit();
                    break;
                }
        }
    }

    public override string findItemString()
    {
        string outputString = title;

        outputString = outputString.PadRight(13, ' ');

        switch (fileAction)
        {
            case (FileAction.SCENE):
                {
                    outputString += "--FILE->";
                    break;
                }
            case (FileAction.TEXT):
                {
                    outputString += "--FILE->";
                    break;
                }
            case (FileAction.URL):
                {
                    outputString += "--LINK->";
                    break;
                }
            case (FileAction.QUIT):
                {
                    outputString += "--FILE->";
                    break;
                }
            case (FileAction.DIVIDER):
                {
                    outputString = "------------ --------";
                    break;
                }
        }

        return "<mspace=68>" + outputString + "</mspace>";
    }

    public override MenuItem setDescription(string description)
    {
        this.description = description;
        return this;
    }

    public override bool isBlank()
    {
        return this.fileAction == FileAction.NONE;
    }

    public override bool isFolder()
    {
        return false;
    }

    public void setTitle(string title)
    {
        this.title = title;
    }

    public override string getTitle()
    {
        return title;
    }

    public override string getItemString()
    {
        return itemString;
    }

    public override string getDescription()
    {
        return description;
    }
}
