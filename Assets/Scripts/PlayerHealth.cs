using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
   [SerializeField] private int health = 100;

    private Animator myAnim;

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        myAnim.Play("hurt");

        if (health <= 0)
        {
            Die();
            StartCoroutine(DelayScene());
        }
    }

    private void Die()
    {
        PlayerController.Instance.state = PlayerController.State.GameOver;
        myAnim.SetTrigger("death");

        PlayerController.Instance.SaveData();
        
    }

    private IEnumerator DelayScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainScene");
    }
}
