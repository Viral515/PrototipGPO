using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private bool stayOnTrigger = false;

    public void OnTriggerStay2D(Collider2D NPC)
    {
        stayOnTrigger = true;
    }
    public void OnTriggerExit2D(Collider2D NPC)
    {
        stayOnTrigger = false;
    }

    void Update()
    {
        if (stayOnTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
        }
    }
}
