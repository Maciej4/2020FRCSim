using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform robot;
    public Vector3 robotRelRot = new Vector3(10, 0, 0);

    public Vector3[] positions = new Vector3[] {
        new Vector3(0, 0.275f, -0.5f),
        new Vector3(0, 1f, -2f),
        new Vector3(0, 2f, 0),
        new Vector3(-3.432f, 1.5f, 8.5f),
    };

    public Vector3 cameraPos = new Vector3(0, 1f, -2f);
    public float followDistance = 1.1f;
    public float followDistanceX = 0f;
    public float xAngle = 0f;
    public float yAngle = 0.6f;
    public Vector3 mouseStartPos = Vector3.zero;
    public Vector2 mouseDelta = Vector2.zero;
    public Vector2 startRots = Vector2.zero;
    public float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;
    //private Vector3 rotVel = Vector3.zero;

    public int currentPos = 0;

    // Start is called before the first frame update
    void Start()
    {
        robot = this.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                decreasePosition();
            }
            else
            {
                increasePosition();
            }
        }

        switch (currentPos) {
            case 0:
                {
                    this.transform.parent = robot;
                    /*if (Vector3.Distance(this.transform.localPosition, positions[0]) < 0.05f)
                    {
                        this.transform.localPosition = positions[0];
                        this.transform.localRotation = Quaternion.Euler(robotRelRot);
                    }
                    else if (Vector3.Distance(this.transform.localPosition, positions[0]) < 0.1f)
                    {
                        this.transform.localPosition = Vector3.SmoothDamp(this.transform.localPosition, positions[0], ref velocity, 0.5f);
                        this.transform.localRotation = Quaternion.Euler(Vector3.SmoothDamp(this.transform.localRotation.eulerAngles, robotRelRot, ref rotVel, 0.1f));
                    }
                    else
                    {
                        this.transform.localPosition = Vector3.SmoothDamp(this.transform.localPosition, positions[0], ref velocity, 0.5f);
                        this.transform.LookAt(robot);
                    }*/
                    this.transform.localPosition = positions[0];
                    this.transform.localRotation = Quaternion.Euler(robotRelRot);
                    break;
                }
            case 1:
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        mouseStartPos = Input.mousePosition;
                    }

                    if (Input.GetMouseButton(0))
                    {
                        mouseDelta = mouseStartPos - Input.mousePosition;
                        mouseDelta *= 0.002f;
                        xAngle = startRots.x + mouseDelta.x;
                        yAngle = startRots.y + mouseDelta.y;
                        yAngle = Mathf.Clamp(yAngle, 0f, Mathf.PI / 2 - 0.01f);
                    }

                    if (Input.GetMouseButtonUp(0))
                    {
                        startRots += mouseDelta;
                        startRots.y = Mathf.Clamp(startRots.y, 0f, Mathf.PI / 2 - 0.01f);
                    }

                    followDistance += Input.mouseScrollDelta.y * 0.1f;
                    followDistance = Mathf.Clamp(followDistance, 0.8f, 5f);

                    this.transform.parent = robot;
                    followDistanceX = followDistance * Mathf.Cos(yAngle);
                    cameraPos.x = followDistanceX * Mathf.Sin(xAngle);
                    cameraPos.y = followDistance * Mathf.Sin(yAngle);
                    cameraPos.z = -followDistanceX * Mathf.Cos(xAngle);
                    this.transform.localPosition = Vector3.SmoothDamp(this.transform.localPosition, cameraPos, ref velocity, smoothTime);
                    this.transform.LookAt(robot);
                    break;
                }
            default: 
                {
                    this.transform.parent = null;
                    this.transform.position = Vector3.SmoothDamp(this.transform.position, positions[currentPos], ref velocity, 0.5f);
                    this.transform.LookAt(robot);
                    break;
                }
        }
    }

    public void increasePosition()
    {
        if (currentPos >= positions.Length - 1)
        {
            currentPos = 0;
        }
        else
        {
            currentPos++;
        }
    }

    public void decreasePosition()
    { 
        if (currentPos <= 0)
        {
            currentPos = positions.Length - 1;
        }
        else
        {
            currentPos--;
        }
    }
}
