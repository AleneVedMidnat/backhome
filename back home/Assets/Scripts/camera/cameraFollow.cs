using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static PlayerMovement;

public class cameraFollow : MonoBehaviour
{
    public Transform target;
    //public PlayerMovement player;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3 (target.position.x, target.position.y, -10f);
        transform.position = newPos;
        //if you want a delay, uncomment out playerMovement stuff and below
        //transform.position = Vector3.Slerp(transform.position, newPos, player.movementSpeed * Time.deltaTime);
    }
}
