using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideManager : MonoBehaviour
{
    public static SlideManager instance;
    public bool isSliding;

    public List<NodeSysteme> nearNodes = new List<NodeSysteme>();
    public NodeSysteme startNode;
    public GameObject selectedObj;

    public float offSetUp;
    public float offSetRight;
    public float offSetDown;
    public float offSetLeft;

    private float mZCoord;

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
        if (isSliding)
        {
            if (selectedObj != null)
            {
                if (MousePos().z >= startNode.transform.position.z + offSetUp)
                {
                    if (nearNodes[0] != null && nearNodes[0].objBuild == null)
                    {
                        Debug.Log("Up");
                        selectedObj.transform.position = (nearNodes[0].transform.position);
                        SwitchInformation(nearNodes[0]);
                    }
                }

                if (MousePos().x >= startNode.transform.position.x + offSetRight)
                {
                    if (nearNodes[1] != null && nearNodes[1].objBuild == null)
                    {
                        Debug.Log("Right");
                        selectedObj.transform.position = (nearNodes[1].transform.position);
                        SwitchInformation(nearNodes[1]);
                    }
                }

                if (MousePos().z <= startNode.transform.position.z - offSetDown)
                {
                    if (nearNodes[2] != null && nearNodes[2].objBuild == null)
                    {
                        Debug.Log("Down");
                        selectedObj.transform.position = (nearNodes[2].transform.position);
                        SwitchInformation(nearNodes[2]);
                    }
                }

                if (MousePos().x <= startNode.transform.position.x - offSetLeft)
                {
                    if (nearNodes[3] != null && nearNodes[3].objBuild == null)
                    {
                        Debug.Log("Left");
                        selectedObj.transform.position = (nearNodes[3].transform.position);
                        SwitchInformation(nearNodes[3]);
                    }
                }
            }
        }
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
}
