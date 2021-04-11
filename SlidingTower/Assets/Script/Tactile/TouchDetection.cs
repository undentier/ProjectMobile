using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        #region Mobile Input
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    ray = Camera.main.ScreenPointToRay(touch.position);

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.transform.gameObject.layer == 5)
                        {
                            return;
                        }

                        if (hit.transform.gameObject.layer == 8)
                        {
                            hit.transform.GetComponent<NodeSysteme>().TouchDetection();
                        }
                    }
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    if (SlideManager.instance.isSliding)
                    {
                        SlideManager.instance.EndSlide();
                    }

                    ray = Camera.main.ScreenPointToRay(touch.position);

                    if (Physics.Raycast(ray, out hit))
                    {
                        
                    }
                }
            }
        }
        #endregion

        #region Mouse Input
        if (Input.GetButtonDown("LeftClick"))
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
            if (SlideManager.instance.isSliding)
            {
                SlideManager.instance.EndSlide();
            }

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {

            }
        }
        //EventSystem.current.IsPointerOverGameObject()
        #endregion
    }
}
