using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : MonoBehaviour
{
    private GameObject interactionUI;
    public bool flag = false;
    public void OnTriggerEnter(Collider other)
     {
         if (TaskManager.Instance.CurrentTask() == TaskManager.Stages.ReturnDog)
         {
             SetInteractionUI(true);
             flag = true;
         }
     }

    public void OnTriggerExit(Collider other)
    {
        if (TaskManager.Instance.CurrentTask() == TaskManager.Stages.ReturnDog)
        {
            SetInteractionUI(false);
            flag = false;
        }
    }
    
    private void SetInteractionUI(bool flag)
    {
        foreach (Transform child in interactionUI.transform)
        {
            child.gameObject.SetActive(flag);
        }
    }

    public void Start()
    {
        interactionUI = GameObject.Find("Canvas/Interaction UI");
    }

    private void Update()
    {
        if (flag)
        {
            if (Input.GetKeyDown(KeyCode.Space) && TaskManager.Instance.CurrentTask() == TaskManager.Stages.ReturnDog)
            {
                StartCoroutine(TeleportAfterSeconds());
                flag = true;
            }
        }
    }
    
    IEnumerator TeleportAfterSeconds()
    {
        SetInteractionUI(false);
        MySceneManagement.Instance.loadingScreen.SetActive(true);
        MySceneManagement.Instance.loadingScreen.GetComponent<LoadingScreen>().StartLoading();
        yield return new WaitForSeconds(5);
        MySceneManagement.Instance.ChangeScene("ParkMeetUp", new Vector3(-25f, 2.6f, -11.5f));
        GameObject.Find("Canvas/Tasks").transform.GetChild(2).gameObject.SetActive(true);
    }
}
