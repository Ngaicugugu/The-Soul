using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerController Instance;

    [SerializeField] private float speed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float distanceBetweenImages;
    [SerializeField] private float dashCooldown;

    private bool isAttacking = false;
    private float horizontalInput;
    [SerializeField] private float facingDirection=1;
    private bool isDashing;
    private bool canMove;


    private float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash= -100f;

    private SpriteRenderer sprite;
    private Animator myAnim;
    private Rigidbody2D rb;

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
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        if (canMove)
        {
            MovePlayer();
        }
        AnimationController();
        CheckDash();
        Dashing();
    }

    private void Attack()
    {
       if( Input.GetMouseButtonDown(0) && !IsAttacking)
        {
            IsAttacking = true;
        }
    }

    private void MovePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        float movementValue = horizontalInput * speed;
        rb.velocity = new Vector2 (movementValue, rb.velocity.y);

        if(horizontalInput != 0)
        {
            FaceFlix();
        }
    }

    private void Dashing()
    {
        if (Input.GetMouseButtonDown(1))
        {
            AttemptToDash();
        }
    }

    private void AttemptToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        AfterPool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
    }

    private void CheckDash()
    {
        if (isDashing)
        {
            if(dashTimeLeft > 0)
            {
                canMove = false;
                rb.velocity = new Vector2(dashSpeed * facingDirection, rb.velocity.y);
                dashTimeLeft -= Time.deltaTime;

                if(Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    AfterPool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }

            if (dashTimeLeft <= 0)
            {
                isDashing=false;
                canMove= true;
            }
        }
    }

    private void FaceFlix()
    {
        
        sprite.flipX = horizontalInput < 0;
        if(sprite.flipX)
        {
            facingDirection = -1;
        }
        else
        {
            facingDirection = 1;
        }
    }

    private void AnimationController()
    {
        myAnim.SetFloat("move_speed", Mathf.Abs(horizontalInput));
    }
}
