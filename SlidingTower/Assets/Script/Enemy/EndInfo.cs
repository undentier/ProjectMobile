using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndInfo : MonoBehaviour
{
    public static Transform endPoint;

    private void Awake()
    {
        endPoint = gameObject.transform;
    }
}
