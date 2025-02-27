using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DogInteraction : MonoBehaviour
{
    private GameObject interactionUI;
    private AudioSource audio;
    public void Start()
    {
        interactionUI = GameObject.Find("Canvas/Interaction UI");
        audio = transform.parent.GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space))
        {
            if (TaskManager.Instance.CurrentTask() == TaskManager.Stages.Collar)
            {
                var colarUI = GameObject.Find("Canvas/Colar");
                colarUI.GetComponent<CanvasGroup>().alpha = 1;
                
                transform.parent.GetComponent<Animator>().SetBool("Leaving",true);
                transform.parent.SetParent(GameObject.Find("Player").transform);
                
                GetComponent<Collider>().enabled = false;
                
                TaskManager.Instance.NextTask();
                
                SetInteractionUI(false);

                StartCoroutine(KillAfterSeconds());
            }

            if (TaskManager.Instance.CurrentTask() == TaskManager.Stages.ReturnDog)
            {
                transform.parent.GetComponent<Animator>().SetBool("Leaving", true);
                transform.parent.SetParent(GameObject.Find("Player").transform);

                GetComponent<Collider>().enabled = false;

                SetInteractionUI(false);

                SetCompleteBoxMessageUI(false);

                StartCoroutine(KillAfterSeconds());
            }

            audio.Play();
        }
    }

    IEnumerator KillAfterSeconds()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().canMove = false;
        yield return new WaitForSeconds(4);
        GameObject.Find("Player").GetComponent<PlayerController>().canMove = true;
        Destroy(transform.parent.gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        SetInteractionUI(true);
        SetCompleteBoxMessageUI(true);
    }

    private void OnTriggerExit(Collider other)
    {
        SetInteractionUI(false);
        SetCompleteBoxMessageUI(false);
    }

    private void SetInteractionUI(bool flag)
    {
        foreach (Transform child in interactionUI.transform)
        {
            child.gameObject.SetActive(flag);
        }
    }

    private void SetCompleteBoxMessageUI(bool flag)
    {
        if (TaskManager.Instance.CurrentTask() == TaskManager.Stages.ReturnDog)
        {
            GameObject taskCompl = GameObject.Find("TaskCompletionMessage");
            taskCompl.GetComponent<CanvasGroup>().alpha = flag ? 1 : 0;
            taskCompl.transform.GetChild(0).gameObject.SetActive(false);
            taskCompl.transform.GetChild(1).gameObject.SetActive(false);
            taskCompl.transform.GetChild(2).gameObject.SetActive(flag);

        }
    }
}