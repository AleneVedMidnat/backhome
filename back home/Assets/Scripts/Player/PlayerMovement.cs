using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    float movementSpeed = 5f;
    Vector2 movement;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

    }

    void FixedUpdate()
    {
        if (movement.x != 0 || movement.y != 0)
        {
            movement.x = movement.x * movementSpeed * Time.deltaTime;
            movement.y = movement.y * movementSpeed * Time.deltaTime;
            animator.SetFloat("horizontal", movement.x);
            animator.SetFloat("vertical", movement.y);
            animator.SetBool("idle", false);

            rb.position = new Vector2(rb.position.x + movement.x, rb.position.y + movement.y);
        }
        else
        {
            animator.SetBool("idle", true);
        }
    }
}
