using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> levels;
    int starTotal;
    int starCount;
    public GameObject totalScoreText;
    // Start is called before the first frame update
    void Start()
    {
        StarNumber();
    }

    // Update is called once per frame
    void Update()
    {
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
            StarNumber();
//        }
    }

    void StarNumber()
    {
        starTotal = 0;
        foreach(GameObject level in levels)
        {
            starCount = level.GetComponent<LevelDisplay>().score;
            starTotal += starCount;
        }
        Bruh();
    }
    void Bruh()
    {
        totalScoreText.GetComponent<Text>().text = "Nombre d'étoiles:" + starTotal.ToString();
    }
}
