using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [Header ("Unity Setup")]
    public string nodelayerName;
    public Color hoverColor;
    public Vector3 buildOffSet;

    [Space]

    [Header("Boost Stats")]
    public int lazerMode;
    public int doubleShootMode;
    public int explosionMode;

    [Space]

    public int slowBonus;
    public int poisonDamage;

    [Space]

    public int fireRateBonus;
    public int damageBonus;
    public int rangeBonus;

    [Space]

    [Header ("SFX")]
    public GameObject speedEffect;

    public BoostBlock boostScript;

    //[HideInInspector]
    public GameObject turret;
    //[HideInInspector]
    public List<GameObject> hitNode = new List<GameObject>();
    private Renderer rend;
    private LayerMask mask;
    private Color startColor;

    private void Start()
    {
        mask = LayerMask.GetMask(nodelayerName);
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

    public void ShareBoost()
    {
        if (boostScript != null)
        {
            GetBoost();
        }
    }

    void GetBoost()
    {
        GetShootType();
        GetBulletType();
        GetBoostType();

    }
    void GetShootType()
    {
        lazerMode += boostScript.lazer;
        doubleShootMode += boostScript.doubleShoot;
        explosionMode += boostScript.explosion;
    }
    void GetBulletType()
    {
        slowBonus += boostScript.slowValue;
        poisonDamage += boostScript.poisonValue;
    }
    void GetBoostType()
    {
        fireRateBonus += boostScript.fireRateBoost;
        damageBonus += boostScript.damageBoost;
        rangeBonus += boostScript.rangeBoost;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            speedEffect.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            speedEffect.SetActive(false);
        }
    }

    #region Controler
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        if (!SlidingManager.slidingInstance.isSliding)
        {
            if (turret != null)
            {
                SlidingManager.slidingInstance.isSliding = true;
                SlidingManager.slidingInstance.InfoStartNode(gameObject, turret, hitNode);
                turret.GetComponent<Animator>().SetBool("Selected", true);
                return;
            }
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

        if (turret == null)
        {
            GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
            turret = Instantiate(turretToBuild, transform.position + buildOffSet, transform.rotation);
        }
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
    #endregion

}
