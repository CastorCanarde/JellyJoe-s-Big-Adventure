using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CheckPoint : MonoBehaviour
{
    GameController gameController;
    public Transform respawnPoint;

    private Animator animator;
    Collider2D coll;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isActivate", false);

        gameController = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("isActivate", true);
            gameController.UpdateCheckpoint(respawnPoint.position);
            coll.enabled = false;
        }
    }
}
