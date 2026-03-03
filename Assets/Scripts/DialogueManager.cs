using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Dialogue currentDialogue;

    private int _index= -1;

    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (currentDialogue==null || currentDialogue.dialogue.Length==0)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementDialogue()
    {
        _index++;
        
        //print("index: " + _index);
        if (currentDialogue.dialogue.Length > _index)
        {
            dialogueText.text = currentDialogue.dialogue[_index];
            nameText.text = currentDialogue.speakerName[_index];
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
