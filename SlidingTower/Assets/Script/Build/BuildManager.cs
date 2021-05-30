using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    [Header ("Basique Turret")]
    public GameObject basiqueTurretPrefab;
    public GameObject basiqueTurretPrevisualizePrefab;
    public GameObject blockPrevisualizePrefab; 

    [Header ("Boost block")]
    public GameObject fireRateBlock;
    public GameObject damageBlock;
    public GameObject rangeBlock;

    [Space]
    public GameObject poisonBlock;
    public GameObject slowBlock;

    [Space]
    public GameObject explosionBlock;
    public GameObject laserBlock;

    private GameObject turretToBuild;
    public int turretPlaceSoundIndex;
    private GameObject turretPreviToBuild;
    public bool isTurretPreview;
    [Space]
    public bool isDraggingTurret;
    private Touch touch;
    public GameObject currentPrevisualisationObject;
    private Vector3 lastPreviPos;
    public bool wantCancel;

    [Header("Unity setup")]
    public GameObject bin;

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    public void SetTurretToBuild(GameObject turret, GameObject turretPrevi, int _turretPlaceSoundIndex, bool isTurret)
    {
        turretToBuild = turret;
        turretPreviToBuild = turretPrevi;
        turretPlaceSoundIndex = _turretPlaceSoundIndex;
        isTurretPreview = isTurret;
    }

    public void Update()
    {
        SlideDetection();
        DragUpdate();

        if (isDraggingTurret)
        {
            bin.SetActive(true);

            if (wantCancel)
            {
                if (currentPrevisualisationObject != null)
                {
                    currentPrevisualisationObject.SetActive(false);

                    if (isTurretPreview)
                    {
                        turretPreviToBuild.transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                StartCoroutine(PreviewCooldown());
            }
        }
        else
        {
            bin.SetActive(false);
        }
    }

    public void StartDragTurret()
    {
        TouchDetection.currentlyHoveredNode = NodeManager.allNodes[0];
        currentPrevisualisationObject = Instantiate(turretPreviToBuild, TouchDetection.currentlyHoveredNode.transform.position, TouchDetection.currentlyHoveredNode.transform.rotation);
        isDraggingTurret = true;
    }

    void DragUpdate()
    {
        if (isDraggingTurret)
        {
            TouchDetection.UpdateCurrentNode();

            currentPrevisualisationObject.transform.position = TouchDetection.currentlyHoveredNode.transform.position;

            if (lastPreviPos != currentPrevisualisationObject.transform.position)
            {
                TurretSoundManager.I.PlayPreviSlide(1);
                lastPreviPos = currentPrevisualisationObject.transform.position;
            }
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Ended)
                {

                    if (!wantCancel)
                    {
                        CreateTurret();
                    }
                    else
                    {
                        CancelBuild();
                    }
                }
            }
            else if (Input.GetButtonUp("LeftClick"))
            {
                if (!wantCancel)
                {
                    CreateTurret();
                }
                else
                {
                    CancelBuild();
                }
            }
        }
    }

    private void CreateTurret()
    {
        isDraggingTurret = false;
        TouchDetection.UpdateCurrentNode();
        TouchDetection.currentlyHoveredNode.CreateTurret();
        TouchDetection.currentlyHoveredNode = NodeManager.allNodes[0];
        Destroy(currentPrevisualisationObject);
        TouchDetection.currentlyHoveredNode = null;
    }

    public void CancelBuild()
    {
        TouchDetection.currentlyHoveredNode = NodeManager.allNodes[0];
        Destroy(currentPrevisualisationObject);
        isDraggingTurret = false;
        wantCancel = false;
        TouchDetection.currentlyHoveredNode = null;
        PlayerSoundManager.I.UnSelectTurret(1);
    }


    void SlideDetection()
    {
        if (UIManager.instance.gamePause == false && UIManager.instance.enemyPreviewActive == false)
        {
            if (!isDraggingTurret)
            {
                if (Input.touchCount > 0)
                {
                    foreach (Touch touch in Input.touches)
                    {
                        if (touch.phase == TouchPhase.Began)
                        {
                            TouchDetection.UpdateCurrentNode();
                            TouchDetection.currentlyHoveredNode.SlideTurret();
                        }
                        else if (touch.phase == TouchPhase.Ended)
                        {
                            if (SlideManager.instance.isSliding)
                            {
                                SlideManager.instance.EndSlide();

                                TouchDetection.currentlyHoveredNode = null;
                            }
                        }
                    }
                }
                else if (Input.GetButtonDown("LeftClick"))
                {
                    TouchDetection.UpdateCurrentNode();

                    if (TouchDetection.currentlyHoveredNode != null)
                    {
                        TouchDetection.currentlyHoveredNode.SlideTurret();
                    }
                }
                else if (Input.GetButtonUp("LeftClick"))
                {
                    if (SlideManager.instance.isSliding)
                    {
                        SlideManager.instance.EndSlide();

                        TouchDetection.currentlyHoveredNode = null;
                    }
                }
            }
        }
    }


    public void EnterCancel()
    {
        wantCancel = true;
    }
    public void ExitCancel()
    {
        StartCoroutine(MicroCooldown());
    }

    IEnumerator MicroCooldown()
    {
        yield return new WaitForEndOfFrame();
        wantCancel = false;
    }

    IEnumerator PreviewCooldown()
    {
        yield return new WaitForEndOfFrame();
        if (currentPrevisualisationObject != null)
        {
            currentPrevisualisationObject.SetActive(true);

            if (isTurretPreview)
            {
                turretPreviToBuild.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
