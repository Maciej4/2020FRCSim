using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PosGoalController : MonoBehaviour, Goal
{
    public Transform goodJobTextTransform;
    public Text goodJobText;
    public RobotEnable robotEnable;
    public LevelMenu levelMenu;
    public GameObject prefabGoal;
    private bool isRobotEnabled = false;
    private bool pastIsRobotEnabled = false;
    private float offsetTime = 0.0f;
    private bool isComplete = false;
    private bool pastIsComplete = false;
    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.50f;
    private string completionTimeString = "";

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("robot"))
        {
            isComplete = true;
            completionTimeString = (Time.time - offsetTime).ToString("000.00");
            goodJobText.text = "Press enter to go to next level!\nYou took: " + completionTimeString + " seconds";
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void Update()
    {
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

    public void ResetGoal()
    {
        isComplete = false;
        //goodJobTextTransform.localPosition = Vector3.zero;
        //goodJobText.text = "";
        this.GetComponent<MeshRenderer>().enabled = true;
        this.GetComponent<BoxCollider>().enabled = true;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("goal");

        if (1 <= objs.Length)
        {
            if (!prefabGoal.name.Equals("blankgoal"))
            {
                Destroy(objs[0]);
                Instantiate(prefabGoal);
            }
        }
    }
}
