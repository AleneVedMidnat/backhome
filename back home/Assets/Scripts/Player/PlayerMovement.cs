using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows.Speech;
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

    //animation variables
    Animator m_animator;
    private string currentAnimationState = "Idle";
    private string newState;

    public int TP = 15;
    private int CooldownTime = 5;
    [SerializeField] int coolDownTimeReset = 200;
    public float dashPower = 2f;
    public int dashTime = 0;
    private bool collidingWithEnemy = false;
    private HitCode enemyCode = null;
    private Vector2 shootDirection;
    [SerializeField] private GameObject specialAttackPrefab;
    [SerializeField] private float shootSpeed = 25f;

    //whether you can do an action 
    bool canDash = true;
    bool canAttack = true;
    bool canSpecialAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //setting this as the shoot direction may cause an issue where it shoots
        //the wrong way, please be aware of that, but putting it after can also
        //complicate things because you have to do a lot of checking axis raw
        //which may affect fps, so it may be better to be a frame off, but depends
        //ill have to test trade offs 
        if (movement != Vector2.zero)
            shootDirection = movement.normalized;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        //walk = WASD
        //run = left click
        if (Input.GetKey(KeyCode.Space) && Input.GetMouseButton(1))
        {
            state = PlayerState.DashAttack;
            newState = "Idle";
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            state = PlayerState.Dash;
            newState = "Dash";
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            state = PlayerState.Attack;
            newState = "Attack";
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            state = PlayerState.SpecialAttack;
            newState = "Idle";
        }
        else if (movement.x != 0 || movement.y != 0)
        {
            if (Input.GetKey(KeyCode.R))
            {
                state = PlayerState.Run;
                newState = "Running";
            }
            else
            {
                state = PlayerState.Walk;
                newState = "Walking";
            }
        }
        SetAnimationState();
    }

    private void LateUpdate() // animation
    {
        
    }

    void SetAnimationState()
    {
        if (newState != currentAnimationState)
        {
            m_animator.ResetTrigger(currentAnimationState);
            currentAnimationState = newState;
            m_animator.SetTrigger(currentAnimationState);
        }
        if (movement.x != 0 || movement.y != 0)
        {
            m_animator.SetFloat("Horizontal", movement.x);
            m_animator.SetFloat("Vertical", movement.y);
        }
        else
        {
            m_animator.SetTrigger("Idle");
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

        switch (state)
        {
            case PlayerState.Walk:
                Move(walkSpeed);
                break;
            case PlayerState.Run:
                Move(runSpeed);
                break;
            case PlayerState.Attack:
                Attack();
                break;
            case PlayerState.Dash:
                Dash();
                break;
            case PlayerState.DashAttack:
                DashAttack();
                break;
            case PlayerState.SpecialAttack:
                SpecialAttack();
                break;
        }

    }


    private void Move(float speed)
    {
        movement.x = movement.x * speed * Time.deltaTime;
        movement.y = movement.y * speed * Time.deltaTime;
        rb.MovePosition(new Vector2(rb.position.x + movement.x, rb.position.y + movement.y));
        StartCoroutine(SetIdle());
    }

    private void Attack()
    {
        if (canAttack == true)
        {
            //StartCoroutine(canAttackSet());
            if (collidingWithEnemy == true)
            {
                //play attack animation
                enemyCode.hp -= 1;
                Debug.Log("enemy attacked");
            }
        }
        StartCoroutine(SetIdle());
    }
    private void Dash()
    {
        if (canDash == true)
        {
            StartCoroutine(canDashSet());
            rb.velocity = new Vector2(movement.x * walkSpeed * dashPower, movement.y * walkSpeed * dashPower);
            Debug.Log("Dash initiated");
            dashTime = 2;
        }
        StartCoroutine(SetIdle());
    }
    private void DashAttack()
    {
        //issue: doesnt work, might be bc it needs a continuous collisio type.
        if (canDash == true)
        {
            Dash();
            StartCoroutine(canDashSet());
            if (enemyCode != null)
            {
                enemyCode.hp--;
                Debug.Log("enemy attacked");
            }
            Debug.Log("DashAttack initiated");
        }
        StartCoroutine(SetIdle());
    }
    private void SpecialAttack()
    {
        if (TP - 5 >= 0 && canSpecialAttack == true)
        {
            StartCoroutine(canSpecialAttackSet());
            Debug.Log(shootDirection);
            GameObject temp = Instantiate(specialAttackPrefab, transform.position, Quaternion.identity);
            temp.GetComponent<Rigidbody2D>().AddForce(shootDirection * shootSpeed, ForceMode2D.Impulse);
            TP -= 5;

        }
        Debug.Log("SpecialAttack initiated");
        StartCoroutine(SetIdle());
    }
    
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collidingWithEnemy = true;
            enemyCode = collision.gameObject.GetComponent<HitCode>();
            Debug.Log("collision enetered");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collidingWithEnemy = false;
            enemyCode = null;
        }
        Debug.Log("collision ended");
    }

    IEnumerator canDashSet()
    {
        Debug.Log("this dash has been called");
        canDash = false;
        yield return new WaitForSecondsRealtime(1);
        Debug.Log("dash reset");
        canDash = true;
    }

    IEnumerator canAttackSet()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.1f);
        canAttack = true;
    }

    IEnumerator canSpecialAttackSet()
    {
        canSpecialAttack = false;
        yield return new WaitForSeconds(1);
        canSpecialAttack = true;
    }

    IEnumerator SetIdle()
    {
        yield return new WaitForSeconds(0.5f);
        if (!Input.anyKey)
        {
            state = PlayerState.Idle;
        }
    }
}
