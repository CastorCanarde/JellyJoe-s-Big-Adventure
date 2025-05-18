using UnityEngine;
using Cinemachine;
using System.Collections;

public class PhaseTrigger : MonoBehaviour
{
    public GamePhaseManager gamePhaseManager;

    public GameObject SpeedEffect;

    public GameObject JellyJoeHeadElevationPhase;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraShake.Instance.ShakeCamera(4f, 100f);
            Instantiate(SpeedEffect);
            gamePhaseManager.SwitchPhase(GamePhaseManager.Phase.Runner);
        }
    }
}