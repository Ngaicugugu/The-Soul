using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
    }

    private void Die()
    {
        PlayerController.Instance.state = PlayerController.State.GameOver;
        myAnim.SetTrigger("death");

    }
}
