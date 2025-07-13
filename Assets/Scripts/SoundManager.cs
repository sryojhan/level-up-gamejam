using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    protected override bool DestroyOnLoad => false;

    public AudioSource music;
    public AudioSource soundEffects;

    public AudioSource stepsSource;
    public AudioSource dialogueSource;

    public AudioClip[] attack;
    public AudioClip[] footSteps;

    public AudioClip boomerangThrow;
    public AudioClip hit;

    public AudioClip collect;
    public AudioClip boing;

    public AudioClip lockedDoor;
    public AudioClip openDoor;


    public AudioClip damageTaken;

    public float musicVolume = .7f;
    public float soundVolume = .7f;

    private void Awake()
    {
        DestroyIfInitialised(this);
    }

    private void Start()
    {
        SetMusicVolume(musicVolume);
        SetEffectsVolume(soundVolume);
    }

    public void SetMusicVolume(float value)
    {
        music.volume = value;
    }

    public void SetEffectsVolume(float value)
    {
        stepsSource.volume = value;
        dialogueSource.volume = value;
        soundEffects.volume = value;
    }

    public void PlayCollectItem()
    {
        soundEffects.PlayOneShot(collect);
    }

    public void PlayUIBoing()
    {
        soundEffects.PlayOneShot(boing);
    }


    public float maxStepPitchChange = .2f;

    public void PlayStep()
    {
        AudioClip step = footSteps[Random.Range(0, footSteps.Length)];

        float pitch = 1 + Random.insideUnitSphere.x * maxStepPitchChange;

        stepsSource.pitch = pitch;
        stepsSource.PlayOneShot(step);
    }

    public void PlayAttack()
    {
        AudioClip play = attack[Random.Range(0, attack.Length)];

        soundEffects.PlayOneShot(play);
    }

    public void PlayHit()
    {
        soundEffects.PlayOneShot(hit);
    }

    public void PlayThrowBoomerang()
    {
        soundEffects.PlayOneShot(boomerangThrow);
    }

    public void BeginDialogue()
    {
        dialogueSource.Play();
    }

    public void EndDialogue()
    {
        dialogueSource.Stop();
    }

    public void PlayLockedDoor()
    {
        soundEffects.PlayOneShot(lockedDoor);
    }

    public void PlayOpenDoor()
    {
        soundEffects.PlayOneShot(openDoor);
    }

    public void PlayDamageTaken()
    {
        soundEffects.PlayOneShot(damageTaken);
    }
}
