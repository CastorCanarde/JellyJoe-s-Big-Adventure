using Cinemachine;
using UnityEngine;
using System.Collections;

public class GamePhaseManager : MonoBehaviour
{
    public GameObject player;
    public enum Phase { Platformer, Runner }
    public Phase currentPhase;
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;

    void Start()
    {
        SwitchPhase(currentPhase);
    }

    public void SwitchPhase(Phase newPhase)
    {
        currentPhase = newPhase;

        var platformer = player.GetComponent<PlayerController>();
        var runner = player.GetComponent<VerticalRunnerController>();

        if (newPhase == Phase.Platformer)
        {
            CameraManager.SwitchCamera(cam1);
            platformer.enabled = true;
            runner.enabled = false;
        }
        else if (newPhase == Phase.Runner)
        {
            CameraManager.SwitchCamera(cam2);
            platformer.enabled = false;
            runner.enabled = true;
        }
    }

  
}