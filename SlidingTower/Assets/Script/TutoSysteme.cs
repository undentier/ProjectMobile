using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoSysteme : MonoBehaviour
{
    public static TutoSysteme instance;

    [Header ("Move Tuto")]
    public GameObject handMoveObj;
    public GameObject turretMoveObj;
    [Space]
    public Animator handMoveAnim;
    public Animator turretMoveAnim;
    private bool lockMoveAnim;

    [Header("Drag and drop Tuto")]
    public GameObject handDragObj;
    public GameObject blackBackGroundObj;
    public Animator blackBackGroundAnim;
    private int dragTutoState;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        blackBackGroundObj.SetActive(false);
        handDragObj.SetActive(false);
    }

    
    void Update()
    {
        if (!lockMoveAnim)
        {
            if (SlideManager.instance.isSliding)
            {
                lockMoveAnim = true;
                handMoveObj.SetActive(false);
                turretMoveObj.SetActive(false);

                WavePanel.instance.isTuto = false;
                WavePanel.instance.isFirstWave = false;
                WavePanel.instance.startWaveButtonAnim.SetInteger("startState", 2);
            }
        }

        if (dragTutoState == 0)
        {
            if (WavePanel.instance.isBuildMode && WavePanel.instance.canBuildTurretIndex > 1)
            {
                Debug.Log("je rentre");
                dragTutoState = 1;
                handDragObj.SetActive(true);
                //blackBackGroundObj.SetActive(true);
            }
        }
        else if (dragTutoState == 1)
        {
            if (LifeManager.lifeInstance.buildToken < 1)
            {
                dragTutoState = 2;
                handDragObj.SetActive(false);
                blackBackGroundObj.SetActive(false);
            }
        }
    }
}
