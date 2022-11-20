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
}
