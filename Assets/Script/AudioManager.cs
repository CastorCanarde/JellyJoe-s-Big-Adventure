using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip startPlatformerMusic;
    public AudioClip platformerMusicBoucle;
    public AudioClip startRunnerMusic;
    public AudioClip runnerMusicBoucle;

    private AudioSource audioSource;
    public GamePhaseManager gamePhaseManager;

    private bool hasStartedLoop = false;
    private GamePhaseManager.Phase lastPhase;

    // Ajout : on garde une trace de si chaque phase a d�j� �t� jou�e
    private bool hasPlayedPlatformer = false;
    private bool hasPlayedRunner = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lastPhase = gamePhaseManager.currentPhase;

        PlayPhaseIntroOnce(lastPhase);
    }

    void Update()
    {
        var currentPhase = gamePhaseManager.currentPhase;

        // Si la phase a chang�
        if (currentPhase != lastPhase)
        {
            lastPhase = currentPhase;
            hasStartedLoop = false;

            PlayPhaseIntroOnce(currentPhase);
        }

        // Si la musique d�intro est termin�e et qu�on n�a pas encore jou� la boucle
        if (!audioSource.isPlaying && !hasStartedLoop)
        {
            PlayPhaseLoop(currentPhase);
            hasStartedLoop = true;
        }
    }

    void PlayPhaseIntroOnce(GamePhaseManager.Phase phase)
    {
        audioSource.loop = false;

        // Si la phase n�a jamais �t� jou�e avant, on joue son intro
        switch (phase)
        {
            case GamePhaseManager.Phase.Platformer:
                if (!hasPlayedPlatformer)
                {
                    audioSource.clip = startPlatformerMusic;
                    audioSource.Play();
                    hasPlayedPlatformer = true;
                }
                break;

            case GamePhaseManager.Phase.Runner:
                if (!hasPlayedRunner)
                {
                    audioSource.clip = startRunnerMusic;
                    audioSource.Play();
                    hasPlayedRunner = true;
                }
                break;
        }
    }

    void PlayPhaseLoop(GamePhaseManager.Phase phase)
    {
        audioSource.loop = true;

        switch (phase)
        {
            case GamePhaseManager.Phase.Platformer:
                audioSource.clip = platformerMusicBoucle;
                break;

            case GamePhaseManager.Phase.Runner:
                audioSource.clip = runnerMusicBoucle;
                break;
        }

        audioSource.Play();
    }
}