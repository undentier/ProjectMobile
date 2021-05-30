using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostBlock : MonoBehaviour
{
    [Header("Stats boost")]
    public int fireRateBoost;
    public int damageBoost;
    public int rangeBoost;

    [Header ("Negative effect")]
    public int slowValue;
    public int poisonValue;

    [Header ("Shooting type")]
    public int lazer;
    public int explosion;

    private float timerEffect;
    private Material boostBlockShader1;
    private Material boostBlockShader2;
    public GameObject boostBlock;


    private void Awake()
    {
        //boostBlockShader1 = boostBlock.GetComponent<MeshRenderer>().materials[0];
        //boostBlockShader2 = boostBlock.GetComponent<MeshRenderer>().materials[1];
    }

    private void Start()
    {
        //StartCoroutine(CreateBlockEffect());
    }

    public IEnumerator CreateBlockEffect()
    {
        timerEffect = 0;
        while (timerEffect < 10)
        {
            timerEffect += Time.deltaTime * 5f;
            boostBlockShader1.SetFloat("_HoloToText", timerEffect);
            boostBlockShader2.SetFloat("_HoloToEmi", timerEffect);
            yield return new WaitForEndOfFrame();
        }
    }
}
