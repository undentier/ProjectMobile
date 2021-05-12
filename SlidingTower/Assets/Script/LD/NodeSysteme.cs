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
    public GameObject particlelvl1;
    public GameObject particlelvl2;
    public GameObject RendererFireRate;
    private Material shaderMatFireRate;
    public GameObject particlelvl1Damage;
    public GameObject particlelvl2Damage;
    public GameObject RendererDamage;
    private Material shaderMatDamage;
    [Space]
    public GameObject slowEffect;
    public GameObject poisonEffect;
    [Space]
    public GameObject explosionEffect;
    public GameObject laserEffect;

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

    [Header ("Object build on")]
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

        shaderMatFireRate = RendererFireRate.GetComponent<MeshRenderer>().material;
        shaderMatDamage = RendererDamage.GetComponent<MeshRenderer>().material;
    }

    public void CreateTurret()
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
        } //Condition for build

    }

    public void SlideTurret()
    {
        if (objBuild != null || SlideManager.instance.isSliding)
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

            if (fireRateUpgrade == 1)
            {
                shaderMatFireRate.SetFloat("height", 0.31f);
                particlelvl1.SetActive(false);
                particlelvl2.SetActive(false);
            }

            if (fireRateUpgrade == 2)
            {
                shaderMatFireRate.SetFloat("height", 0.5f);
                particlelvl1.SetActive(true); 
                particlelvl2.SetActive(false);
            }

            if (fireRateUpgrade == 3)
            {
                shaderMatFireRate.SetFloat("height", 0.75f);
                particlelvl1.SetActive(false);
                particlelvl2.SetActive(true);
            }

            if (fireRateUpgrade == 4)
            {
                shaderMatFireRate.SetFloat("height", 0.75f);
                particlelvl1.SetActive(true);
                particlelvl2.SetActive(true);
            }
        }
        else
        {
            fireRateEffect.SetActive(false);
            particlelvl1.SetActive(false);
            particlelvl2.SetActive(false);
        }

        if (damageUpgrade > 0)
        {
            damageEffect.SetActive(true);

            if (damageUpgrade == 1)
            {
                shaderMatDamage.SetFloat("height", 0.31f);
                particlelvl1Damage.SetActive(false);
                particlelvl2Damage.SetActive(false);
            }

            if (damageUpgrade == 2)
            {
                shaderMatDamage.SetFloat("height", 0.5f);
                particlelvl1Damage.SetActive(true);
                particlelvl2Damage.SetActive(false);
            }

            if (damageUpgrade == 3)
            {
                shaderMatDamage.SetFloat("height", 0.75f);
                particlelvl1Damage.SetActive(false);
                particlelvl2Damage.SetActive(true);
            }

            if (damageUpgrade == 4)
            {
                shaderMatDamage.SetFloat("height", 0.75f);
                particlelvl1Damage.SetActive(true);
                particlelvl2Damage.SetActive(true);
            }
        }
        else
        {
            damageEffect.SetActive(false);
            particlelvl1Damage.SetActive(false);
            particlelvl2Damage.SetActive(false);
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

        #region Shoot type
        if (explosionUpgrade > 0)
        {
            explosionEffect.SetActive(true);
        }
        else
        {
            explosionEffect.SetActive(false);
        }

        if (laserUpgrade > 0)
        {
            laserEffect.SetActive(true);
        }
        else
        {
            laserEffect.SetActive(false);
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
