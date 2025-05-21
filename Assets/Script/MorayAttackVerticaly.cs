using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class MorayAttackVerticaly : MonoBehaviour
{
    public float downSpeed = 1f;  
    public float upSpeed = 2f;     
    public float delayTime = 0.5f; 
    public float maxDropDistance = 5f;

    [SerializeField] Animator animator;



    private void Start()
    {
        StartCoroutine(AttackSequence());

    }

    private System.Collections.IEnumerator AttackSequence()
    {
        while (true)
        {
            animator.SetFloat("Speedmultiplier", downSpeed / 0.2f);
            animator.SetBool("isAttacking", true);
            transform.DOMoveY(transform.position.y - maxDropDistance, downSpeed)
                .SetEase(Ease.InQuad);


            yield return new WaitForSeconds(downSpeed);
            animator.SetBool("isAttacking", false);

            animator.SetFloat("Speedmultiplier", upSpeed / 0.7f);
            animator.SetBool("isBacking", true);
            transform.DOMoveY(transform.position.y + maxDropDistance, upSpeed)
                .SetEase(Ease.OutQuad);

            yield return new WaitForSeconds(upSpeed);
            animator.SetBool("isBacking", false);

            animator.SetBool("isWaiting", true);
            animator.SetFloat("Speedmultiplier", delayTime / 0.08f);
            yield return new WaitForSeconds(delayTime);
            animator.SetBool("isWaiting", false);
        }

    }
}

