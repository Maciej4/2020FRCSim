using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RobotMode : MonoBehaviour, IPointerClickHandler
{
    public Text Text;
    public RectTransform ArrowBottom;
    public RectTransform DropdownItems;

    public int RobotState = 0;
    private string[] RobotStates = new string[] { "Teleop", "Auto", "Test", "Practice" };

    public bool LockDropdown = false;
    private bool DropdownState = false;

    void Start()
    {
        SetState(RobotState);

        if (LockDropdown)
        {
            ArrowBottom.localPosition = new Vector3(77.5200348f, -3.6f, 0f);
        }
    }

    public void OpenDropdown()
    {
        DropdownState = true;
        ArrowBottom.localPosition = new Vector3(77.5200348f, 4.7f, 0f);
        DropdownItems.localPosition = Vector3.zero;
    }

    public void SetState(int NewRobotState)
    {
        DropdownState = false;
        RobotState = NewRobotState;
        Text.text = RobotStates[RobotState];
        ArrowBottom.localPosition = new Vector3(77.5200348f, -12.5999918f, 0f);
        DropdownItems.localPosition = new Vector3(0f, -500f, 0f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (LockDropdown) 
        {
            return;
        }

        if (DropdownState)
        {
            SetState(RobotState);
            return;
        }

        OpenDropdown();
    }
}
