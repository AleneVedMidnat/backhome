using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCode : MonoBehaviour
{
    public int hp = 10;

    private void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

}
