using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnEnemies : MonoBehaviour
{
    bool coroutineRunning=false;
    [SerializeField] GameObject m_player;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            child.gameObject.GetComponent<EnemyAI>().player = m_player;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (coroutineRunning == false)
        {
            RespawnEnemiesFunc();
        }
    }

    IEnumerator RespawnEnemiesFunc()
    {
        coroutineRunning=true;
        yield return new WaitForSeconds(2);
        for (int i = 0; i < transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(true); //make sure all children in that area are on when the player leaves or comes
        }
        coroutineRunning=false;
    }
}


