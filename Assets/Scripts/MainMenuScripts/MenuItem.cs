using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class MenuItem
{
    private string title;
    private string itemString;
    private string description;

    public abstract void activate(MenuController menuController);
    public abstract string findItemString();
    public abstract MenuItem setDescription(string description);

    public abstract bool isBlank();

    public abstract bool isFolder();

    public abstract string getTitle();

    public abstract string getItemString();

    public abstract string getDescription();
}
