using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIscript : MonoBehaviour
{
    // Start is called before the first frame update
    int HPvalue;
    int TPvalue;
    public int MaxHP = 50;
    public int MaxTP = 25;
    public  GameObject HPtext;
    public GameObject TPtext;
    public GameObject HPholder;
    public GameObject TPholder;
    Slider HPslider;
    Slider TPslider;

    void Start()
    {
        HPslider = HPholder.GetComponent<Slider>();
        TPslider = HPholder.GetComponent<Slider>();
        HPvalue = MaxHP;
        TPvalue = MaxTP;
        HPtext.GetComponent<TextMeshProUGUI>().SetText(HPvalue.ToString());// = HPvalue.ToString();
        TPtext.GetComponent<TextMeshProUGUI>().SetText(TPvalue.ToString());
        
    }

    public void AddHP(int HP)
    { 
        HPvalue += HP; 
        HPslider.value = (HPvalue/MaxHP * 100);
        HPtext.GetComponent<TextMeshProUGUI>().SetText(HPvalue.ToString());
    }
    public void AddTP(int TP) { 
        TPvalue += TP; 
        TPslider.value = (TPvalue/MaxTP * 100);
        TPtext.GetComponent<TextMeshProUGUI>().SetText(TPvalue.ToString());
    }
    public void SubtractHP(int HP) { 
        HPvalue -= HP;
        HPslider.value = (HPvalue / MaxHP * 100);
        HPtext.GetComponent<TextMeshProUGUI>().SetText(HPvalue.ToString());
    }
    public void SubtractTP(int TP) 
    { 
        HPvalue += TP;
        TPslider.value = (TPvalue / MaxTP * 100);
        TPtext.GetComponent<TextMeshProUGUI>().SetText(TPvalue.ToString());
    }
}
