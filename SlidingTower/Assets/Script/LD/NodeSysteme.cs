using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSysteme : MonoBehaviour
{
    public List<NodeSysteme> closestNodes = new List<NodeSysteme>();
    public LayerMask nodeMask;
    public Color selectedColor;
    private Color startColor;

    public GameObject objBuild;
    public Transform nodePos;
    private MeshRenderer rend;

    private void Start()
    {
        nodePos = GetComponent<Transform>();
        rend = GetComponent<MeshRenderer>();
        startColor = rend.material.color;

        FindCloseNodes();
    }

    private void Update()
    {
        
    }

    public void TouchDetection()
    {
        if (objBuild == null)
        {
            GameObject objToBuild = BuildManager.instance.GetTurretToBuild();
            objBuild = Instantiate(objToBuild, transform.position, transform.rotation);
            ObjTypeDetection();
        }
        else if (objBuild != null)
        {
            if (!SlideManager.instance.isSliding)
            {
                SlideManager.instance.StartSlide(this);
            }
            else if (SlideManager.instance.isSliding)
            {
                SlideManager.instance.EndSlide();
            }
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

    void ObjTypeDetection()
    {
        if (objBuild != null)
        {
            if (objBuild.gameObject.layer == 9) // BoosBlock layer
            {
                // TakeBoost from block
            }
            else if (objBuild.gameObject.layer == 11) // Turret layer
            {
                // Give boost to turret;
            }
        }
    }
}
