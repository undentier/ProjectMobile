using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartInfo : MonoBehaviour
{
    public static Transform startPoint;
    //public GameObject wavePreview;

    private bool uiActive = false;
    private void Awake()
    {
        startPoint = gameObject.transform;
    }

    private void Start()
    {
        //wavePreview.SetActive(false);
    }

    public void GetingTouch()
    {
        if (!uiActive)
        {
            uiActive = true;
            //wavePreview.SetActive(true);
        }
        else
        {
            uiActive = false;
            //wavePreview.SetActive(false);
        }
    }
}
