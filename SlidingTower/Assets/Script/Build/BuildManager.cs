﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    [Header ("Basique Turret")]
    public GameObject basiqueTurretPrefab;
    public GameObject basiqueTurretPrevisualizePrefab;

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
    [HideInInspector] public bool isDraggingTurret;
    private Touch touch;
    private GameObject currentPrevisualisationObject;
    private NodeSysteme selectedNodeToSlide;

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
    }

    public void StartDragTurret()
    {
        isDraggingTurret = true;
        TouchDetection.UpdateCurrentNode();
        currentPrevisualisationObject = Instantiate(turretPreviToBuild, TouchDetection.currentlyHoveredNode.transform.position, TouchDetection.currentlyHoveredNode.transform.rotation);
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
                    CreateTurret();
                }
            }
            if (Input.GetButtonUp("LeftClick"))
            {
                CreateTurret();
            }
        }
    }

    private void CreateTurret()
    {
        TouchDetection.UpdateCurrentNode();
        TouchDetection.currentlyHoveredNode.CreateTurret();
        isDraggingTurret = false;
        Destroy(currentPrevisualisationObject);
    }

    void SlideDetection()
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
                        Debug.Log("EndSlide");
                        SlideManager.instance.EndSlide();
                    }
                }
            }
        }

        if (Input.GetButtonDown("LeftClick"))
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
