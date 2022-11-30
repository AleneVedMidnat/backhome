using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIscript : MonoBehaviour
{
    // Start is called before the first frame update
    int HPvalue;
    int TPvalue;
    int MaxHP;
    int MaxTP;
    public GameObject hholder;
    public GameObject tholder;
    TextMeshPro HPtext;
    TextMeshPro TPtext;
    public GameObject HPholder;
    public GameObject TPholder;
    Slider HPslider;
    Slider TPslider;

    void Start()
    {
        HPslider = HPholder.GetComponent<Slider>();
        TPslider = HPholder.GetComponent<Slider>();
        HPtext = hholder.GetComponent<TextMeshPro>();
        TPtext = tholder.GetComponent<TextMeshPro>();
    }

    public void AddHP(int HP)
    { 
        HPvalue += HP; 
        HPslider.value = (HPvalue/MaxHP * 100);
    }
    public void AddTP(int TP) { 
        TPvalue += TP; 
        TPslider.value = (TPvalue/MaxTP * 100);
    }
    public void SubtractHP(int HP) { 
        HPvalue -= HP;
        HPslider.value = (HPvalue / MaxHP * 100);
    }
    public void SubtractTP(int TP) 
    { 
        HPvalue += TP;
        TPslider.value = (TPvalue / MaxTP * 100);
    }
}
