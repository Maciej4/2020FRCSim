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
        if (!(mainAntenna.unityPacket is null))
        {
            mainAntenna.unityPacket.joyAxisArray[0] = Input.GetAxis("Horizontal");
            mainAntenna.unityPacket.joyAxisArray[1] = Input.GetAxis("Vertical");
        }
    }
}
