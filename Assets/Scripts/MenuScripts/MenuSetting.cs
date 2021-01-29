using UnityEngine;

public class MenuSetting : MenuItem
{
    private string title = "SETTING";
    private string itemString = "";
    private string description = "";
    private Option option;

    // Script serialization error if Screen.fullscreen is called in constructor.
    private bool isItemStringFound = false;

    public enum Option
    {
        QUALITY,
        FULLSCREEN,
        RESOLUTION
    }

    private string[] optionNames =
    {
        "quality",
        "fullscreen",
        "resolution"
    };

    public string[] avaliableOptions = { };

    public MenuSetting(Option option, string[] avaliableOptions)
    {
        this.option = option;
        this.avaliableOptions = avaliableOptions;
        //itemString = findItemString();
    }

    public override void activate(MenuController menuController)
    {
        return;
    }

    private int getCurrentOption()
    {
        switch (option)
        {
            case (Option.QUALITY):
                {
                    return QualitySettings.GetQualityLevel();
                }
            case (Option.FULLSCREEN):
                {
                    return Screen.fullScreen ? 0 : 1;
                }
            default:
                {
                    return 0;
                }
        }
    }

    public void moveOptionLeft()
    {
        switch (option)
        {
            case (Option.QUALITY):
                {
                    QualitySettings.DecreaseLevel();
                    break;
                }
            case (Option.FULLSCREEN):
                {
                    Resolution maxRes = Screen.resolutions[Screen.resolutions.Length - 1];
                    Screen.SetResolution(maxRes.width, maxRes.height, true);
                    forceFindItemString(0);
                    return;
                }
            default:
                {
                    break;
                }
        }

        findItemString();
    }

    public void moveOptionRight()
    {
        switch (option)
        {
            case (Option.QUALITY):
                {
                    QualitySettings.IncreaseLevel();
                    break;
                }
            case (Option.FULLSCREEN):
                {
                    Screen.fullScreen = false;
                    forceFindItemString(1);
                    return;
                }
            default:
                {
                    break;
                }
        }

        findItemString();
    }

    public override string findItemString()
    {
        string outputString = optionNames[(int)option];

        outputString = outputString.PadRight(13, ' ');

        outputString += avaliableOptions[getCurrentOption()];//"N/A";

        itemString = "<mspace=68>" + outputString + "</mspace>";

        return itemString;
    }

    private string forceFindItemString(int forceOption)
    {
        string outputString = optionNames[(int)option];

        outputString = outputString.PadRight(13, ' ');

        outputString += avaliableOptions[forceOption];//"N/A";

        itemString = "<mspace=68>" + outputString + "</mspace>";

        return itemString;
    }

    public override string getDescription()
    {
        return description;
    }

    public override string getItemString()
    {
        if (!isItemStringFound)
        {
            isItemStringFound = true;
            findItemString();
        }

        return itemString;
    }

    public override string getTitle()
    {
        return title;
    }

    public override bool isBlank()
    {
        return false;
    }

    public override bool isFolder()
    {
        return false;
    }

    public override MenuItem setDescription(string description)
    {
        this.description = description;
        return this;
    }
}
