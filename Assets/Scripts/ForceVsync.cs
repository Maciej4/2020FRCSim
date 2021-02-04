using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceVsync : MonoBehaviour
{
    void Start()
    {
        // Sync framerate to monitors refresh rate
        QualitySettings.vSyncCount = 1;
    }
}
