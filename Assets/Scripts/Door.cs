using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private bool isLocked = false;

    private bool isPlayerNear;

    private void Update()
    {
        if (!isLocked && isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            PlayerController.Instance.SaveData();
            SceneManager.LoadScene(sceneName);
        }
        else if(isLocked && isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Chua du dieu kien");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }


    public void LockDoor()
    {
        isLocked = true;
    }

    public void UnlockDoor()
    {
        isLocked = false;
    }
}
