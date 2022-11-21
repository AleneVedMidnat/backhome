using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class pmn : MonoBehaviour
{
    Rigidbody2D m_rb;
    Animator m_animator;
    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float runSpeed = 5f;

    Vector2 movement;
    PlayerState m_state = PlayerState.idle;

    string nextState;
    string currentState;
    bool canChangeState = true;

    private bool collidingWithEnemy = false;
    private HitCode enemyCode = null;

    public float dashPower = 100f;
    public int dashTime = 0;
    [SerializeField] int TP = 25;

    private Vector2 shootDirection;
    [SerializeField] private GameObject specialAttackPrefab;
    [SerializeField] private float shootSpeed = 25f;

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
        if (canChangeState == true)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            if (Input.GetKeyDown(KeyCode.Q))
            {
                m_state = PlayerState.attack;
                m_animator.Play("Base Layer.Attack");
                StartCoroutine(SetToIdle(0.5f));
                StartCoroutine(ResetMovement(0.5f));
                Attack();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                m_state = PlayerState.dash;
                m_animator.Play("Base Layer.Idle");
                StartCoroutine(SetToIdle(0.1f));
                StartCoroutine(ResetMovement(0.1f));
                Dash();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                m_state = PlayerState.specialAttack;
                m_animator.Play("Base Layer.Idle");
                StartCoroutine(ResetMovement(0.5f));
                SpecialAttack();
            }
            else if (movement != Vector2.zero)
            {
                if (Input.GetKey(KeyCode.R))
                {
                    m_state = PlayerState.running;
                    m_animator.Play("Running");
                    StartCoroutine(SetToIdle(0.5f));
                    Move(runSpeed);
                }
                else
                {
                    m_state = PlayerState.walking;
                    m_animator.Play("Walking");
                    StartCoroutine(SetToIdle(0.5f));
                    Move(walkSpeed);
                }
            }
            else
            {
                m_state = PlayerState.idle;
            }
        }
    }

    private void FixedUpdate()
    {
        if (dashTime == 0)
        {
            m_rb.velocity = Vector2.zero;
        }
        else
        {
            dashTime--;
        }
    }

    private void Move(float speed)
    {
        movement = movement.normalized * speed * Time.deltaTime;
        m_rb.MovePosition(new Vector2(m_rb.position.x + movement.x, m_rb.position.y + movement.y));
        m_animator.SetFloat("Horizontal", movement.x);
        m_animator.SetFloat("Vertical", movement.y);
    }

    private void Attack()
    {
        if (collidingWithEnemy == true)
        {
            //play attack animation
            enemyCode.hp -= 1;
            Debug.Log("enemy attacked");
        }
    }

    private void Dash()
    {
        if (movement.x != 0 || movement.y != 0)
        {
            m_rb.velocity = new Vector2(movement.x * walkSpeed * dashPower, movement.y * walkSpeed * dashPower);
            Debug.Log("Dash initiated");
            dashTime = 5;
        }
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

    IEnumerator SetToIdle(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        if (Input.anyKey == false)
        {
            m_animator.Play("Base Layer.Idle");
        }
    }

    IEnumerator ResetMovement(float timeToWait)
    {
        canChangeState = false;
        yield return new WaitForSeconds(timeToWait);
        canChangeState = true;
    }
  
    enum PlayerState
    {
        walking,
        running,
        attack,
        specialAttack,
        dash,
        idle
    }
}

