using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelescopeBox : MonoBehaviour
{
    public Transform[] telescopeStages;

    public Transform[] topStage;

    private readonly float maxHeight = 0.48f;

    private float goalHeight = 0.01f;
    private float currentHeight = 0.01f;
    public float raisedHeight = 0.45f;
    public bool isRaised = false;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isRaised = !isRaised;
        }

        if (isRaised)
        {
            goalHeight = raisedHeight;
            //hookTilt.useSpring = true;
        }
        else
        {
            goalHeight = 0.01f;
            //hookTilt.useSpring = false;
        }

        currentHeight = Mathf.Lerp(currentHeight, goalHeight, 0.01f);

        for (int i = 0; i < telescopeStages.Length; i++)
        {
            telescopeStages[i].localPosition = new Vector3(0f, currentHeight * (i + 1) + 0.26f, 0f);
        }

        for (int i = 0; i < topStage.Length; i++)
        {
            topStage[i].localPosition = new Vector3(0f, currentHeight * (telescopeStages.Length) + 0.26f + 0.2586f, 0f);
        }

        if (0.8f < topStage[0].localPosition.y)
        {
            topStage[0].localRotation = Quaternion.Euler(-90f, 0f, 0f);
        }
        else
        {
            topStage[0].localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
