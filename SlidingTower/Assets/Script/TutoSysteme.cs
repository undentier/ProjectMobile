using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoSysteme : MonoBehaviour
{
    public GameObject handMoveObj;
    public GameObject turretMoveObj;
    public Animator handMoveAnim;
    public Animator turretMoveAnim;
    private bool lockTutoMoveAnim;

    private void Start()
    {
        handMoveAnim.SetBool("isTuto", true);
        StartCoroutine(CoolDown("turretTuto", true, turretMoveAnim, 0.7f));
    }

    private void Update()
    {
        if (!lockTutoMoveAnim)
        {
            if (SlideManager.instance.isSliding)
            {
                lockTutoMoveAnim = true;
                handMoveObj.SetActive(false);
                turretMoveObj.SetActive(false);
            }
        }
    }

    IEnumerator CoolDown(string boolName, bool state, Animator animator, float time)
    {
        yield return new WaitForSeconds(time);
        animator.SetBool(boolName, state);
    }
}
