using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerController Instance;

    private Animator myAnim;
    private bool isAttacking = false;

    public Animator MyAnim => myAnim;

    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void Attack()
    {
       if( Input.GetMouseButtonDown(0) && !IsAttacking)
        {
            IsAttacking = true;
        }
    }
}
