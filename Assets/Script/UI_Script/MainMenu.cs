using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Play()
    {
        //SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
       
    }

    public void AnimatedJJ()
    {
        animator.SetBool("isLoading", true);

    }
}
