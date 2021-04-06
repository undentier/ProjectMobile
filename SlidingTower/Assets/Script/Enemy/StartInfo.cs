using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartInfo : MonoBehaviour
{
    public static Transform startPoint;
    private void Awake()
    {
        startPoint = gameObject.transform;
    }
}
