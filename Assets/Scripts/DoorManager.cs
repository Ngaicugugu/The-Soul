using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{
    public static DoorManager Instance;

    private Door doorBoss1;
    private Door doorBoss2;
    private Door doorBoss3;

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
        if(SceneManager.GetActiveScene().name == "MainScene")
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            FindDoors();
            UpdateDoors();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene")
        {
            FindDoors();
            UpdateDoors();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void FindDoors()
    {
        doorBoss1 = GameObject.Find("Door1").GetComponent<Door>();
        doorBoss2 = GameObject.Find("Door2").GetComponent<Door>();
        doorBoss3 = GameObject.Find("Door3").GetComponent<Door>();
    }

    public void UpdateDoors()
    {
        if (GameManager.Instance.Boss3Defeated)
        {
            doorBoss1.LockDoor();
            doorBoss2.LockDoor();
            doorBoss3.LockDoor();
        }
        else if (GameManager.Instance.Boss2Defeated)
        {
            doorBoss1.LockDoor();
            doorBoss2.LockDoor();
            doorBoss3.UnlockDoor();
        }
        else if (GameManager.Instance.Boss1Defeated)
        {
            doorBoss1.LockDoor();
            doorBoss2.UnlockDoor();
            doorBoss3.LockDoor();
        }
        else
        {
            doorBoss1.UnlockDoor();
            doorBoss2.LockDoor();
            doorBoss3.LockDoor();
        }
    }
}
