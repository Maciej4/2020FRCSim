using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //UnityPacket unityPacket = new UnityPacket();
        //unityPacket.hardware.Add();
        Debug.Log("---");
        Hardware talonFX = new TestMotor();
        Debug.Log(talonFX.type);
        Hardware[] hardware = new Hardware[1];
        hardware[0] = talonFX;
        Debug.Log(hardware[0].type);
        Debug.Log(hardware.Length);
        Debug.Log(JsonUtility.ToJson(hardware));

        Debug.Log("---");

        Debug.Log(JsonUtility.ToJson(hardware[0]));

        Debug.Log("---");

        TestClass123[] testClass123s = new TestClass123[1];
        testClass123s[0] = new TestClass123();

        Debug.Log(JsonUtility.ToJson(testClass123s));

        Debug.Log("---");

        Debug.Log(JsonUtility.ToJson(testClass123s[0]));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
