using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNetDrive : NetworkBehaviour
{
    public Rigidbody rigidbody;

    // need to use FixedUpdate for rigidbody
    void FixedUpdate()
    {
        // only let the local player control the racket.
        // don't control other player's rackets
        if (isLocalPlayer)
            if (Input.GetKey(KeyCode.Space))
                rigidbody.AddForce(1f * Vector3.up, ForceMode.Impulse);
    }
}
