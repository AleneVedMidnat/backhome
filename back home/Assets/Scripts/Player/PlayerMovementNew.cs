using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementNew : MonoBehaviour
{
    //components to get 
    Rigidbody2D m_rb;
    Animator m_animator;

    //player variables
    [SerializeField] float m_walkSpeed;
    [SerializeField] float m_runSpeed;
    PlayerState m_state = PlayerState.IDLE;
    Vector2 movement;
    public int TP = 15;
    [SerializeField] float dashPower = 2f;
    int dashtime = 2;

    //special attack
    Vector2 shootDirection;
    [SerializeField] GameObject specialAttackPrefab;
    [SerializeField] float shootSpeed = 25f;

    //collisions
    private bool collidingWithEnemy = false;
    private HitCode enemyCode = null;

    //animation variables
    string currentAnimationState = "Idle";

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movement != Vector2.zero)
            shootDirection = movement.normalized;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        //if (movement == Vector2.zero){
        //    m_state = PlayerState.IDLE;
        //}

            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    m_state = PlayerState.DASH;
            //    SetAnimationState("Dash");
            //    Dash();
            //}
        if (Input.GetKeyDown(KeyCode.Q))
        {
            m_state = PlayerState.ATTACK;
            SetAnimationState("Attack");
            Attack();
        }
        //else if (Input.GetKeyDown(KeyCode.E))
        //{
        //    m_state = PlayerState.SPECIALATTACK;
        //    SetAnimationState("Idle");
        //    SpecialAttack();
        //}
        else if (movement != Vector2.zero)
        {
            if (Input.GetKey(KeyCode.R))
            {
                m_state = PlayerState.RUN;
                SetAnimationState("Running");
                Move(m_runSpeed);
            }
            else
            {
                m_state = PlayerState.WALK;
                SetAnimationState("Walking");
                Move(m_walkSpeed);
            }
        }
        else
        {
            SetAnimationState("Idle");
        }
    }

    void SetAnimationState(string newState)
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
    }

    private void Move(float speed)
    {
        movement = movement * speed * Time.deltaTime;
        m_rb.MovePosition(new Vector2(m_rb.position.x + movement.x, m_rb.position.y + movement.y));
    }

    private void Attack()
    {
        if (collidingWithEnemy == true)
        {
            enemyCode.hp -= 1;
            Debug.Log("enemy attacked");
        }
    }

    private void Dash()
    {
        m_rb.velocity = new Vector2(movement.x * m_walkSpeed * dashPower, movement.y * m_walkSpeed * dashPower);
        Debug.Log("Dash initiated");
        dashtime = 2;
    }

    private void SpecialAttack()
    {
        if (TP - 5 >= 0)
        {
            Debug.Log(shootDirection);
            GameObject temp = Instantiate(specialAttackPrefab, transform.position, Quaternion.identity);
            temp.GetComponent<Rigidbody2D>().AddForce(shootDirection * shootSpeed, ForceMode2D.Impulse);
            TP -= 5;

        }
        Debug.Log("SpecialAttack initiated");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
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
}


enum PlayerState
{
    IDLE,
    WALK,
    RUN,
    ATTACK,
    DASH,
    SPECIALATTACK
};