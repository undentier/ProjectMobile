using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [Header ("Unity Setup")]
    public string layerName;
    public Color hoverColor;
    public Vector3 buildOffSet;

    [Space]

    [Header("Boost Stats")]
    public bool lazerMode;
    public bool doubleShootMode;
    public bool explosionMode;

    [Space]

    public int slowBonus;
    public int poisonDamage;

    [Space]

    public int fireRateBonus;
    public int damageBonus;
    public int rangeBonus;

    [Space]

    [Header ("BoostEffect")]
    public GameObject speedEffect;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public List<GameObject> hitNode = new List<GameObject>();
    private Renderer rend;
    private LayerMask mask;
    private Color startColor;

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
        
            if (!SlidingManager.slidingInstance.isSliding)
            {
                if (turret != null)
                {
                    turret.GetComponent<Animator>().SetBool("Selected", true);
                    SlidingManager.slidingInstance.isSliding = true;
                    SlidingManager.slidingInstance.InfoStartNode(gameObject, turret, hitNode);
                    return;
                }
            }
        
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

    private void OnMouseUp()
    {
        if (SlidingManager.slidingInstance.isSliding)
        {
            SlidingManager.slidingInstance.isSliding = false;
            SlidingManager.slidingInstance.selectedTower.GetComponent<Animator>().SetBool("Selected", false); // Nul à chier à changer
            SlidingManager.slidingInstance.InfoStartNode(null, null, null);
            return;
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
