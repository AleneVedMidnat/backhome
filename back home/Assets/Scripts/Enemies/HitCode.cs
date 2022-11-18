using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCode : MonoBehaviour
{
    public int hp = 10;

    private void Update()
    {
        Debug.Log(hp);
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

}
