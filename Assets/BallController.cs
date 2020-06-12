using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rigidbody;

    private void Start() 
    {
        rigidbody = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (this.transform.position.y < -1)
        {
            teleport();
        }
    }

    public void teleport()
    {
        if (rigidbody is null) 
        {
            rigidbody = this.GetComponent<Rigidbody>();
        }

        rigidbody.velocity = new Vector3(0f, 0f, 0f);
        this.transform.position = new Vector3(1.591f, 1.716f, -8.468f);
    }
}
