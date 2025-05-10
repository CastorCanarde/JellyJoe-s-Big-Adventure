using UnityEngine;

public class GamePhaseManager : MonoBehaviour
{
    public GameObject player;
    public enum Phase { Platformer, Runner }
    public Phase currentPhase;



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
            platformer.enabled = true;
            runner.enabled = false;
        }
        else if (newPhase == Phase.Runner)
        {
            platformer.enabled = false;
            runner.enabled = true;

        }
    }

}