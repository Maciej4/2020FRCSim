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
    private readonly string[] RobotStates = new string[] { "Teleop", "Auton", "Test", "Practice" };

    public bool IsDropdownLocked = false;
    private bool DropdownState = false;

    void Start()
    {
        SetState(RobotState);

        SetDropdownLockState(IsDropdownLocked);
    }

    public void OpenDropdown()
    {
        DropdownState = true;
        ArrowBottom.localPosition = new Vector3(77.5200348f, 4.7f, 0f);
        DropdownItems.localPosition = Vector3.zero;
    }

    public void CloseDropdown()
    {
        DropdownState = false;
        ArrowBottom.localPosition = new Vector3(77.5200348f, -12.5999918f, 0f);
        DropdownItems.localPosition = new Vector3(0f, -500f, 0f);
    }

    public void SetDropdownLockState(bool NewDropdownLockState)
    {
        IsDropdownLocked = NewDropdownLockState;

        if (IsDropdownLocked)
        {
            ArrowBottom.localPosition = new Vector3(77.5200348f, -3.6f, 0f);
        }
        else
        {
            ArrowBottom.localPosition = new Vector3(77.5200348f, -12.5999918f, 0f);
        }
    }

    public void SetState(int NewRobotState)
    {
        RobotState = NewRobotState;
        Text.text = RobotStates[RobotState];
        CloseDropdown();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsDropdownLocked) 
        {
            return;
        }

        if (DropdownState)
        {
            CloseDropdown();
            return;
        }

        OpenDropdown();
    }
}
