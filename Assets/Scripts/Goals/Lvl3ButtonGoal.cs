using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lvl3ButtonGoal : MonoBehaviour, Goal
{
    public Transform goodJobTextTransform;
    public Text goodJobText;
    public RobotEnable robotEnable;
    public LevelMenu levelMenu;
    public Rigidbody goalRigidbody;
    private bool isRobotEnabled = false;
    private bool pastIsRobotEnabled = false;
    private float offsetTime = 0.0f;
    private bool isComplete = false;
    private bool pastIsComplete = false;
    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.50f;
    private string completionTimeString = "";

    public void ResetGoal()
    {
        isComplete = false;
        this.transform.position = new Vector3(0f, 0.4549561f, 0.2f);
        goalRigidbody.isKinematic = false;
    }

    void Update()
    {
        if (transform.position.z < 0.15f && !isComplete)
        {
            isComplete = true;
            goalRigidbody.isKinematic = true;
            goalRigidbody.velocity = Vector3.zero;
            completionTimeString = (Time.time - offsetTime).ToString("000.00");
            goodJobText.text = "Press enter to go to next level!\nYou took: " + completionTimeString + " seconds";
        }

        if (isComplete)
        {
            pastIsComplete = true;
        }

        if (pastIsComplete)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                levelMenu.loadNextLevel();
            }

            goodJobTextTransform.localPosition = Vector3.SmoothDamp(goodJobTextTransform.localPosition, new Vector3(0f, 900f, 0f), ref velocity, smoothTime);
        }

        isRobotEnabled = robotEnable.RobotState;

        if (isRobotEnabled && !pastIsRobotEnabled)
        {
            offsetTime = Time.time;
        }

        pastIsRobotEnabled = isRobotEnabled;
    }
}
