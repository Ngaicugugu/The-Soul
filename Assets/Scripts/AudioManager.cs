using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sFXSource;

    public AudioClip Background1;
    public AudioClip Background2;
    public AudioClip Attack;
    public AudioClip FootStep;
    public AudioClip Death;
    public AudioClip PickUp;
    public AudioClip Slash;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        musicSource.clip = Background1;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sFXSource.PlayOneShot(clip);
    }

}
