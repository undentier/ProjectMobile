using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingManager : MonoBehaviour
{
    public static SlidingManager slidingInstance;

    [Header ("Don't fill this")]
    public GameObject startNode;
    public GameObject selectedTower;
    public List<GameObject> nearNode = new List<GameObject>();
    public bool isSliding;
    public Vector3 offset;

    private float mZCoord;

    public void InfoStartNode(GameObject _startNode, GameObject _selectedTower,List<GameObject> _nearNode)
    {
        startNode = _startNode;
        selectedTower = _selectedTower;
        nearNode = _nearNode;
    }

    private void Awake()
    {
        if (slidingInstance != null)
        {
            Destroy(gameObject);
        }
        slidingInstance = this;
    }

    private void Update()
    {
        if (selectedTower != null)
        {
            selectedTower.GetComponent<Animator>().SetBool("Selected", true);

            if (MousePos().z >= startNode.transform.position.z + 2f)
            {
                if (nearNode[0] != null)
                {
                    if (nearNode[0].gameObject.transform.GetComponent<Node>().turret == null)
                    {
                        selectedTower.transform.position = (nearNode[0].transform.position + offset);
                        nearNode[0].gameObject.transform.GetComponent<Node>().turret = selectedTower;
                        startNode.GetComponent<Node>().turret = null;

                        InfoStartNode(nearNode[0], selectedTower, nearNode[0].GetComponent<Node>().hitNode);
                        startNode.GetComponent<Node>().ShareBoost();
                    }
                }
            }

            if (MousePos().x >= startNode.transform.position.x + 2f)
            {
                if (nearNode[1] != null)
                {
                    if (nearNode[1].gameObject.transform.GetComponent<Node>().turret == null)
                    {
                        selectedTower.transform.position = (nearNode[1].transform.position + offset);
                        startNode.GetComponent<Node>().turret = null;
                        nearNode[1].gameObject.transform.GetComponent<Node>().turret = selectedTower;

                        InfoStartNode(nearNode[1], selectedTower, nearNode[1].GetComponent<Node>().hitNode);
                        startNode.GetComponent<Node>().ShareBoost();
                    }
                }
            }

            if (MousePos().z <= startNode.transform.position.z - 2f)
            {
                if (nearNode[2] != null)
                {
                    if (nearNode[2].gameObject.transform.GetComponent<Node>().turret == null)
                    {
                        selectedTower.transform.position = (nearNode[2].transform.position + offset);
                        startNode.GetComponent<Node>().turret = null;
                        nearNode[2].gameObject.transform.GetComponent<Node>().turret = selectedTower;

                        InfoStartNode(nearNode[2], selectedTower, nearNode[2].GetComponent<Node>().hitNode);
                        startNode.GetComponent<Node>().ShareBoost();
                    }
                }
            }

            if (MousePos().x <= startNode.transform.position.x - 2f)
            {
                if (nearNode[3] != null)
                {
                    if (nearNode[3].gameObject.transform.GetComponent<Node>().turret == null)
                    {
                        selectedTower.transform.position = (nearNode[3].transform.position + offset);
                        startNode.GetComponent<Node>().turret = null;
                        nearNode[3].gameObject.transform.GetComponent<Node>().turret = selectedTower;

                        InfoStartNode(nearNode[3], selectedTower, nearNode[3].GetComponent<Node>().hitNode);
                        startNode.GetComponent<Node>().ShareBoost();
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
}
