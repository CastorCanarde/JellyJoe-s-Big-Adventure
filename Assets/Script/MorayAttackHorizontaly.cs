using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class MorayAttackHorizontaly : MonoBehaviour
{
    public float goSpeed = 1f;  
    public float backSpeed = 2f;     
    public float delayTime = 0.5f; 
    public float maxDistance = 5f;

    [SerializeField] Animator animator;


    private void Start()
    {
        StartCoroutine(AttackSequence());
    }

    private System.Collections.IEnumerator AttackSequence()
    {
        while (true) 
        {
            animator.SetFloat("Speedmultiplier", goSpeed / 0.2f);
            animator.SetBool("isAttacking", true);
            transform.DOMoveX(transform.position.x - maxDistance, goSpeed)
                .SetEase(Ease.InQuad); 

            yield return new WaitForSeconds(goSpeed);
            animator.SetBool("isAttacking", false);

            animator.SetFloat("Speedmultiplier", backSpeed / 0.7f);
            animator.SetBool("isBacking", true);
            transform.DOMoveX(transform.position.x + maxDistance, backSpeed)
                .SetEase(Ease.OutQuad);


            yield return new WaitForSeconds(backSpeed);
            animator.SetBool("isBacking", false);

            animator.SetBool("isWaiting", true);
            animator.SetFloat("Speedmultiplier", delayTime / 0.08f);
            yield return new WaitForSeconds(delayTime);
            animator.SetBool("isWaiting", false);

        }
    }
}

