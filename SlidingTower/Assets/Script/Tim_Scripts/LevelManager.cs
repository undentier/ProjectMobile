using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public List<LevelDisplay> levels;
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
        for (int i = 0; i < levels.Count; i++)
        {
            starCount = levels[i].score;
            starTotal += starCount;
        }
        Write();
    }
    void Write()
    {
        totalScoreText.GetComponent<Text>().text = "Nombre d'étoiles:" + starTotal.ToString();
    }
}
