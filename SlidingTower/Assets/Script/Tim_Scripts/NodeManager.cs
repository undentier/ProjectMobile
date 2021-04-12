using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public static List<NodeSysteme> allNodes;

    void Awake()
    {
        allNodes = new List<NodeSysteme>();
        for (int i = 0; i < transform.childCount; i++)
        {
            allNodes.Add(transform.GetChild(i).GetComponent<NodeSysteme>());
        }
    }
}
