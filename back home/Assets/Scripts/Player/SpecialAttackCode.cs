using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpecialAttackCode : MonoBehaviour
{
    [SerializeField] int hpReduced = 2;
    Rigidbody2D rb;
    public Vector3 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 180;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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


}
