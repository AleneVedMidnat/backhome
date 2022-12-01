using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 50;
    int playerHP;
    Vector2 startPosition;
    [SerializeField] GameObject m_UI;
    [SerializeField] GameObject m_pauseScreen;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        playerHP = maxHealth;
        m_UI.GetComponent<UIscript>().MaxHP = maxHealth;
    }

    public void decreaseHP(int HPdec)
    {
        playerHP -= HPdec;
        m_UI.GetComponent<UIscript>().SubtractHP(HPdec);
        if (playerHP <= 0)
        {
            m_pauseScreen.SetActive(true);
            
        }
    }

    public void ResetVariables()
    {
        playerHP = maxHealth;
        gameObject.GetComponent<pmn>().TP = 25;
    }

    public void continueGame()
    {
        ResetVariables();
        transform.position = startPosition;
    }
}
