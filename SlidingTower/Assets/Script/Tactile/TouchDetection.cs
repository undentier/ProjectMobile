using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetection : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;

    void Update()
    {
        TestDetection();
    }

    void TestDetection()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    ray = Camera.main.ScreenPointToRay(touch.position);

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.transform.gameObject.layer == 8)
                        {
                            hit.transform.GetComponent<NodeSysteme>().TouchDetection();
                        }
                    }
                }

                else if (touch.phase == TouchPhase.Ended)
                {
                    ray = Camera.main.ScreenPointToRay(touch.position);

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.transform.gameObject.layer == 8)
                        {
                            if (SlideManager.instance.isSliding)
                            {
                                hit.transform.GetComponent<NodeSysteme>().TouchDetection();
                            }
                        }
                    }
                }
            }
        }
        else if (Input.GetButtonDown("LeftClick"))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == 8)
                {
                    hit.transform.GetComponent<NodeSysteme>().TouchDetection();
                }
            }
        }
        else if (Input.GetButtonUp("LeftClick"))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == 8)
                {
                    hit.transform.GetComponent<NodeSysteme>().TouchDetection();
                }
            }
        }
    }
}
