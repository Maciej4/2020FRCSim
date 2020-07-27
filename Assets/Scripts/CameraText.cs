using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraText : MonoBehaviour
{
    private CameraController cameraController;
    public Text text;

    private readonly float textOpaqueTime = 4f;
    private readonly float textFadeTime = 1f;

    private Color currentColor = Color.white;
    private float messageStartTime;
    private int currentCameraPos;
    private int pastCameraPos = -1;

    private void Start()
    {
        cameraController = GetComponent<CameraController>();
    }

    void Update()
    {
        currentCameraPos = cameraController.currentPos;

        if (currentCameraPos != pastCameraPos)
        {
            currentColor.a = 1f;
            messageStartTime = Time.time;
            text.text = cameraController.posNames[currentCameraPos];
        }

        if (messageStartTime > Time.time - textOpaqueTime)
        {
            currentColor.a = 1f;
            text.color = currentColor;
        }
        else if (messageStartTime > Time.time - textFadeTime - textOpaqueTime)
        {
            currentColor.a = (messageStartTime + textFadeTime + textOpaqueTime - Time.time) / textFadeTime;
            text.color = currentColor;
        }
        else
        {
            currentColor.a = 0f;
            text.color = currentColor;
        }

        pastCameraPos = currentCameraPos;
    }
}
