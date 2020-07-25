using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RobotEnable : MonoBehaviour, IPointerClickHandler
{
    public Text Text;
    private RawImage RawImage;

    private readonly Color Red = new Color(255, 0, 0);
    private readonly Color Green = new Color(0, 255, 0);

    public bool RobotState = false;

    private void Start()
    {
        RawImage = this.GetComponent<RawImage>();

        if (RobotState)
        {
            RawImage.color = Green;
            Text.text = "Disable";
        }
        else
        {
            RawImage.color = Red;
            Text.text = "Enable";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleRobotState();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ToggleRobotState();
    }

    public void ToggleRobotState()
    {
        if (RobotState)
        {
            RawImage.color = Red;
            Text.text = "Enable";
            RobotState = !RobotState;
            return;
        }

        RawImage.color = Green;
        Text.text = "Disable";
        RobotState = !RobotState;
    }

    public void SetRobotState(bool NewRobotState)
    {
        if (NewRobotState != RobotState)
        {
            ToggleRobotState();
        }
    }
}
