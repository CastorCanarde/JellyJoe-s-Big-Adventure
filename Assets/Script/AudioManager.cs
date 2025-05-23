using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip startPlatformerMusic;
    public AudioClip platformerMusicBoucle;
    public AudioClip startRunnerMusic;
    public AudioClip runnerMusicBoucle;

    private AudioSource audioSource;
    public GamePhaseManager gamePhaseManager;

    private bool hasStartedRunner = false;
    private bool isWaitingForLoop = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = startPlatformerMusic;
        audioSource.loop = false;
        audioSource.Play();
        isWaitingForLoop = true;
    }

    void Update()
    {
        // Jouer la boucle Platformer si l'intro est terminée
        if (isWaitingForLoop && !audioSource.isPlaying && !hasStartedRunner)
        {
            audioSource.clip = platformerMusicBoucle;
            audioSource.loop = true;
            audioSource.Play();
            isWaitingForLoop = false;
        }

        // Quand on passe EN PREMIÈRE FOIS en phase Runner
        if (!hasStartedRunner && gamePhaseManager.currentPhase == GamePhaseManager.Phase.Runner)
        {
            hasStartedRunner = true;
            isWaitingForLoop = true;

            audioSource.clip = startRunnerMusic;
            audioSource.loop = false;
            audioSource.Play();
        }

        // Jouer la boucle Runner une fois que l'intro est finie
        if (hasStartedRunner && isWaitingForLoop && !audioSource.isPlaying)
        {
            audioSource.clip = runnerMusicBoucle;
            audioSource.loop = true;
            audioSource.Play();
            isWaitingForLoop = false;
        }
    }
}