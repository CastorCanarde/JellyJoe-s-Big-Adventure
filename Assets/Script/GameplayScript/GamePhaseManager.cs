using Cinemachine;
using UnityEngine;

public class GamePhaseManager : MonoBehaviour
{
    public GameObject player;
    public enum Phase { Platformer, Runner }
    public Phase currentPhase;

    private AudioSource audioSource;


    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;

    public Sprite spritePlayer;
    public GameObject JellyJoeHeadElevationPhase;

    private SpriteRenderer playerSpriteRenderer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        SwitchPhase(currentPhase);
    }

    public void SwitchPhase(Phase newPhase)
    {
        currentPhase = newPhase;

        var platformer = player.GetComponent<PlayerController>();
        var runner = player.GetComponent<VerticalRunnerController>();

        if (newPhase == Phase.Platformer)
        {
            playerSpriteRenderer.enabled = true;
            JellyJoeHeadElevationPhase.SetActive(false);
            CameraManager.SwitchCamera(cam1);
            platformer.enabled = true;
            runner.enabled = false;
        }
        else if (newPhase == Phase.Runner)
        {
            playerSpriteRenderer.enabled = false;
            JellyJoeHeadElevationPhase.SetActive(true);
            CameraManager.SwitchCamera(cam2);
            platformer.enabled = false;
            runner.enabled = true;
        }
    }
}