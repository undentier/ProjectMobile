using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public List<LevelsSO> levels;
    int starTotal;
    public GameObject totalScoreText;
    public LevelDisplay levelButton;
    public RectTransform levelSelectionPanelTransform;
    // Start is called before the first frame update
    void Start()
    {
        StarNumber();
        InitializeSelectionPanel();
    }

    // Update is called once per frame
    void Update()
    {
 /*      if (Input.GetKeyDown(KeyCode.Space))
        {

        }*/
            StarNumber();
    }

    void StarNumber()
    {
        starTotal = 0;
        for (int i = 0; i < levels.Count; i++)
        {
            starTotal += levels[i].score;
        }
        Write();
    }
    void Write()
    {
        totalScoreText.GetComponent<Text>().text = "Nombre d'étoiles:" + starTotal.ToString();
    }
    void InitializeSelectionPanel()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            LevelDisplay newleveldisplay = Instantiate(levelButton, levelSelectionPanelTransform);
            newleveldisplay.level = levels[i];
        }
        levelSelectionPanelTransform.sizeDelta = new Vector2((levels.Count-1)*(levelButton.GetComponent<RectTransform>().sizeDelta.x+levelSelectionPanelTransform.GetComponent<HorizontalLayoutGroup>().spacing), levelSelectionPanelTransform.sizeDelta.y);
        levelSelectionPanelTransform.GetComponent<ScrollRect>().normalizedPosition = Vector2.zero;
    }
}
