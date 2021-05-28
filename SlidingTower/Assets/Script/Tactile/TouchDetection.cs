using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TouchDetection : MonoBehaviour
{
    private static Ray ray;
    private static RaycastHit hit;
    [SerializeField]
    public static NodeSysteme currentlyHoveredNode;
    public NodeSysteme test;
    static Touch touch;

    private void Start()
    {
        currentlyHoveredNode = NodeManager.allNodes[0];

    }

    public static void UpdateCurrentNode()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            ray = Camera.main.ScreenPointToRay(touch.position);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == 8)
                {
                    for (int i = 0; i < NodeManager.allNodes.Count; i++)
                    {
                        if (hit.transform == NodeManager.allNodes[i].transform)
                        {
                            currentlyHoveredNode = NodeManager.allNodes[i];
                        }
                    }
                }
            }
        }
        else
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == 5)
                {
                    return;
                }

                if (hit.transform.gameObject.layer == 8)
                {
                    for (int i = 0; i < NodeManager.allNodes.Count; i++)
                    {
                        if (hit.transform == NodeManager.allNodes[i].transform)
                        {
                            currentlyHoveredNode = NodeManager.allNodes[i];
                        }
                    }
                }
            }
        }
    }

    private void Update()
    {
        test = currentlyHoveredNode;
    }
}
