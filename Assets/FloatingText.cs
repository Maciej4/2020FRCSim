using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public Transform target;
    public MeshRenderer text;

    public bool hideText;

    void Update()
    {
        this.transform.LookAt(target);
        text.enabled = !hideText;
    }
}
