using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenu : MonoBehaviour
{
    public Text f1ForHelpText;

    private readonly Vector3 downPosition = new Vector3(0f, -600f, 0f);
    private RectTransform rectTransform;

    private bool helpMenuState = false;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) 
        {
            ToggleHelpMenu();
        }
    }

    public void ToggleHelpMenu()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            return;
        }

        if (helpMenuState) 
        {
            rectTransform.anchoredPosition = Vector3.zero;
            f1ForHelpText.text = "F1 for help";
            helpMenuState = false;
            return;
        }

        rectTransform.anchoredPosition = downPosition;
        f1ForHelpText.text = "F1 to hide";
        helpMenuState = true;
    }
}
