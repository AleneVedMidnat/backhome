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
        TPslider = TPholder.GetComponent<Slider>();
        HPvalue = MaxHP;
        TPvalue =  MaxTP;
        HPslider.maxValue = MaxHP;
        TPslider.maxValue = MaxTP;
        
        HPtext.GetComponent<TextMeshProUGUI>().SetText(HPvalue.ToString());// = HPvalue.ToString();
        TPtext.GetComponent<TextMeshProUGUI>().SetText(TPvalue.ToString());
        
    }

    public void AddHP(int HP)
    { 
        HPvalue += HP; 
        HPslider.value = HPvalue;
        HPtext.GetComponent<TextMeshProUGUI>().SetText(HPvalue.ToString());
    }
    public void AddTP(int TP) { 
        TPvalue += TP; 
        TPslider.value = TPvalue;
        TPtext.GetComponent<TextMeshProUGUI>().SetText(TPvalue.ToString());
    }
    public void SubtractHP(int HP) { 
        HPvalue -= HP;
        HPslider.value = HPvalue;
        HPtext.GetComponent<TextMeshProUGUI>().SetText(HPvalue.ToString());
    }
    public void SubtractTP(int TP) 
    { 
        TPvalue -= TP;
        TPslider.value = TPvalue;
        TPtext.GetComponent<TextMeshProUGUI>().SetText(TPvalue.ToString());
    }

    public void ResetElements()
    {
        HPvalue = MaxHP;
        HPslider.value = HPvalue;
        HPtext.GetComponent<TextMeshProUGUI>().SetText(HPvalue.ToString());
        TPvalue = MaxTP;
        TPslider.value = TPvalue;
        TPtext.GetComponent<TextMeshProUGUI>().SetText(TPvalue.ToString());
    }
}
