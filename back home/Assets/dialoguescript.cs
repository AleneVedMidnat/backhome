using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class dialoguescript : MonoBehaviour
{
    string[] dialogue = { "Press enter to continue",
                          "You've ended up on an abandoned planet, and your portal has broken down", 
                          "There are 4 portal stones in the area, you need to fight past the locals and relight them",
                          "You can use WASD to move",
                          "Press Q to attack. Press E to special attack. Press space to dash.",
                          "You can light portal stones using P.",
                          "Then come back to the portal and make your way home.",
                          "Good luck!"};
    int dialogueLine = 0;
    int maxdia = 2;
    [SerializeField] GameObject dialogueText;
    [SerializeField] GameObject characterUI;
    
    // Start is called before the first frame update
    void Start()
    {
        dialogueText.GetComponent<TextMeshProUGUI>().SetText(dialogue[0]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("pressed");
            NextLine();
        }
    }

    void NextLine()
    {
        dialogueLine++;
        if (dialogueLine <= dialogue.Length -1)
        {
            dialogueText.GetComponent<TextMeshProUGUI>().SetText(dialogue[dialogueLine]);
        }
        else
        {
            characterUI.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
