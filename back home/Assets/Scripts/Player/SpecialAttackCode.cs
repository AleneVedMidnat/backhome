using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackCode : MonoBehaviour
{
    [SerializeField] int hpReduced = 2;

    private void Start()
    {
        StartCoroutine(destroyself());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            HitCode temp = collision.GetComponent<HitCode>();
            temp.hp -= hpReduced;
            Debug.Log("collision detected");
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    IEnumerator destroyself()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
