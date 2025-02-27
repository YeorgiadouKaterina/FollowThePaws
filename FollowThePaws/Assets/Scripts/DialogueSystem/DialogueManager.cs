using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> sentences;
    public Queue<string> npcSentences;
    public Text nameText;
    public Text dialogueText;
    public Animator animator;

    private string npcName;
    private string name;
    private void Start()
    {
        sentences = new Queue<string>();
        npcSentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    { 
        animator.SetBool("IsOpen",true);
        name = dialogue.name;
        npcName = dialogue.nameNPC;
        
        sentences.Clear();
        npcSentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        foreach (var sentence in dialogue.sentencesNPC)
        {
            npcSentences.Enqueue(sentence);
        }
        
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0 && npcSentences.Count == 0)
        {
            EndDialogue();
            return; 
        }

        string sentence = "I am a test";
        if (sentences.Count == npcSentences.Count)
        {
            nameText.text = name;        
            sentence = sentences.Dequeue();
        }
        else
        {
            nameText.text = npcName;
            sentence = npcSentences.Dequeue();
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (var letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            for(int i = 0; i < 5; i++)
                yield return null;
        }
    }
    void EndDialogue()
    {
        animator.SetBool("IsOpen",false);
    }

    public bool IsDialogueFinished()
    {
        if (sentences.Count == 0 && npcSentences.Count == 0 && animator.GetBool("IsOpen") == false)
            return true;

        return false;
    }
}
