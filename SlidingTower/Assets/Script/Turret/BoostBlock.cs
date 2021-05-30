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
    private Material boostBlockIcone;
    public GameObject boostBlock;
    public GameObject icone;


    private void Awake()
    {
        boostBlockShader1 = boostBlock.GetComponent<MeshRenderer>().materials[0];
        boostBlockShader2 = boostBlock.GetComponent<MeshRenderer>().materials[1];
        boostBlockIcone = icone.GetComponent<SpriteRenderer>().material;
    }

    private void Start()
    {
        StartCoroutine(CreateBlockEffect());
    }

    public IEnumerator CreateBlockEffect()
    {
        timerEffect = 0;
        while (timerEffect < 35)
        {
            timerEffect += Time.deltaTime * 25f;
            boostBlockShader1.SetFloat("_HoloToText", timerEffect);
            boostBlockShader2.SetFloat("_HoloToEmi", timerEffect);
            boostBlockIcone.SetFloat("_HoloToEmi", timerEffect);
            yield return new WaitForEndOfFrame();
        }
    }
}
