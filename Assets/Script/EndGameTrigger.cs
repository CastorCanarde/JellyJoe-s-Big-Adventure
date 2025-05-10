using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class EndGameTrigger : MonoBehaviour
{
    public string sceneDeFin = "EndGame";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DOTween.KillAll(true);
            SceneManager.LoadScene(sceneDeFin);
        }
    }
}