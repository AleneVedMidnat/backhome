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
        Time.timeScale = 0;
    }
    
    public void setVariables()
    {
        m_UI.GetComponent<UIscript>().ResetElements();
        m_player.GetComponent<PlayerHealth>().ResetVariables();
    }

    public void ToMenu()
    {
        //scene manager to menu
    }

     public void ToContinue()
    {
        Debug.Log("pressed");
        Debug.Log(currentTime);
        Time.timeScale = currentTime;
        setVariables();
        this.transform.parent.gameObject.SetActive(false);

    }

}
