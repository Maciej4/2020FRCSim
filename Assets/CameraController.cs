using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform robot;

    public int currentPos = 0;
    public int maxPos = 9;
    public Vector3 robotRelPos = new Vector3(0, 2f, -1f);
    public Vector3 robotRelRot = new Vector3(0, 0, 0);
    public Vector3 pos1 = new Vector3(0, 2f, 0);
    public Vector3 pos2 = new Vector3(3.39f, 1.5f, -8.16f);
    public Vector3 pos3 = new Vector3(-0.304f, 1.5f, -8.441f);
    public Vector3 pos4 = new Vector3(-3.283f, 1.5f, -8.065f);
    public Vector3 pos5 = new Vector3(-0.304f, 1.5f, -8.441f);
    public Vector3 pos6 = new Vector3(-3.432f, 1.5f, 8.151f);
    public Vector3 pos7 = new Vector3(0.134f, 1.5f, 8.464f);
    public Vector3 pos8 = new Vector3(3.369f, 1.5f, 8.108f);
    public Vector3 pos9 = new Vector3(10f, 4f, 0f);

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
            addVal();
        }

        switch (currentPos) {
            case 0:
                {
                    this.transform.parent = robot;
                    this.transform.localPosition = robotRelPos;
                    this.transform.localRotation = Quaternion.Euler(robotRelRot);
                    break;
                }

            case 1:
                {
                    this.transform.parent = null;
                    this.transform.position = pos1;
                    this.transform.LookAt(robot);
                    break;
                }

            case 2:
                {
                    this.transform.parent = null;
                    this.transform.position = pos2;
                    this.transform.LookAt(robot);
                    break;
                }

            case 3:
                {
                    this.transform.parent = null;
                    this.transform.position = pos3;
                    this.transform.LookAt(robot);
                    break;
                }
            case 4:
                {
                    this.transform.parent = null;
                    this.transform.position = pos4;
                    this.transform.LookAt(robot);
                    break;
                }
            case 5:
                {
                    this.transform.parent = null;
                    this.transform.position = pos5;
                    this.transform.LookAt(robot);
                    break;
                }
            case 6:
                {
                    this.transform.parent = null;
                    this.transform.position = pos6;
                    this.transform.LookAt(robot);
                    break;
                }
            case 7:
                {
                    this.transform.parent = null;
                    this.transform.position = pos7;
                    this.transform.LookAt(robot);
                    break;
                }
            case 8:
                {
                    this.transform.parent = null;
                    this.transform.position = pos8;
                    this.transform.LookAt(robot);
                    break;
                }
            case 9:
                {
                    this.transform.parent = null;
                    this.transform.position = pos9;
                    this.transform.LookAt(robot);
                    break;
                }
        }
    }

    public void addVal()
    {
        if (currentPos == maxPos)
        {
            currentPos = 0;
        }
        else
        {
            currentPos++;
        }
    }
}
