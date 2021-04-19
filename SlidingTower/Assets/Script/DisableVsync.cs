using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableVsync : MonoBehaviour
{
    void Start()
    {
        if (Application.isMobilePlatform)
        {
            QualitySettings.vSyncCount = 0;
        }
    }
}
