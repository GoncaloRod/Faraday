using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton

    private static AudioManager _instance;

    public static AudioManager Instance => _instance;

    #endregion

    public AudioSource buildSound;

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

    public void PlayBuildSound()
    {
        buildSound.Play();
    }
}
