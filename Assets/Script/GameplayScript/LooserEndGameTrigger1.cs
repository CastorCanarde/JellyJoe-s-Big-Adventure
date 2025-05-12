using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LooserEndGameTrigger1 : MonoBehaviour
{
    public string LooseMenu = "LooseMenu";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DOTween.KillAll(true);
            SceneManager.LoadScene(LooseMenu);
        }
    }
}