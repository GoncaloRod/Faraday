using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton

    private static AudioManager _instance;

    public static AudioManager Instance => _instance;

    #endregion

    public AudioSource buildSound, clickSound, mainMenuMusic, chillMusic, introBuildingMusic, buildingMusic;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void Update()
    {
        if (chillMusic.isPlaying && chillMusic.volume <= 0.14f)
        {
            if (chillMusic.volume <= 0.14f)
                chillMusic.volume += 0.01f * Time.deltaTime;
        }
        else if (introBuildingMusic.isPlaying && introBuildingMusic.volume <= 0.14f)
        {
            if (introBuildingMusic.volume <= 0.14f)
                introBuildingMusic.volume += 0.01f * Time.deltaTime;
        }
    }

    public void PlayChillMusic()
    {
        chillMusic.Play();
    }

    public void StopChillMusic()
    {
        chillMusic.Stop();
        chillMusic.volume = 0.0f;
    }

    public void PlayBuildSound()
    {
        buildSound.Play();
    }

    public void PlayClickSound()
    {
        clickSound.Play();
    }

    public void PlayBuildingMusic()
    {
        introBuildingMusic.Play();
        buildingMusic.PlayDelayed(introBuildingMusic.clip.length);
    }

    public void StopBuildingMusic()
    {
        introBuildingMusic.Stop();
        buildingMusic.Stop();
        introBuildingMusic.volume = 0.0f;
        PlayChillMusic();
    }
}
