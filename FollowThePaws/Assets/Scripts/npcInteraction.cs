using System;
using System.Collections;
using UnityEngine;

public class npcInteraction : MonoBehaviour
{
    private bool flag = false;
    
    private GameObject interactionUI;
    private GameObject tasksUI;
    public void Start()
    {
        interactionUI = GameObject.Find("Canvas/Interaction UI");
        tasksUI = GameObject.Find("Canvas/Tasks");
    }
    
        private void Update()
    {
        if (flag)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                SetInteractionUI(false);
                if (transform.parent.name.Equals("Granny") && TaskManager.Instance.CurrentTask() == TaskManager.Stages.Neighbours)
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().canMove = false;
                    tasksUI.GetComponent<TaskManagerUI>().SetChecked(GameObject.Find("Canvas/Tasks/ParkTasks/Image"));
                    TaskManager.Instance.NextTask();
                    transform.parent.GetComponent<DialogueTrigger>().TriggerDialogue();
                    Destroy(GameObject.Find("Player/Rover"));
                }
                else if (transform.parent.name.Equals("Granny") && TaskManager.Instance.CurrentTask() == TaskManager.Stages.SearchInternet)
                {
                    var dialogueMan = GameObject.Find("MANAGERS/DialogueManager");
                    dialogueMan.GetComponent<DialogueManager>().DisplayNextSentence();
                    if (dialogueMan.GetComponent<DialogueManager>().IsDialogueFinished()) //depricated
                    {
                        GameObject taskCompl = GameObject.Find("TaskCompletionMessage");
                        taskCompl.GetComponent<CanvasGroup>().alpha = 1;
                        taskCompl.transform.GetChild(0).gameObject.SetActive(true);
                        taskCompl.transform.GetChild(1).gameObject.SetActive(true);
                    }
                }


                if (transform.parent.name.Equals("Michelle") && TaskManager.Instance.CurrentTask() == TaskManager.Stages.ReturnDog)
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().canMove = false;
                    tasksUI.GetComponent<TaskManagerUI>().SetChecked(GameObject.Find("Canvas/Tasks/ParkMeetupTasks/Image"));
                    transform.parent.GetComponent<DialogueTrigger>().TriggerDialogue();
                    TaskManager.Instance.NextTask();
                }else if (transform.parent.name.Equals("Michelle") && TaskManager.Instance.CurrentTask() == TaskManager.Stages.DeletePost)
                {
                    var dialogueMan = GameObject.Find("MANAGERS/DialogueManager");
                    dialogueMan.GetComponent<DialogueManager>().DisplayNextSentence();
                    if (dialogueMan.GetComponent<DialogueManager>().IsDialogueFinished())
                    {
                        GameObject taskCompl = GameObject.Find("TaskCompletionMessage");
                        taskCompl.GetComponent<CanvasGroup>().alpha = 1;
                        taskCompl.transform.GetChild(1).gameObject.SetActive(false);
                        taskCompl.transform.GetChild(2).gameObject.SetActive(false);
                        taskCompl.transform.GetChild(0).gameObject.SetActive(true);
                        taskCompl.transform.GetChild(3).gameObject.SetActive(true);
                    }
                }
            }
        }
    }
        
    private void OnTriggerEnter(Collider other)
    {
        if (transform.parent.name.Equals("Granny") && TaskManager.Instance.CurrentTask() == TaskManager.Stages.Neighbours)
        {
            SetInteractionUI(true);
            transform.parent.gameObject.GetComponent<Animator>().SetBool("TalkingFlag", true);
            flag = true;
        }

        if (transform.parent.name.Equals("Michelle"))
        {
            SetInteractionUI(true);
            transform.parent.gameObject.GetComponent<Animator>().SetBool("TalkingFlag", true);
            flag = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (transform.parent.name.Equals("Granny") && TaskManager.Instance.CurrentTask() == TaskManager.Stages.Neighbours){
            SetInteractionUI(false);
            flag = false;
        }

        if (transform.parent.name.Equals("Michelle"))
        {
            SetInteractionUI(false);
            flag = true;
        }
    }

    private void SetInteractionUI(bool flag)
    {
        foreach (Transform child in interactionUI.transform)
        {
            child.gameObject.SetActive(flag);
        }
    }
}
