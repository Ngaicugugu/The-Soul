using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMusic : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "MainScene":
                AudioManager.Instance.PlayMusic(AudioManager.Instance.Background1);
                break;
            case "MenuScene":
                AudioManager.Instance.PlayMusic(AudioManager.Instance.Background1);
                break;
            default:
                AudioManager.Instance.PlayMusic(AudioManager.Instance.Background2);
                break;

        }
    }
}
