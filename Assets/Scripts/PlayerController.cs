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
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float jumpForce;

    private bool isAttacking = false;
    private bool isAirAttacking = false;
    private float horizontalInput;
    private bool isDashing;
    private bool canMove;
    private bool isGrounded;
    private float facingDirection=1;
    private float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash= -100f;

    private SpriteRenderer sprite;
    private Animator myAnim;
    private Rigidbody2D rb;
    

    public Animator MyAnim => myAnim;

    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }

    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }
    public bool IsAirAttacking { get => isAirAttacking; set => isAirAttacking = value; }

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
        AirAttack();
        Attack();
        if (canMove)
        {
            MovePlayer();
        }
        GroundCheck();
        Jump();
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

    private void AirAttack()
    {
        if (Input.GetMouseButtonDown(0) && !isAirAttacking && !isGrounded)
        {
            isAirAttacking = true;
            rb.AddForce(Vector2.down * 6,ForceMode2D.Impulse);
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
            if(Time.time >= (lastDash + dashCooldown))
            {
                AttemptToDash();
            }
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
        myAnim.SetFloat("vertical_velocity", rb.velocity.y);
        myAnim.SetBool("is_grounded", isGrounded);
    }

    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
        isGrounded = hit.collider != null;
    }

    private void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
