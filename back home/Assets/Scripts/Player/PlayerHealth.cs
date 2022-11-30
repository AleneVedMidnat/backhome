using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    int playerHP;
    Vector2 startPosition;
    [SerializeField] GameObject m_UI;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    public void decreaseHP(int HPdec)
    {
        playerHP -= HPdec;
        m_UI.GetComponent<UIscript>().SubtractHP(HPdec);
        if (playerHP <= 0)
        {
            //display continue screen
            transform.position = startPosition;
        }
    }
}
