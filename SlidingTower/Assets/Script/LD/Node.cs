using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    private Color startColor;
    public Vector3 buildOffSet;

    public string layerName;
    private LayerMask mask;

    public List<GameObject> hitNode = new List<GameObject>();

    private Renderer rend;

    
    public GameObject turret;

    private void Start()
    {
        mask = LayerMask.GetMask(layerName);
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        FindClosestNode();
    }

    void FindClosestNode()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.forward, out hit, Mathf.Infinity, mask) == true)
        {
            Debug.DrawRay(transform.position, Vector3.forward, Color.yellow, 100f);
            hitNode.Add(hit.transform.gameObject);
        }
        else
        {
            hitNode.Add(null);
        }

        if (Physics.Raycast(transform.position, Vector3.right, out hit, Mathf.Infinity, mask) == true)
        {
            Debug.DrawRay(transform.position, Vector3.right, Color.yellow, 100f);
            hitNode.Add(hit.transform.gameObject);
        }
        else
        {
            hitNode.Add(null);
        }

        if (Physics.Raycast(transform.position, Vector3.back, out hit, Mathf.Infinity, mask) == true)
        {
            Debug.DrawRay(transform.position, Vector3.back, Color.yellow, 100f);
            hitNode.Add(hit.transform.gameObject);
        }
        else
        {
            hitNode.Add(null);
        }

        if (Physics.Raycast(transform.position, Vector3.left, out hit, Mathf.Infinity, mask) == true)
        {
            Debug.DrawRay(transform.position, Vector3.left, Color.yellow, 100f);
            hitNode.Add(hit.transform.gameObject);
        }
        else
        {
            hitNode.Add(null);
        }
    }
    

    private void OnMouseDown()
    {
        if (SlidingManager.slidingInstance.slidingMode && !SlidingManager.slidingInstance.isSliding)
        {
            if (turret != null)
            {
                SlidingManager.slidingInstance.isSliding = true;
                SlidingManager.slidingInstance.InfoStartNode(gameObject, turret, hitNode);
                return;
            }
        }
        else
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (BuildManager.instance.GetTurretToBuild() == null)
            {
                return;
            }

            if (turret != null)
            {
                Debug.Log("Can't build there");
                return;
            }


            GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
            turret = Instantiate(turretToBuild, transform.position + buildOffSet, transform.rotation);
        }
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
