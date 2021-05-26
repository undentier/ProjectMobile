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
    private GameObject turretPreviToBuild;
    [Space]
    public bool isDraggingTurret;
    private Touch touch;
    public GameObject currentPrevisualisationObject;
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

    public void SetTurretToBuild(GameObject turret, GameObject turretPrevi)
    {
        turretToBuild = turret;
        turretPreviToBuild = turretPrevi;
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
        TouchDetection.UpdateCurrentNode();
        currentPrevisualisationObject = Instantiate(turretPreviToBuild, TouchDetection.currentlyHoveredNode.transform.position, TouchDetection.currentlyHoveredNode.transform.rotation);
        isDraggingTurret = true;
    }

    void DragUpdate()
    {
        if (isDraggingTurret)
        {
            TouchDetection.UpdateCurrentNode();
            currentPrevisualisationObject.transform.position = TouchDetection.currentlyHoveredNode.transform.position;
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
    }

    public void CancelBuild()
    {
        TouchDetection.currentlyHoveredNode = NodeManager.allNodes[0];
        Destroy(currentPrevisualisationObject);
        isDraggingTurret = false;
        wantCancel = false;
    }


    void SlideDetection()
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
                        }
                    }
                }
            }
            else if (Input.GetButtonDown("LeftClick"))
            {
                TouchDetection.UpdateCurrentNode();
                TouchDetection.currentlyHoveredNode.SlideTurret();
            }
            else if (Input.GetButtonUp("LeftClick"))
            {
                if (SlideManager.instance.isSliding)
                {
                    SlideManager.instance.EndSlide();
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
        }
    }
}
