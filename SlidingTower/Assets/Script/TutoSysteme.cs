using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoSysteme : MonoBehaviour
{
    public GameObject handMoveObj;
    public GameObject turretMoveObj;
    [Space]
    public Animator handMoveAnim;
    public Animator turretMoveAnim;
    private bool lockMoveAnim;

    void Start()
    {
        handMoveAnim.SetBool("canMove", true);
    }

    
    void Update()
    {
        if (!lockMoveAnim)
        {
            if (SlideManager.instance.isSliding)
            {
                handMoveObj.SetActive(false);
                turretMoveObj.SetActive(false);
            }
        }
    }


    IEnumerator CoolDown(string boolName, Animator objAnimator, float coolDown, bool state)
    {
        yield return new WaitForSeconds(coolDown);
        objAnimator.SetBool(boolName, state);
    }
}
