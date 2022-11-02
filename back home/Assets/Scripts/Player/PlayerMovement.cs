using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

//needs doing:
//fucntion to take damage

public class PlayerMovement : MonoBehaviour
{
    enum PlayerState
    {
        Idle = 0,
        Walk,
        Run,
        Attack,
        Dash,
        DashAttack,
        SpecialAttack
    }

    Rigidbody2D rb;
    Vector2 movement;
    public float walkSpeed = 3f;
    public float runSpeed = 5f;
    PlayerState state;
    Animator animator;
    public int HP = 25;
    public int TP = 15;
    public int CooldownTime = 5;
    public float dashPower = 2f;
    public int dashTime = 0;
    private bool collidingWithEnemy = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        //walk = WASD
        //run = left click
        if (Input.GetKey(KeyCode.Space) && Input.GetMouseButton(1))
        {
            state = PlayerState.DashAttack;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            state = PlayerState.Dash;
        }
        else if (Input.GetMouseButton(1))
        {
            state = PlayerState.Attack;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            state = PlayerState.SpecialAttack;
        }
        else if (movement.x != 0 || movement.y != 0)
        {
            if (Input.GetMouseButton(0))
            {
                state = PlayerState.Run;
            }
            else
            {
                state = PlayerState.Walk;
            }
        }
        else
        {
            state = PlayerState.Idle;
        }
    }

    private void LateUpdate() // animation
    {
        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("horizontal", movement.x);
            animator.SetFloat("vertical", movement.y);
            animator.SetBool("idle", false);
        }
        else
        {
            animator.SetBool("idle", true);
        }
    }

    void FixedUpdate() // physics
    {
        if (dashTime == 0)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
           dashTime--;
        }
        //Idle,Walk,Run,Attack,Dash,DashAttack,SpecialAttack
        if (CooldownTime > 0)
        {
            CooldownTime--;
        }
        else if (CooldownTime < 0)
        {
            CooldownTime = 0;
        }
        bool check = coolDownTimeZero(); //checks if cooldown is 0

        switch (state)
        {
            case PlayerState.Walk:
                Move(walkSpeed);
                break;
            case PlayerState.Run:
                Move(runSpeed);
                break;
            case PlayerState.Attack:
                Attack(check);
                break;
            case PlayerState.Dash:
                Dash(check);
                break;
            case PlayerState.DashAttack:
                DashAttack(check);
                break;
            case PlayerState.SpecialAttack:
                SpecialAttack(check);
                break;
        }

    }


    private void Move(float speed)
    {
        movement.x = movement.x * speed * Time.deltaTime;
        movement.y = movement.y * speed * Time.deltaTime;
        rb.MovePosition(new Vector2(rb.position.x + movement.x, rb.position.y + movement.y));
    }
    private void Attack(bool check)
    {
        if (check == true)
        {
            if (collidingWithEnemy == true)
            {
                //play attack animation
                //take off enemies hp
            }
            Debug.Log("Attack initiated");
        }
        
    }
    private void Dash(bool check)
    {
        if (check == true)
        {
            rb.velocity = new Vector2(movement.x * walkSpeed * dashPower, movement.y * walkSpeed * dashPower);
            Debug.Log("Dash initiated");
            CooldownTime = 50;
            dashTime = 2;
        }

    }
    private void DashAttack(bool check)
    {
        if (check == true)
        {
            //check if enemy is in path of dash if so take away their hp
            //you can just call dash to get them to actual dash
            Debug.Log("DashAttack initiated");
        }
    }
    private void SpecialAttack(bool check)
    {
        if (TP - 5 >= 0 && check == true)
        {
            //create special attack in direction the player is facing (animator direction i guess cause it holds the
            // last frame data
        }
        Debug.Log("SpecialAttack initiated");
    }
    private bool coolDownTimeZero()
    {
        if (CooldownTime > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collidingWithEnemy = true;
            HitCode enemyCode = collision.GetComponent<HitCode>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collidingWithEnemy = false;
        }
    }
}
