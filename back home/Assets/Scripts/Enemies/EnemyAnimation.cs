using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class EnemyAnimation : MonoBehaviour
{
    Animator m_animator;
    states m_state;
    Rigidbody2D m_rb;
    SpriteRenderer m_spriteRen;
    string currentstate;
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_state = states.IDLE;
        currentstate = "idle";
        m_rb = GetComponent<Rigidbody2D>();
        m_spriteRen = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_rb.velocity.x > 0)
        {
            m_spriteRen.flipX = true;
        }
        else
        {
            m_spriteRen.flipX = false; 
        }

        m_animator.ResetTrigger(currentstate);
        switch (m_state) 
        { 
            case states.IDLE:
                m_animator.SetTrigger("idle");
                currentstate = "idle";
                break;
            case states.MOVING:
                m_animator.SetTrigger("moving");
                currentstate = "moving";
                break;
            case states.HIT:
                m_animator.SetTrigger("hit");
                currentstate = "hit";
                break;
        }

    }
}

enum states
{
    HIT,
    MOVING,
    IDLE
}
