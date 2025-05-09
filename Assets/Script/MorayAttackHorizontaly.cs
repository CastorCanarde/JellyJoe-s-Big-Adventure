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

    private void Start()
    {
        StartCoroutine(AttackSequence());
    }

    private System.Collections.IEnumerator AttackSequence()
    {
        while (true) 
        {
            transform.DOMoveX(transform.position.x - maxDistance, goSpeed)
                .SetEase(Ease.InQuad); 

            yield return new WaitForSeconds(goSpeed);

            transform.DOMoveX(transform.position.x + maxDistance, backSpeed)
                .SetEase(Ease.OutQuad);

            yield return new WaitForSeconds(backSpeed);

            yield return new WaitForSeconds(delayTime);
        }
    }
}

