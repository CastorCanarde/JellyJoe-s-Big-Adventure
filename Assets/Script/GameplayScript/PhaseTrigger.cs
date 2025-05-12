using UnityEngine;
using Cinemachine;

public class PhaseTrigger : MonoBehaviour
{
    public GamePhaseManager gamePhaseManager;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gamePhaseManager.SwitchPhase(GamePhaseManager.Phase.Runner);
        }
    }
}