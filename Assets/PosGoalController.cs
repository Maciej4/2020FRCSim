using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PosGoalController : MonoBehaviour
{
    public Transform goodJobTextTransform;
    public Text goodJobText;
    public DriverStation driverStation;
    public LevelMenu levelMenu;
    private bool isRobotEnabled = false;
    private bool pastIsRobotEnabled = false;
    private float offsetTime = 0.0f;
    private bool isComplete = false;
    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.50f;
    private bool finalEdit = false;
    private string completionTimeString = "";

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Robot")) 
        {
            isComplete = true;
            completionTimeString = (Time.time - offsetTime).ToString("000.00");
            goodJobText.text = "Level complete!\nYou took: " + completionTimeString + " seconds";
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void Update()
    {
        if (isComplete)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                levelMenu.loadNextLevel();
            }

            goodJobTextTransform.localPosition = Vector3.SmoothDamp(goodJobTextTransform.localPosition, new Vector3(0f, 900f, 0f), ref velocity, smoothTime);
            if (800 < goodJobTextTransform.localPosition.y && !finalEdit)
            {
                finalEdit = true;
                goodJobText.text = "Press enter to go to next level!\nYou took: " + completionTimeString + " seconds";
            }
        }

        isRobotEnabled = driverStation.robotState;

        if (isRobotEnabled && !pastIsRobotEnabled)
        {
            offsetTime = Time.time;
        }

        pastIsRobotEnabled = isRobotEnabled;
    }
}
