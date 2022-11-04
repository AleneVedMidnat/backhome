using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPhandler : MonoBehaviour
{
    public int HP = 25;
    private Vector2 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            transform.position = startPos;
        }
    }
}
