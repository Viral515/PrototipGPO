using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private bool stayOnTrigger = false;
    public GameObject inventoryWindow;

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
            if (Input.GetKeyDown(KeyCode.E) && (inventoryWindow.activeSelf == false))
            {
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
        }
    }
}
