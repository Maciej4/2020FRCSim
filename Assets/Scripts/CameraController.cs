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
        //new Vector3(0, 2f, 0),
        new Vector3(-3.432f, 1.5f, 8.5f),
        Vector3.zero
    };

    public Vector3 cameraPos = new Vector3(0, 1f, -2f);

    public float followDistance = 1.1f;
    public float followDistanceX = 0f;
    public float xAngle = 0f;
    public float yAngle = 0.6f;
    public Vector2 startRots = Vector2.zero;

    public float followDistanceA = 10.4f;
    public float followDistanceXA = 0f;
    public float xAngleA = -2.584484f;
    public float yAngleA = 0.3417884f;
    public Vector2 startRotsA = Vector2.zero;

    public Vector3 mouseStartPos = Vector3.zero;
    public Vector2 mouseDelta = Vector2.zero;
    public float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;
    //private Vector3 rotVel = Vector3.zero;

    public bool orbitMode = true;

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
            case 3:
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        orbitMode = false;
                        mouseStartPos = Input.mousePosition;
                    }

                    if (Input.GetMouseButton(0))
                    {
                        mouseDelta = mouseStartPos - Input.mousePosition;
                        mouseDelta *= 0.002f;
                        xAngleA = startRotsA.x + mouseDelta.x;
                        yAngleA = startRotsA.y + mouseDelta.y;
                        yAngleA = Mathf.Clamp(yAngleA, 0.1f, Mathf.PI / 2 - 0.01f);
                    }

                    if (Input.GetMouseButtonUp(0))
                    {
                        startRotsA += mouseDelta;
                        startRotsA.y = Mathf.Clamp(startRotsA.y, 0.1f, Mathf.PI / 2 - 0.01f);
                    }

                    if (orbitMode)
                    {
                        xAngleA += 0.3f * Time.deltaTime;
                        startRotsA.x = xAngleA;
                    }

                    followDistanceA += Input.mouseScrollDelta.y * 0.4f;
                    followDistanceA = Mathf.Clamp(followDistanceA, 4f, 15f);

                    this.transform.parent = null;
                    followDistanceXA = followDistanceA * Mathf.Cos(yAngleA);
                    cameraPos.x = followDistanceXA * Mathf.Sin(xAngleA);
                    cameraPos.y = followDistanceA * Mathf.Sin(yAngleA);
                    cameraPos.z = -followDistanceXA * Mathf.Cos(xAngleA);
                    this.transform.localPosition = Vector3.SmoothDamp(this.transform.localPosition, cameraPos, ref velocity, smoothTime);
                    this.transform.LookAt(Vector3.zero);
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
        orbitMode = false;

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
        orbitMode = false;

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
