using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RobotModeItem : MonoBehaviour, IPointerClickHandler
{
    public RobotMode RobotMode;
    public int StateToSet;

    public void OnPointerClick(PointerEventData eventData)
    {
        RobotMode.SetState(StateToSet);
    }
}
