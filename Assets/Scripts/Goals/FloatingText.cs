using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public RobotEnable robotEnable;
    public Transform target;
    public MeshRenderer text;

    void Update()
    {
        this.transform.LookAt(target);
        text.enabled = !robotEnable.RobotState;
    }
}
