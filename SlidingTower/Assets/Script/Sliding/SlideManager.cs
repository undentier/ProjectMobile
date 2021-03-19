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
    public NodeSysteme targetNode;
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

    void Update()
    {
        if (isSliding && canSlide)
        {
            Debug.Log("switch");
            if (selectedObj != null)
            {
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

        SlideMouvement();
    }

    private Vector3 MousePos()
    {
        mZCoord = Camera.main.WorldToScreenPoint(startNode.transform.position).z;
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void StartSlide(NodeSysteme _startNode)
    {
        if (!isSliding)
        {
            isSliding = true;
            startNode = _startNode;
            nearNodes = _startNode.closestNodes;
            selectedObj = _startNode.objBuild;
        }
    }

    public void EndSlide()
    {
        if (isSliding)
        {
            isSliding = false;
            startNode = null;
            selectedObj = null;
            nearNodes = null;
        }
    }

    void SwitchInformation(NodeSysteme _nextStartNode)
    {
        startNode.objBuild = null;
        startNode = _nextStartNode;
        startNode.objBuild = selectedObj;
        nearNodes = startNode.closestNodes;
    }

    void SlideMouvement()
    {
        if (targetNode != null)
        {
            canSlide = false;
            Vector3 dir = targetNode.transform.position - selectedObj.transform.position;
            float distancePerFrame = Time.deltaTime * slidingSpeed;
 
            if (dir.magnitude <= distancePerFrame)
            {
                Debug.Log("j'arrete");
                selectedObj.transform.Translate(Vector3.zero);
                targetNode = null;
                canSlide = true;
            }
            else
            {
                selectedObj.transform.Translate(dir.normalized * distancePerFrame, Space.World);
                Debug.Log("je déplace");
            }
        }
    }
}
