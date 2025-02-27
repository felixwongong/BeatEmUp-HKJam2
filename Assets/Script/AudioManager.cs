using System;
using DocumentFormat.OpenXml.Presentation;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class AudioManager : MonoInstance<AudioManager>
{
    public enum AudioType
    {
        Explosion,
        Fall,
        HitFinisher,
        HitLarge1,
        HitLarge2,
        HitLarge3,
        HitSmall1,
        HitSmall2,
        KickSwing,
        Level1BGM,
        Level2BGM,
        Level3BGM,
        MainBGM,
        PunchSwing,
        VictoryMale,
        Walk1,
        Walk2,
        WoodBatFinisher,
        WoodBatSwing
    }

    // Singleton to keep instance alive through all scenes
    [Header("---------- Audio Source ----------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("---------- Audio Clip   ----------")]
    public AudioClip explosion;
    public AudioClip fall;
    public AudioClip hitFinisher;
    public AudioClip hitLarge1;
    public AudioClip hitLarge2;
    public AudioClip hitLarge3;
    public AudioClip hitSmall1;
    public AudioClip hitSmall2;
    public AudioClip kickSwing;
    public AudioClip level1BGM;
    public AudioClip level2BGM;
    public AudioClip level3BGM;
    public AudioClip mainBGM;
    public AudioClip punchSwing;
    public AudioClip victoryMale;
    public AudioClip walk1;
    public AudioClip walk2;
    public AudioClip woodBatFinisher;
    public AudioClip woodBatSwing;
    public AudioClip youDeerSound;

    public void PlaySoundFXClip(AudioClip clip, float volume, bool randomPitch = false)
    {
        if (randomPitch)
        {
            SFXSource.pitch = Random.Range(0.9f, 1.1f);
        }
        SFXSource.volume = volume;
        SFXSource.PlayOneShot(clip);
    }

    public void PlayBgm(AudioClip clip, float volume)
    {
        musicSource.clip = clip;
        musicSource.Play();
        musicSource.volume = volume;
    }

    public void StopBgm()
    {
        musicSource.Stop();
    }
}