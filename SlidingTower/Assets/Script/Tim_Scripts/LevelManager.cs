using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public List<LevelsSO> levelsChapter1;
    public List<LevelsSO> levelsChapter2;
    public List<LevelsSO> levelsChapter3;
    private List<LevelsSO> currentChapterLevels;
    int starTotal;
    public GameObject totalScoreText;
    public LevelDisplay levelButton;
    public RectTransform levelSelectionPanelTransform;
    public List<LevelDisplay> levelDisplays;
    public Button chapter2Button;
    public Button chapter3Button;
    int chapterSelected;
    // Start is called before the first frame update
    void Start()
    {
        levelDisplays = new List<LevelDisplay>();
        StarNumber();
        //InitializeSelectionPanel();
    }

    // Update is called once per frame
    void Update()
    {
 /*      if (Input.GetKeyDown(KeyCode.Space))
        {

        }*/
            StarNumber();
        UnlockChapters();
    }

    void StarNumber()
    {
        starTotal = 0;
        for (int i = 0; i < levelsChapter1.Count; i++)
        {
            starTotal += levelsChapter1[i].score;
        }
        for (int j = 0; j < levelsChapter2.Count; j++)
        {
            starTotal += levelsChapter2[j].score;
        }
        for (int k = 0; k < levelsChapter3.Count; k++)
        {
            starTotal += levelsChapter3[k].score;
        }
        Write();
    }
    void Write()
    {
        totalScoreText.GetComponent<Text>().text = "Nombre d'étoiles: " + starTotal.ToString();
    }
    public void InitializeSelectionPanel(int chapter)
    {
        levelDisplays.Clear();
        while (levelSelectionPanelTransform.childCount != 0)
        {
            DestroyImmediate(levelSelectionPanelTransform.GetChild(0).gameObject);
        }
        chapterSelected = chapter;
        switch (chapter)
        {
            case 0:
                currentChapterLevels = levelsChapter1;
                break;
            case 1:
                currentChapterLevels = levelsChapter2;
                break;
            case 2:
                currentChapterLevels = levelsChapter3;
                break;
        }
        for (int i = 0; i < currentChapterLevels.Count; i++)
        {
            LevelDisplay newleveldisplay = Instantiate(levelButton, levelSelectionPanelTransform);
            newleveldisplay.level = currentChapterLevels[i];
            levelDisplays.Add(newleveldisplay);
        }
        levelSelectionPanelTransform.sizeDelta = new Vector2((currentChapterLevels.Count-1)*(levelButton.GetComponent<RectTransform>().sizeDelta.x+levelSelectionPanelTransform.GetComponent<HorizontalLayoutGroup>().spacing), levelSelectionPanelTransform.sizeDelta.y);
        levelSelectionPanelTransform.GetComponent<ScrollRect>().normalizedPosition = Vector2.zero;
    }
  /*  public void ClearList()
    {
        levelDisplays.Clear();
        for (int i = 0; i < levelSelectionPanelTransform.childCount; i++)
        {
            DestroyImmediate(levelSelectionPanelTransform.GetChild(0).gameObject);
        }
    }*/
    
    void UnlockChapters()
    {
        if (starTotal > 7)
        {
            chapter2Button.interactable = true;
            if (starTotal > 14)
            {
                chapter3Button.interactable = true;
            }
        }
    }
}
