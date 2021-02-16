using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingManager : MonoBehaviour
{
    public static SlidingManager slidingInstance;

    public bool slidingMode;

    public GameObject startNode;
    public List<GameObject> nearNode = new List<GameObject>();
    public GameObject selectedTower;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            slidingMode = !slidingMode;
        }

        if (slidingMode)
        {
            if (selectedTower != null)
            {
                //selectedTower.GetComponent<Renderer>().material.color = Color.blue;

                if (Input.GetKeyDown(KeyCode.P))
                {
                    //selectedTower.GetComponent<Renderer>().material.color = Color.white;
                    InfoStartNode(null, null, null);
                }

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (nearNode[0] != null)
                    {
                        if (nearNode[0].gameObject.transform.GetComponent<Node>().turret == null)
                        {
                            selectedTower.transform.position = nearNode[0].transform.position;
                            startNode.GetComponent<Node>().turret = null;
                            nearNode[0].gameObject.transform.GetComponent<Node>().turret = selectedTower;

                            InfoStartNode(nearNode[0], selectedTower, nearNode[0].GetComponent<Node>().hitNode);
                        }
                    }
                }

                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (nearNode[1] != null)
                    {
                        if (nearNode[1].gameObject.transform.GetComponent<Node>().turret == null)
                        {
                            selectedTower.transform.position = nearNode[1].transform.position;
                            startNode.GetComponent<Node>().turret = null;
                            nearNode[1].gameObject.transform.GetComponent<Node>().turret = selectedTower;

                            InfoStartNode(nearNode[1], selectedTower, nearNode[1].GetComponent<Node>().hitNode);
                        }
                    }
                }

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (nearNode[2] != null)
                    {
                        if (nearNode[2].gameObject.transform.GetComponent<Node>().turret == null)
                        {
                            selectedTower.transform.position = nearNode[2].transform.position;
                            startNode.GetComponent<Node>().turret = null;
                            nearNode[2].gameObject.transform.GetComponent<Node>().turret = selectedTower;

                            InfoStartNode(nearNode[2], selectedTower, nearNode[2].GetComponent<Node>().hitNode);
                        }
                    }
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (nearNode[3] != null)
                    {
                        if (nearNode[3].gameObject.transform.GetComponent<Node>().turret == null)
                        {
                            selectedTower.transform.position = nearNode[3].transform.position;
                            startNode.GetComponent<Node>().turret = null;
                            nearNode[3].gameObject.transform.GetComponent<Node>().turret = selectedTower;

                            InfoStartNode(nearNode[3], selectedTower, nearNode[3].GetComponent<Node>().hitNode);
                        }
                    }
                }
            }
        }

    }
}
