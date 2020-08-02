using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotJoysticks : MonoBehaviour
{
    private RawImage RawImage;

    private bool JoystickState = false;

    private readonly Color Red = new Color(255, 0, 0);
    private readonly Color Green = new Color(0, 255, 0);

    void Start()
    {
        RawImage = GetComponent<RawImage>();

        bool IsJoystickConnected = Input.GetJoystickNames().Length == 0 || Input.GetJoystickNames()[0].Equals("");

        if (IsJoystickConnected != JoystickState)
        {
            ToggleJoystickState();
        }
    }

    void Update()
    {
        bool IsJoystickConnected = Input.GetJoystickNames().Length == 0 || Input.GetJoystickNames()[0].Equals("");

        if (IsJoystickConnected != JoystickState)
        {
            ToggleJoystickState();
        }
    }

    private void ToggleJoystickState()
    {
        JoystickState = !JoystickState;

        if (JoystickState)
        {
            RawImage.color = Red;
        }
        else
        {
            RawImage.color = Green;
        }

    }
}
