using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItem
{
    public enum FileType
    {
        FILE,
        FOLDER,
        LINK,
        DIVIDER,
        BLANK,
        DIR
    }

    private string title = "";
    private FileType type = FileType.FILE;
    private string url = "";
    private string itemString = "";

    public MenuItem()
    {
        this.title = "";
        this.itemString = "";
        this.type = FileType.BLANK;
    }

    public MenuItem(string title, FileType type) 
    {
        this.title = title;
        this.type = type;
        itemString = findItemString();
    }

    public FileType getItemType() 
    {
        return type;
    }

    public MenuItem setUrl(string url)
    {
        this.url = url;
        return this;
    }

    public string getItemString() 
    {
        return itemString;
    }

    public string getTitle()
    {
        return title;
    }

    public string getUrl()
    {
        return url;
    }

    public string findItemString()
    {
        string outputString = title;

        if (type != FileType.DIR)
        {
            outputString = outputString.PadRight(13, ' ');
        }

        switch (type) 
        { 
            case FileType.FILE:
                {
                    outputString += "--FILE->";
                    break;
                }
            case FileType.FOLDER:
                {
                    outputString += ">FOLDER<";
                    break;
                }
            case FileType.LINK:
                {
                    outputString += "--LINK->";
                    break;
                }
            case FileType.DIVIDER:
                {
                    outputString = "------------ --------";
                    break;
                }
            case FileType.BLANK:
                {
                    outputString = "";
                    break;
                }
        }

        return "<mspace=68>" + outputString + "</mspace>";
    }

    public void activate() 
    {
        switch (type)
        {
            case FileType.FILE:
                {
                    break;
                }
            case FileType.FOLDER:
                {
                    break;
                }
            case FileType.LINK:
                {
                    Application.OpenURL(url);
                    break;
                }
        }
    }
}
