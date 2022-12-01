using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class pauseUI : MonoBehaviour
{
    
    [SerializeField] GameObject m_UI;
    [SerializeField] GameObject m_player;
    float currentTime;
    private void OnEnable()
    {
        currentTime = Time.timeScale;
        Time.timeScale = 0f;
        
    }
    private void Update()
    {
        Debug.Log("the time scel is" + Time.timeScale);
    }
    public void setVariables()
    {
        m_UI.GetComponent<UIscript>().ResetElements();
        m_player.GetComponent<PlayerHealth>().continueGame();
    }

    public void ToMenu()
    {
        //scene manager to menu
    }

     public void ToContinue()
    {
        Debug.Log("pressed");
        Time.timeScale = 1.0f;
        setVariables();
        this.transform.parent.gameObject.SetActive(false);

    }

}
