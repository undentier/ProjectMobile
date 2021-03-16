using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSysteme : MonoBehaviour
{
    public List<NodeSysteme> closestNodes = new List<NodeSysteme>();
    public Color selectedColor;
    public LayerMask nodeMask;
    private Color startColor;
    private MeshRenderer rend;

    public bool isSelected;
    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
        startColor = rend.material.color;
        FindCloseNodes();
    }

    private void Update()
    {
        if (isSelected)
        {
            rend.material.color = selectedColor;
        }
        else
        {
            rend.material.color = startColor;
        }
    }

    public void TouchDetection()
    {
        if (!isSelected)
        {
            isSelected = true;
        }
        else
        {
            isSelected = false;
        }
    }

    void FindCloseNodes()
    {
        for (int i = 0; i < 4; i++)
        {
            Vector3 dir = Vector3.zero;
            RaycastHit hit;
            switch (i)
            {
                case 0:
                    dir = Vector3.forward;
                    break;
                case 1:
                    dir = Vector3.right;
                    break;
                case 2:
                    dir = Vector3.back;
                    break;
                case 3:
                    dir = Vector3.left;
                    break;
            }

            if (Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity, nodeMask))
            {
                closestNodes.Add(hit.transform.gameObject.GetComponent<NodeSysteme>());
            }
            else
            {
                closestNodes.Add(null);
            }
        }
    }
}
