using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : PersistentSingleton<AudioManager>
{
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource effectsAudioSource;

    public void PlaySound(AudioClip audioClip)
    {
        effectsAudioSource.PlayOneShot(audioClip);
    }

    public void PlaySound(AudioClip audioClip, float volume)
    {
        effectsAudioSource.PlayOneShot(audioClip, volume);
    }

    public void PlayRandomSound(AudioClip[] audioClips)
    {
        int randomAudioClipIndex = Random.Range(0, audioClips.Length);
        AudioClip audioClip = audioClips[randomAudioClipIndex];
        effectsAudioSource.PlayOneShot(audioClip);
    }

    public void PlayRandomSound(AudioClip[] audioClips, float volume)
    {
        int randomAudioClipIndex = Random.Range(0, audioClips.Length);
        AudioClip audioClip = audioClips[randomAudioClipIndex];
        effectsAudioSource.PlayOneShot(audioClip, volume);
    }

    public void PlayMusic(AudioClip audioClip, float volume)
    {
        if (musicAudioSource.clip == audioClip)
            return;

        musicAudioSource.Stop();
        musicAudioSource.clip = audioClip;
        musicAudioSource.volume = volume;
        musicAudioSource.Play();
    }

    public void PauseMusic()
    {
        musicAudioSource.Pause();
    }

    public void StopMusic()
    {
        musicAudioSource.Stop();
    }

    public void ToggleMusic()
    {
        musicAudioSource.mute = !musicAudioSource.mute;
    }

    public void ToggleEffects()
    {
        effectsAudioSource.mute = !effectsAudioSource.mute;
    }

    public void ChangeMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void ChangeMusicVolume(float volume)
    {
        musicAudioSource.volume = volume;
    }

    public void ChangeEffectsVolume(float volume)
    {
        effectsAudioSource.volume = volume;
    }

    public float GetMusicVolume()
    {
        return musicAudioSource.volume;
    }

    public float GetEffectsVolume()
    {
        return effectsAudioSource.volume;
    }

    public float CalculateVolumeByCollisionForce(float collisionForce, float forceThreshold)
    {
        float volume = 1;

        if (collisionForce <= forceThreshold)
        {
            volume = collisionForce / forceThreshold;
        }

        Debug.Log("Volume: " + volume);

        return volume;
    }
}
