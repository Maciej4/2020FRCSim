using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFolder : MenuItem
{
    private string title = "";
    private string itemString = "";
    private string description = "";
    public List<MenuItem> folder;
    private bool isUpdir = false;

    public MenuFolder()
    {
        title = "GO UP";
        itemString = "<mspace=68>/..          <UP-FOL></mspace>";
        isUpdir = true;
    }

    public MenuFolder(string title, List<MenuItem> folder)
    {
        this.title = title;
        this.folder = folder;
        itemString = findItemString();
    }

    public override void activate(MenuController menuController)
    {
        if (isUpdir)
        {
            List<MenuItem> deepestFolder = null;

            if (1 < menuController.directoryPositions.Count)
            {
                for (int i = 0; i < menuController.directoryPositions.Count - 1; i++)
                {
                    if (deepestFolder is null)
                    {
                        deepestFolder = ((MenuFolder)menuController.root[menuController.directoryPositions[0]]).folder;
                    }
                    else
                    {
                        deepestFolder = ((MenuFolder)deepestFolder[i]).folder;
                    }
                }

                menuController.currentDirectory = deepestFolder;
            }
            else
            {
                menuController.currentDirectory = menuController.root;
            }

            menuController.setCursorPos(menuController.directoryPositions[menuController.directoryPositions.Count - 1]);
            menuController.directoryPositions.RemoveAt(menuController.directoryPositions.Count - 1);
        }
        else
        {
            menuController.currentDirectory = folder;
            menuController.directoryPositions.Add(menuController.cursorPos);
            menuController.setCursorPos(0);
        }
    }

    public override string findItemString()
    {
        string outputString = title;
        
        outputString = outputString.PadRight(13, ' ');

        return "<mspace=68>" + outputString + ">FOLDER<</mspace>";
    }

    public override MenuItem setDescription(string description)
    {
        this.description = description;
        return this;
    }

    public override bool isBlank()
    {
        return false;
    }

    public override bool isFolder()
    {
        return true;
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
