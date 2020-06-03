using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanInterface : MonoBehaviour
{
    ZMQClient mainAntenna;

    // Start is called before the first frame update
    void Start()
    {
        mainAntenna = this.GetComponent<ZMQClient>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mainAntenna.unityPacket.driveX = Input.GetAxis("Horizontal");
        mainAntenna.unityPacket.driveY = Input.GetAxis("Vertical");
    }
}
