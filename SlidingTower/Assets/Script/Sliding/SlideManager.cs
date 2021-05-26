using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideManager : MonoBehaviour
{
    public static SlideManager instance;

    #region Variable
    [Header ("Mouvement stats")]
    public float slidingSpeed;
    [Space]
    public float offSetUp;
    public float offSetRight;
    public float offSetDown;
    public float offSetLeft;

    [HideInInspector]
    public bool isSliding;
    [HideInInspector]
    public List<NodeSysteme> nearNodes = new List<NodeSysteme>();
    [HideInInspector]
    public NodeSysteme startNode;
    [HideInInspector]
    public GameObject selectedObj;

    private float mZCoord;
    private bool canSlide = true;

    private NodeSysteme targetNode;
    private GameObject objToMove;

    private BoostBlock boostBlockScript;
    private Turret turretScript;
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        SlideDetection();

        SlideMouvement();
    }

    void SlideMouvement()
    {
        if (targetNode != null)
        {
            canSlide = false;
            Vector3 dir = targetNode.transform.position - objToMove.transform.position;
            float distancePerFrame = Time.deltaTime * slidingSpeed;

            if (dir.magnitude <= 0.1f)
            {
                dir = Vector3.zero;
                objToMove.transform.Translate(Vector3.zero);
                targetNode = null;
                objToMove = null;
                canSlide = true;
            }
            else
            {
                objToMove.transform.position = Vector3.MoveTowards(objToMove.transform.position, targetNode.transform.position, slidingSpeed * Time.deltaTime);
            }
        }
    }
    void SlideDetection()
    {
        if (isSliding && canSlide)
        {
            if (selectedObj != null)
            {
                objToMove = selectedObj;

                #region Up direction
                if (MousePos().z >= startNode.transform.position.z + offSetUp)
                {
                    if (nearNodes[0] != null && nearNodes[0].objBuild == null)
                    {
                        targetNode = nearNodes[0];
                        SwitchInformation(nearNodes[0]);
                    }
                }
                #endregion

                #region Right direction
                else if (MousePos().x >= startNode.transform.position.x + offSetRight)
                {
                    if (nearNodes[1] != null && nearNodes[1].objBuild == null)
                    {
                        targetNode = nearNodes[1];
                        SwitchInformation(nearNodes[1]);
                    }
                }
                #endregion

                #region Down direction
                else if (MousePos().z <= startNode.transform.position.z - offSetDown)
                {
                    if (nearNodes[2] != null && nearNodes[2].objBuild == null)
                    {
                        targetNode = nearNodes[2];
                        SwitchInformation(nearNodes[2]);
                    }
                }
                #endregion

                #region Left direction
                else if (MousePos().x <= startNode.transform.position.x - offSetLeft)
                {
                    if (nearNodes[3] != null && nearNodes[3].objBuild == null)
                    {
                        targetNode = nearNodes[3];
                        SwitchInformation(nearNodes[3]);
                    }
                }
                #endregion
            }
        }
    }


    public void StartSlide(NodeSysteme _startNode)
    {
        if (!isSliding)
        {
            isSliding = true;
            startNode = _startNode;
            nearNodes = _startNode.closestNodes;
            selectedObj = _startNode.objBuild;

            selectedObj.GetComponent<Animator>().SetBool("Selected", true);

            ObjectDetection();
        }
    }
    public void EndSlide()
    {
        if (isSliding)
        {
            selectedObj.GetComponent<Animator>().SetBool("Selected", false);

            startNode.ObjTypeDetection();

            if (boostBlockScript != null)
            {
                startNode.GetUpgrade(boostBlockScript);
                startNode.UpgradeNeighbour(boostBlockScript);
                startNode.TurretNeighbour();

                startNode.GetNeighbourObjs();
                startNode.SetAllNeighbourObjcs();
            }
            else if (turretScript != null)
            {
                turretScript.GetNodeUpgrade(startNode);

                startNode.GetNeighbourObjs();
                startNode.SetAllNeighbourObjcs();
            }

            isSliding = false;
            startNode = null;
            selectedObj = null;
            nearNodes = null;

            ObjectDetection();
        }
    }

    void SwitchInformation(NodeSysteme _nextStartNode)
    {
        startNode.objBuild = null;

        startNode = _nextStartNode;
        startNode.objBuild = selectedObj;
        nearNodes = startNode.closestNodes;
    }

    private Vector3 MousePos()
    {
        mZCoord = Camera.main.WorldToScreenPoint(startNode.transform.position).z;
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void ObjectDetection()
    {
        if (selectedObj == null)
        {
            boostBlockScript = null;
            turretScript = null;
        }
        else if (selectedObj.layer == 9)
        {
            boostBlockScript = selectedObj.GetComponent<BoostBlock>();
        }
        else if (selectedObj.layer == 11)
        {
            turretScript = selectedObj.GetComponent<Turret>();
        }
    }
}
