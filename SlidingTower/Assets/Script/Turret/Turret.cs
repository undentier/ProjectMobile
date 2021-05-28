using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    #region Variable
    [Header ("Basic stats")]
    public float startFireRate = 3;
    public float startDamage;
    public float startRange;
    public float rotationSpeed;
    public int numMaxTargets = 5;

    #region Upgrade Value
    [Header ("Value of stat upgrade")]
    public float[] fireRateBonus;
    public int[] damageBonus;
    public float[] rangeBonus;

    private float actualFireRate;
    private float actualDamage;
    private float actualRange;

    [Header ("Value of negatif effect")]
    public float[] slowForceBonus;
    public float[] slowDurationBonus;

    private float actualSlowForce;
    private float actualSlowDuration;
    [Space]
    public float[] poisonDamageBonus;
    public float[] poisonDurationBonus;
    public float[] poisonTickBonus;

    private float actualpoisonDamage;
    private float actualPoisonDuration;
    private float actualPoisonTick;

    [Header("Value of shooting type")]
    public float[] explosionRadiusBonus;
    public int[] numOfCanonBonus;

    private float actualExplosionRadius;
    private int actualNumOfCanon;
    [Space]
    public float[] laserDamageReductionBonus;
    public float[] laserFirerateMultiplierBonus;
    public float[] microLaserDamageReductionBonus;

    private float actualLaserDamageReduction;
    private float actualLaserFireRateMultiplier;
    private float actualMicroLaserDamageReduction;
    #endregion

    [Header ("Unity setup")]
    public Transform partToRotate;
    public GameObject basicBullet;
    public GameObject explosiveBullet;
    public Transform shootPoint;
    public LineRenderer[] laserLines;


    [Header("Mesh")]
    public GameObject basicCanon;
    public GameObject explosionCanon;
    public GameObject laserCanon;
    public GameObject discoCanon;
    public GameObject baseTurret;
    public GameObject matDisco;

    public GameObject particlesLaserlvl1;
    public GameObject particlesLaserlvl2;
    public GameObject particlesLaserlvl3;

    [Header("Material")]
    private float timerEffect;
    private Material baseTurretShader;
     
    private Material shaderMatEmissive;
    private Material shaderMatEmissive1;
    private Material shaderMatEmissive2;
    private Material shaderMatEmissive3;
    private Material shaderMatEmissive4;

    private Material shaderMatLaser;

    [Header("LaserSoundOption")]
    public AudioSource source;
    public float laserSoundFadeTime;
    private bool laserSoundIsPlaying;

    [HideInInspector]
    public List<Enemy> targetList = new List<Enemy>();
    private GameObject bulletToShoot;
    private float fireCoolDown;
    private float[] laserMultiplier;
    private float[] laserCoolDown;

    #region Upgrade variable

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

    #endregion

    #endregion 

    private void Awake()
    {
        bulletToShoot = basicBullet;
        laserMultiplier = new float[numMaxTargets];
        laserCoolDown = new float[numMaxTargets];

        baseTurretShader = baseTurret.GetComponent<MeshRenderer>().materials[0];
        shaderMatEmissive = basicCanon.GetComponent<MeshRenderer>().materials[1];
        shaderMatEmissive1 = baseTurret.GetComponent<MeshRenderer>().materials[1];
        shaderMatEmissive2 = explosionCanon.GetComponent<MeshRenderer>().materials[1];
        shaderMatEmissive3 = laserCanon.GetComponent<MeshRenderer>().materials[1];
        shaderMatEmissive4 = matDisco.GetComponent<MeshRenderer>().materials[1];
        ResetLaser();
    }

    private void Start()
    {
        StartCoroutine(CreateTurretEffect());
    }

    void FixedUpdate()
    {
        targetList.RemoveAll(list_item => list_item == null);
        FindTargets();

        if (laserUpgrade > 0)
        {
            Laser();
        }
        else
        {
            MultiShoot();
        }
    }

    void FindTargets()
    {
        for (int i = 0; i < WaveSpawner.instance.enemyList.Count; i++)
        {
            if (targetList.Count < numMaxTargets)
            {
                if (WaveSpawner.instance.enemyList[i] != null)
                {
                    if (Vector3.Distance(transform.position, WaveSpawner.instance.enemyList[i].transform.position) < actualRange)
                    {
                        if (!targetList.Contains(WaveSpawner.instance.enemyList[i]))
                        {
                            targetList.Add(WaveSpawner.instance.enemyList[i]);
                        }
                    }
                }

            }
        }

        for (int i = 0; i < targetList.Count; i++)
        {
            if (targetList.Count > 0)
            {
                if (targetList[i] != null)
                {
                    if (Vector3.Distance(transform.position, targetList[i].transform.position) > actualRange)
                    {
                        targetList.Remove(targetList[i]);
                    }
                }
            }
        }
    }

    void MultiShoot()
    {
        if (targetList.Count > 0)
        {
            AimTarget();

            if (fireCoolDown <= 0f)
            {
                TurretSoundManager.I.Shoot(1);
                Fire(targetList[0]);
                fireCoolDown = 1f / actualFireRate;
            }
            fireCoolDown -= Time.deltaTime;
        }
    }
    void Laser()
    {
        AimTarget();

        for (int i = 0; i < actualNumOfCanon; i++)
        {
            if (targetList.Count > i)
            {
                if (!laserSoundIsPlaying)
                {
                    laserSoundIsPlaying = true;
                    StartCoroutine(StartLaserSound());
                }

                shaderMatLaser = laserLines[i].GetComponent<LineRenderer>().material;
                laserLines[i].enabled = true;
                laserLines[i].SetPosition(0, shootPoint.position);
                laserLines[i].SetPosition(1, targetList[i].transform.position);

                if (actualNumOfCanon > 1)
                {
                    laserMultiplier[i] += Time.deltaTime * (actualLaserFireRateMultiplier / actualMicroLaserDamageReduction);
                }
                else
                {
                    laserMultiplier[i] += Time.deltaTime * actualLaserFireRateMultiplier;
                }

                if (laserCoolDown[i] <= 0f)
                {
                    if (slowUpgrade == 0 && poisonUpgrade == 0)
                    {
                        shaderMatLaser.SetFloat("inputColorLaser", 0);
                        targetList[i].transform.GetChild(0).gameObject.SetActive(true);
                        targetList[i].transform.GetChild(1).gameObject.SetActive(false);
                        targetList[i].transform.GetChild(2).gameObject.SetActive(false);
                        targetList[i].transform.GetChild(3).gameObject.SetActive(false);
                    }
                    if (slowUpgrade > 0)
                    {
                        targetList[i].StartSlow(actualSlowForce, actualSlowDuration);

                        if (poisonUpgrade == 0)
                        {
                            shaderMatLaser.SetFloat("inputColorLaser", 1);
                            targetList[i].transform.GetChild(2).gameObject.SetActive(true);
                            targetList[i].transform.GetChild(0).gameObject.SetActive(false);
                            targetList[i].transform.GetChild(1).gameObject.SetActive(false);
                            targetList[i].transform.GetChild(3).gameObject.SetActive(false);
                        }
                    }
                    if (poisonUpgrade > 0)
                    {
                        targetList[i].Poison(actualpoisonDamage, actualPoisonDuration, actualPoisonTick);

                        if (slowUpgrade == 0)
                        {
                            shaderMatLaser.SetFloat("inputColorLaser", 2);
                            targetList[i].transform.GetChild(1).gameObject.SetActive(true);
                            targetList[i].transform.GetChild(0).gameObject.SetActive(false);
                            targetList[i].transform.GetChild(2).gameObject.SetActive(false);
                            targetList[i].transform.GetChild(3).gameObject.SetActive(false);
                        }
                    }
                    if (slowUpgrade > 0 && poisonUpgrade > 0)
                    {
                        shaderMatLaser.SetFloat("inputColorLaser", 3);
                        targetList[i].transform.GetChild(3).gameObject.SetActive(true);
                        targetList[i].transform.GetChild(0).gameObject.SetActive(false);
                        targetList[i].transform.GetChild(2).gameObject.SetActive(false);
                        targetList[i].transform.GetChild(1).gameObject.SetActive(false);
                    }

                    targetList[i].TakeDamage(actualDamage / actualLaserDamageReduction);

                    if (targetList[i].actualHealth <= 0)
                    {
                        laserMultiplier[i] = 1f;
                        laserCoolDown[i] = 0f;
                    }

                    laserCoolDown[i] = 1 / (actualFireRate * laserMultiplier[i]);
                }
                laserCoolDown[i] -= Time.deltaTime;
            }
            else if (laserLines[i].enabled)
            {
                if (laserSoundIsPlaying)
                {
                    laserSoundIsPlaying = false;
                    StartCoroutine(StopLaserSound());
                }
                laserLines[i].enabled = false;
                laserMultiplier[i] = 1f;
                laserCoolDown[i] = 0f;

                if (i <= targetList.Count)
                {
                    targetList[i].transform.GetChild(3).gameObject.SetActive(false);
                    targetList[i].transform.GetChild(0).gameObject.SetActive(false);
                    targetList[i].transform.GetChild(2).gameObject.SetActive(false);
                    targetList[i].transform.GetChild(1).gameObject.SetActive(false);
                }
            }
        }
    }
    void AimTarget()
    {
        if (targetList.Count > 0)
        {
            if (targetList[0] != null)
            {
                Vector3 dir = targetList[0].transform.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
                partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            }
        }
    }
    void Fire(Enemy target)
    {
        GameObject actualBullet = Instantiate(bulletToShoot, shootPoint.position, shootPoint.rotation);
        Bullet bulletScript = actualBullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.GetTarget(target.transform);
            bulletScript.GetDamage(actualDamage);
            bulletScript.GetSlowInfo(actualSlowForce, actualSlowDuration);
            bulletScript.GetPoisonInfo(actualpoisonDamage, actualPoisonDuration, actualPoisonTick);
            bulletScript.GetExplosiveInfo(actualExplosionRadius);
        }
    }

    public void GetNodeUpgrade(NodeSysteme node)
    {
        laserUpgrade = node.laserUpgrade;
        explosionUpgrade = node.explosionUpgrade;

        slowUpgrade = node.slowUpgrade;
        poisonUpgrade = node.poisonUpgrade;

        fireRateUpgrade = node.fireRateUpgrade;
        damageUpgrade = node.damageUpgrade;
        rangeUpgrade = node.rangeUpgrade;

        ApplyUpgrade();
    }
    public void ResetUpgrade()
    {
        laserUpgrade = 0;
        explosionUpgrade = 0;

        slowUpgrade = 0;
        poisonUpgrade = 0;

        fireRateUpgrade = 0;
        damageUpgrade = 0;
        rangeUpgrade = 0;

        ApplyUpgrade();
    }

    void ApplyUpgrade()
    {
        #region Stats boost
        switch (fireRateUpgrade)
        {
            case 0:
                actualFireRate = startFireRate;
                break;
            case 1:
                actualFireRate = fireRateBonus[0];
                break;
            case 2:
                actualFireRate = fireRateBonus[1];
                break;
            case 3:
                actualFireRate = fireRateBonus[2];
                break;
            case 4:
                actualFireRate = fireRateBonus[3];
                break;
        }

        switch (damageUpgrade)
        {
            case 0:
                actualDamage = startDamage;
                break;
            case 1:
                actualDamage = damageBonus[0];
                break;
            case 2:
                actualDamage = damageBonus[1];
                break;
            case 3:
                actualDamage = damageBonus[2];
                break;
            case 4:
                actualDamage = damageBonus[3];
                break;
        }

        switch (rangeUpgrade)
        {
            case 0:
                actualRange = startRange;
                break;
            case 1:
                actualRange = rangeBonus[0];
                break;
            case 2:
                actualRange = rangeBonus[1];
                break;
            case 3:
                actualRange = rangeBonus[2];
                break;
            case 4:
                actualRange = rangeBonus[3];
                break;
        }
        #endregion

        #region Negatif effect boost
        switch (slowUpgrade)
        {
            case 0:
                actualSlowForce = 0;
                actualSlowDuration = 0;
                break;
            case 1:
                actualSlowForce = slowForceBonus[0];
                actualSlowDuration = slowDurationBonus[0];
                break;
            case 2:
                actualSlowForce = slowForceBonus[1];
                actualSlowDuration = slowDurationBonus[1];
                break;
            case 3:
                actualSlowForce = slowForceBonus[2];
                actualSlowDuration = slowDurationBonus[2];
                break;
            case 4:
                actualSlowForce = slowForceBonus[3];
                actualSlowDuration = slowDurationBonus[3];
                break;
        }

        switch (poisonUpgrade)
        {
            case 0:
                actualpoisonDamage = 0f;
                actualPoisonDuration = 0f;
                actualPoisonTick = 0f;
                break;
            case 1:
                actualpoisonDamage = poisonDamageBonus[0];
                actualPoisonDuration = poisonDurationBonus[0];
                actualPoisonTick = poisonTickBonus[0];
                break;
            case 2:
                actualpoisonDamage = poisonDamageBonus[1];
                actualPoisonDuration = poisonDurationBonus[1];
                actualPoisonTick = poisonTickBonus[1];
                break;
            case 3:
                actualpoisonDamage = poisonDamageBonus[2];
                actualPoisonDuration = poisonDurationBonus[2];
                actualPoisonTick = poisonTickBonus[2];
                break;
            case 4:
                actualpoisonDamage = poisonDamageBonus[3];
                actualPoisonDuration = poisonDurationBonus[3];
                actualPoisonTick = poisonTickBonus[3];
                break;
        }
        #endregion

        #region Shoot type boost
        switch (explosionUpgrade)
        {
            case 0:
                bulletToShoot = basicBullet;
                actualExplosionRadius = 0f;
                actualNumOfCanon = 1;
                break;
            case 1:
                bulletToShoot = explosiveBullet;
                actualExplosionRadius = explosionRadiusBonus[0];
                actualNumOfCanon = numOfCanonBonus[0];
                break;
            case 2:
                bulletToShoot = explosiveBullet;
                actualExplosionRadius = explosionRadiusBonus[1];
                actualNumOfCanon = numOfCanonBonus[1];
                break;
            case 3:
                bulletToShoot = explosiveBullet;
                actualExplosionRadius = explosionRadiusBonus[2];
                actualNumOfCanon = numOfCanonBonus[2];
                break;
            case 4:
                bulletToShoot = explosiveBullet;
                actualExplosionRadius = explosionRadiusBonus[3];
                actualNumOfCanon = numOfCanonBonus[3];
                break;
        }

        switch (laserUpgrade)
        {
            case 0:
                actualLaserDamageReduction = 0;
                actualLaserFireRateMultiplier = 0;
                actualMicroLaserDamageReduction = 0;
                ResetLaser();
                break;
            case 1:
                actualLaserDamageReduction = laserDamageReductionBonus[0];
                actualLaserFireRateMultiplier = laserFirerateMultiplierBonus[0];
                actualMicroLaserDamageReduction = microLaserDamageReductionBonus[0];
                break;
            case 2:
                actualLaserDamageReduction = laserDamageReductionBonus[1];
                actualLaserFireRateMultiplier = laserFirerateMultiplierBonus[1];
                actualMicroLaserDamageReduction = microLaserDamageReductionBonus[1];
                break;
            case 3:
                actualLaserDamageReduction = laserDamageReductionBonus[2];
                actualLaserFireRateMultiplier = laserFirerateMultiplierBonus[2];
                actualMicroLaserDamageReduction = microLaserDamageReductionBonus[2];
                break;
            case 4:
                actualLaserDamageReduction = laserDamageReductionBonus[3];
                actualLaserFireRateMultiplier = laserFirerateMultiplierBonus[3];
                actualMicroLaserDamageReduction = microLaserDamageReductionBonus[3];
                break;
        }
        #endregion

        SetEffect();
    }

    void ResetLaser()
    {
        if (laserUpgrade < 1)
        {
            for (int i = 0; i < laserLines.Length; i++)
            {
                laserLines[i].enabled = false;
                laserMultiplier[i] = 1f;
                laserCoolDown[i] = 0f;
            }
        }
    }

    void SetEffect()
    {
        #region Canon
        if (explosionUpgrade > 0 && laserUpgrade == 0)
        {
            explosionCanon.SetActive(true);

            laserCanon.SetActive(false);
            discoCanon.SetActive(false);
            basicCanon.SetActive(false);

        }
        else if (laserUpgrade > 0 && explosionUpgrade == 0)
        {
            laserCanon.SetActive(true);

            discoCanon.SetActive(false);
            basicCanon.SetActive(false);
            explosionCanon.SetActive(false);

            if (laserUpgrade == 1)
            { 
                particlesLaserlvl1.SetActive(true);
                particlesLaserlvl2.SetActive(false);
                particlesLaserlvl3.SetActive(false);
            }
        }
        else if (laserUpgrade > 0 && explosionUpgrade > 0)
        {
            discoCanon.SetActive(true);

            explosionCanon.SetActive(false);
            laserCanon.SetActive(false);
            basicCanon.SetActive(false);
        }
        else
        {
            basicCanon.SetActive(true);

            explosionCanon.SetActive(false);
            laserCanon.SetActive(false);
            discoCanon.SetActive(false);
        }
        #endregion

        #region Negatif effect
        if (slowUpgrade > 0 && poisonUpgrade == 0)
        {
            shaderMatEmissive.SetFloat("inputColorEmissive", 1);
            shaderMatEmissive1.SetFloat("inputColorEmissive", 1);
            shaderMatEmissive2.SetFloat("inputColorEmissive", 1);
            shaderMatEmissive3.SetFloat("inputColorEmissive", 1);
            shaderMatEmissive4.SetFloat("inputColorEmissive", 1);
        }
        else if (poisonUpgrade > 0 && slowUpgrade == 0)
        {
            shaderMatEmissive.SetFloat("inputColorEmissive", 2);
            shaderMatEmissive1.SetFloat("inputColorEmissive", 2);
            shaderMatEmissive2.SetFloat("inputColorEmissive", 2);
            shaderMatEmissive3.SetFloat("inputColorEmissive", 2);
            shaderMatEmissive4.SetFloat("inputColorEmissive", 2);
        }
        else if (poisonUpgrade > 0 && slowUpgrade > 0)
        {
            shaderMatEmissive.SetFloat("inputColorEmissive", 3);
            shaderMatEmissive1.SetFloat("inputColorEmissive", 3);
            shaderMatEmissive2.SetFloat("inputColorEmissive", 3);
            shaderMatEmissive3.SetFloat("inputColorEmissive", 3);
            shaderMatEmissive4.SetFloat("inputColorEmissive", 3);
        }
        else
        {
            shaderMatEmissive.SetFloat("inputColorEmissive", 0);
            shaderMatEmissive1.SetFloat("inputColorEmissive", 0);
            shaderMatEmissive2.SetFloat("inputColorEmissive", 0);
            shaderMatEmissive3.SetFloat("inputColorEmissive", 0);
            shaderMatEmissive4.SetFloat("inputColorEmissive", 0);
        }
        #endregion
    }

    private IEnumerator StartLaserSound()
    {
        source.volume = 0;
        for (float i = 0; i < laserSoundFadeTime; i += Time.deltaTime)
        {
            source.volume = i / laserSoundFadeTime;
            yield return new WaitForEndOfFrame();
        }
        source.volume = 1;
    }
    private IEnumerator StopLaserSound()
    {
        source.volume = 1;
        for (float i = 0; i < laserSoundFadeTime; i += Time.deltaTime)
        {
            source.volume = 1 - (i / laserSoundFadeTime);
            yield return new WaitForEndOfFrame();
        }
        source.volume = 0;
    }

    public IEnumerator CreateTurretEffect()
    {
        timerEffect = 0;
        while (timerEffect < 5)
        {
            timerEffect += Time.deltaTime;
            baseTurretShader.SetFloat("_HoloToText", timerEffect);
        }
        yield return new WaitForEndOfFrame();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, actualRange);
    }
}
