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

    public GameObject m_UI;

    Vector2 movement;
    //PlayerState m_state = PlayerState.idle;

    //string nextState;
    //string currentState;
    bool canChangeState = true;

    private bool collidingWithEnemy = false;
    private HitCode enemyCode = null;

    public float dashPower = 100f;
    public int dashTime = 0;
    public int TP = 25;

    private Vector2 shootDirection;
    [SerializeField] private GameObject specialAttackPrefab;
    [SerializeField] private float shootSpeed = 25f;

    private Queue<string> functionQueue;

    //audio 
    [SerializeField] AudioSource attackAudio;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        functionQueue = new Queue<string>();
        m_UI.GetComponent<UIscript>().MaxTP = TP;
        shootDirection = new Vector2(0, 1);
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
                //m_state = PlayerState.attack;
                m_animator.Play("Base Layer.Attack");
                attackAudio.Play();
                StartCoroutine(SetToIdle(0.5f));
                StartCoroutine(ResetMovement(0.5f));
                functionQueue.Enqueue("attack");
                //Attack();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                //m_state = PlayerState.dash;
                m_animator.Play("Base Layer.Idle");
                StartCoroutine(SetToIdle(0.1f));
                StartCoroutine(ResetMovement(0.1f));
                functionQueue.Enqueue("dash");
                //Dash();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                //m_state = PlayerState.specialAttack;
                m_animator.Play("Base Layer.Idle");
                StartCoroutine(ResetMovement(0.5f));
                functionQueue.Enqueue("specialAttack");
                //SpecialAttack();
            }
            
            //else
            //{
            //    m_state = PlayerState.idle;
            //}
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
        if (movement != Vector2.zero)
        {
            if (Input.GetKey(KeyCode.R))
            {
                //m_state = PlayerState.running;
                m_animator.Play("Running");
                StartCoroutine(SetToIdle(0.5f));
                //functionQueue.Enqueue("running");
                Move(runSpeed);
            }
            else
            {
                //m_state = PlayerState.walking;
                m_animator.Play("Walking");
                StartCoroutine(SetToIdle(0.5f));
                //functionQueue.Enqueue("walking");
                Move(walkSpeed);
            }
        }
        if (functionQueue.Count > 0)
        {
            switch (functionQueue.Peek())
            {
                case "attack":
                    Attack();
                    break;
                case "dash":
                    Dash();
                    break;
                case "specialAttack":
                    SpecialAttack();
                    break;
                //case "walking":
                //    Move(walkSpeed);
                //    break;
                //case "running":
                //    Move(runSpeed);
                //    break;
            }
            functionQueue.Dequeue();
        }

    }

    private void Move(float speed)
    {
        Debug.Log("move used");
        movement.x = movement.x * speed * Time.deltaTime;
        movement.y = movement.y * speed * Time.deltaTime;
        m_rb.MovePosition(new Vector2(m_rb.position.x + movement.x, m_rb.position.y + movement.y));
        m_animator.SetFloat("Horizontal", movement.x);
        m_animator.SetFloat("Vertical", movement.y);
    }

    private void Attack()
    {
        Debug.Log("attack called");
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
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            temp.transform.rotation = Quaternion.AngleAxis(angle + 135, Vector3.forward);
            
            TP -= 5;
            m_UI.GetComponent<UIscript>().SubtractTP(5);

        }
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

