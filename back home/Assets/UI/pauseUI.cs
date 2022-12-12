using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pauseUI : MonoBehaviour
{
    
    [SerializeField] GameObject m_UI;
    [SerializeField] GameObject m_player;

    //fade out
    bool fadeout = false;
    [SerializeField] GameObject blackImage;

    float currentTime;
    private void OnEnable()
    {
        currentTime = Time.timeScale;
        Time.timeScale = 0f;
        
    }
    private void Update()
    {
        if (fadeout == true)
        {
            
            var alpha = blackImage.GetComponent<Image>().color;
            alpha.a += 0.001f;
            Debug.Log(alpha.a);
            blackImage.GetComponent<Image>().color = alpha;
            if (alpha.a >= 1)
            {
                SceneManager.LoadScene("Start");
            }
        }
        Debug.Log("the time scel is" + Time.timeScale);
    }
    public void setVariables()
    {
        m_UI.GetComponent<UIscript>().ResetElements();
        m_player.GetComponent<PlayerHealth>().continueGame();
    }

    public void ToMenu()
    {
        fadeout = true;
    }

     public void ToContinue()
    {
        Debug.Log("pressed");
        Time.timeScale = 1.0f;
        setVariables();
        this.transform.parent.gameObject.SetActive(false);

    }

    

}
