using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip[] soundEffects;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    private void Start()
    {
        musicSource = gameObject.AddComponent<AudioSource>();

        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.volume = 0.5f;
        musicSource.Play();
    }

    public void PlaySFX(int index)
    {
        if (index >= 0 && index < soundEffects.Length)
        {
            if (sfxSource == null)
                sfxSource = gameObject.AddComponent<AudioSource>();

            sfxSource.PlayOneShot(soundEffects[index]);
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public float GetMusicVolume()
    {
        return musicSource.volume;
    }

    public void SetSFXVolume(float volume)
    {
        if (sfxSource != null)
            sfxSource.volume = volume;
    }
}