using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    float movementSpeed = 5f;
    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

            rb.position = new Vector2(rb.position.x + movement.x, rb.position.y + movement.y);
        }
    }
}
