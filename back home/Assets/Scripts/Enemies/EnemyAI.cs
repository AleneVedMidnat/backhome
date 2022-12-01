using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Vector2 m_startPosition;
    Vector2 m_targetPosition;
    public GameObject player;
    [SerializeField] int distanceToChase;
    [SerializeField] float m_speed;
    bool colliding;
    [SerializeField] AudioSource attackaudio;

    void Start()
    {
        m_startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.transform.position - transform.position).magnitude < distanceToChase)
        {
            m_targetPosition = player.transform.position;
            ShouldAttack();
        }
        else
        {
            m_targetPosition = m_startPosition;
        }

    }
    private void FixedUpdate()
    {
        if ((Vector2)transform.position != m_targetPosition)
        {
            transform.position = Vector2.MoveTowards((Vector2)transform.position, m_targetPosition, m_speed);
        }
    }

    void ShouldAttack()
    {
        int temp = Random.Range(0, 50);
        if (temp == 25)
        {
            Attack();
        }
    }

    void Attack()
    {
        if (colliding == true)
        {
            Debug.Log("attack from enemy");
            player.GetComponent<PlayerHealth>().decreaseHP(2);
            attackaudio.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            colliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            colliding = true;
        }
    }
}
