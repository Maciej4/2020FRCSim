using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    void Update()
    {
        if (this.transform.position.y < -1)
        {
            teleport();
        }
    }

    public void teleport()
    {
        this.transform.position = new Vector3(1.591f, 1.716f, -8.468f);
    }
}
