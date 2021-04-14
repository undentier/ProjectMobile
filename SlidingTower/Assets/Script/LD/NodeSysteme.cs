using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSysteme : MonoBehaviour
{
    #region variable
    public List<NodeSysteme> closestNodes = new List<NodeSysteme>();
    public LayerMask nodeMask;

    [Header("FeedBack upgrade")]
    public GameObject fireRateEffect;
    public GameObject damageEffect;
    public GameObject rangeEffect;
    [Space]
    public GameObject slowEffect;
    public GameObject poisonEffect;

    [HideInInspector]
    public int laserUpgrade;
    [HideInInspector]
    public int explosionUpgrade;

    [HideInInspector]
    public int slowUpgrade;
    [HideInInspector]
    public int poisonUpgrade;

    [HideInInspector]
    public int fireRateUpgrade;
    [HideInInspector]
    public int damageUpgrade;
    [HideInInspector]
    public int rangeUpgrade;

    //[HideInInspector]
    public GameObject objBuild;

    private BoostBlock boostBlockScript;
    private Turret turretScript;
    #endregion

    private void Start()
    {
        FindCloseNodes();

        ObjTypeDetection();

        if (boostBlockScript != null)
        {
            GetUpgrade(boostBlockScript);
            UpgradeNeighbour(boostBlockScript);
            TurretNeighbour();
        }
        else if (turretScript != null)
        {
            turretScript.GetNodeUpgrade(this);
        }
    }

    public void TouchDetection()
    {
        if (objBuild == null && !SlideManager.instance.isSliding && LifeManager.lifeInstance.buildToken > 0)
        {
            GameObject objToBuild = BuildManager.instance.GetTurretToBuild();

            if (objToBuild == null)
            {
                return;
            }

            objBuild = Instantiate(objToBuild, transform.position, transform.rotation);
            LifeManager.lifeInstance.ChangeToken(-1);
            WavePanel.instance.startWaveButton.SetActive(true);

            ObjTypeDetection();

            if (boostBlockScript != null)
            {
                GetUpgrade(boostBlockScript);
                UpgradeNeighbour(boostBlockScript);
                TurretNeighbour();
            }
            else if (turretScript != null)
            {
                turretScript.GetNodeUpgrade(this);
            }
        } // Condition for build

        else if (objBuild != null)
        {
            if (!SlideManager.instance.isSliding)
            {
                ObjTypeDetection();

                if (boostBlockScript != null)
                {
                    DeleteUpgrade(boostBlockScript);
                    DownGradeNeighbour(boostBlockScript);
                    boostBlockScript = null;
                    TurretNeighbour();
                }
                else if (turretScript != null)
                {
                    turretScript.ResetUpgrade();
                    turretScript = null;
                }

                SlideManager.instance.StartSlide(this);
            }
        } // Condition for start sliding
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

            if (Physics.Raycast(transform.position, dir, out hit, 2f, nodeMask))
            {
                closestNodes.Add(hit.transform.gameObject.GetComponent<NodeSysteme>());
            }
            else
            {
                closestNodes.Add(null);
            }
        }
    }

    public void ObjTypeDetection()
    {
        if (objBuild != null)
        {
            if (objBuild.gameObject.layer == 9) // BoosBlock layer
            {
                boostBlockScript = objBuild.GetComponent<BoostBlock>();
            }
            else if (objBuild.gameObject.layer == 11) // Turret layer
            {
                turretScript = objBuild.GetComponent<Turret>();
            }
            else
            {
                boostBlockScript = null;
                turretScript = null;
            }
        }
    }

    void SetEffect()
    {
        #region Stats effect
        if (fireRateUpgrade > 0)
        {
            fireRateEffect.SetActive(true);
        }
        else
        {
            fireRateEffect.SetActive(false);
        }

        if (damageUpgrade > 0)
        {
            damageEffect.SetActive(true);
        }
        else
        {
            damageEffect.SetActive(false);
        }

        if (rangeUpgrade > 0)
        {
            rangeEffect.SetActive(true);
        }
        else
        {
            rangeEffect.SetActive(false);
        }
        #endregion

        #region Negatif effect
        if (slowUpgrade > 0)
        {
            slowEffect.SetActive(true);
        }
        else
        {
            slowEffect.SetActive(false);
        }

        if (poisonUpgrade > 0)
        {
            poisonEffect.SetActive(true);
        }
        else
        {
            poisonEffect.SetActive(false);
        }
        #endregion
    }

    #region Upgrade control
    public void GetUpgrade(BoostBlock boostBlock)
    {
        laserUpgrade += boostBlock.lazer;
        explosionUpgrade += boostBlock.explosion;

        slowUpgrade += boostBlock.slowValue;
        poisonUpgrade += boostBlock.poisonValue;

        fireRateUpgrade += boostBlock.fireRateBoost;
        damageUpgrade += boostBlock.damageBoost;
        rangeUpgrade += boostBlock.rangeBoost;

        SetEffect();
    }
    public void DeleteUpgrade(BoostBlock boostBlock)
    {
        laserUpgrade -= boostBlock.lazer;
        explosionUpgrade -= boostBlock.explosion;

        slowUpgrade -= boostBlock.slowValue;
        poisonUpgrade -= boostBlock.poisonValue;

        fireRateUpgrade -= boostBlock.fireRateBoost;
        damageUpgrade -= boostBlock.damageBoost;
        rangeUpgrade -= boostBlock.rangeBoost;

        SetEffect();
    }

    public void UpgradeNeighbour(BoostBlock boostBlock)
    {
        for (int i = 0; i < closestNodes.Count; i++)
        {
            if (closestNodes[i] != null)
            {
                closestNodes[i].GetUpgrade(boostBlock);
            }
        }
        SetEffect();
    }
    public void DownGradeNeighbour(BoostBlock boostBlock)
    {
        for (int i = 0; i < closestNodes.Count; i++)
        {
            if (closestNodes[i] != null)
            {
                closestNodes[i].DeleteUpgrade(boostBlock);
            }
        }
        SetEffect();
    }

    public void TurretNeighbour()
    {
        for (int i = 0; i < closestNodes.Count; i++)
        {
            if (closestNodes[i] != null)
            {
                if (closestNodes[i].turretScript != null)
                {
                    closestNodes[i].turretScript.GetNodeUpgrade(closestNodes[i]);
                }
            }
        }
    }
    #endregion
}
