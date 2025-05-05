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

    private void Start()
    {
        StartCoroutine(AttackSequence());
    }

    private System.Collections.IEnumerator AttackSequence()
    {
        while (true) 
        {
            transform.DOMoveY(transform.position.y - maxDropDistance, downSpeed)
                .SetEase(Ease.InQuad); 

            yield return new WaitForSeconds(downSpeed);

            transform.DOMoveY(transform.position.y + maxDropDistance, upSpeed)
                .SetEase(Ease.OutQuad);

            yield return new WaitForSeconds(upSpeed);

            yield return new WaitForSeconds(delayTime);
        }
    }
}

