using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueSpace;
    public Text dialogueText;
    public Text nameText;
    public GameObject nextButton;
    public GameObject endButton;

    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    

    public void StartDialogue(Dialogue dialogue) 
    {
        dialogueSpace.SetActive(true);
        Time.timeScale = 0.0f;

        nextButton.SetActive(true);
        endButton.SetActive(false);
        sentences.Clear();

        foreach (string sentence in dialogue.sentencesAndNames)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 1)
        {
            nextButton.SetActive(false);
            endButton.SetActive(true);
        }
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string name = sentences.Dequeue();
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        nameText.text = name;
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        dialogueSpace.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
