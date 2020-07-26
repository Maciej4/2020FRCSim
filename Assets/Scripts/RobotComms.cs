using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RobotComms : MonoBehaviour, IPointerClickHandler
{
    public ZMQClient zmqClient;
    public NanoStation nanoStation;
    private RawImage RawImage;

    private readonly Color Red = new Color(255, 0, 0);
    private readonly Color Green = new Color(0, 255, 0);

    private void Start()
    {
        RawImage = GetComponent<RawImage>();
    }

    private void Update()
    {
        if (zmqClient.isComms())
        {
            RawImage.color = Green;
        }
        else
        {
            RawImage.color = Red;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (nanoStation is null)
        {
            return;
        }

        nanoStation.KillRobotComms();
    }
}
