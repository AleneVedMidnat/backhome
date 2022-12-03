using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawner : MonoBehaviour
{
    [SerializeField] GameObject m_player;
    int childrenNo;
    // Start is called before the first frame update
    void Start()
    {
        childrenNo = transform.childCount;
        for (int i = 0; i < childrenNo; i++)
        {
            var child = transform.GetChild(i);
            child.gameObject.GetComponent<EnemyAI>().player = m_player;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < childrenNo; i++)
        {
            var child = transform.GetChild(i);
            child.gameObject.SetActive(true);
        }
    }
}
