using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackCode : MonoBehaviour
{
    [SerializeField] int hpReduced = 2;

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

}
