using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadishSpawner : MonoBehaviour
{
    [SerializeField] GameObject radishPrefab;
    [SerializeField] GameObject m_player;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(countDown());
    }

    IEnumerator countDown()
    {
        yield return new WaitForSeconds(10);
        GameObject temp = Instantiate(radishPrefab, transform.position, Quaternion.identity);
        temp.GetComponent<EnemyAI>().player = m_player;
        StartCoroutine(countDown());
    }
}
