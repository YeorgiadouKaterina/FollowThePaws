using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TaskManagerUI : MonoBehaviour
{
    public Sprite AnnotationUI, QuestionmarkUI, CheckUI;
    public GameObject currentTaskUI;
    public GameObject hintText;

    public void Start()
    {
        hintText = GameObject.Find("Canvas/Hint/HintCounts");
    }

    public GameObject GetTaskUI()
    {
        if (TaskManager.Instance.CurrentTask() == TaskManager.Stages.Collar ||
            TaskManager.Instance.CurrentTask() == TaskManager.Stages.Neighbours)
        {
            return transform.GetChild(0).GetChild(0).gameObject;
        }

        if (TaskManager.Instance.CurrentTask() == TaskManager.Stages.SearchInternet)
        {
            return transform.GetChild(1).GetChild(0).gameObject;
        }

        if (TaskManager.Instance.CurrentTask() == TaskManager.Stages.PostOnline
           )
        {
            return transform.GetChild(1).GetChild(1).gameObject;
        }

        if (TaskManager.Instance.CurrentTask() == TaskManager.Stages.UserVerification ||
            TaskManager.Instance.CurrentTask() == TaskManager.Stages.FindUser)
        {
            return transform.GetChild(1).GetChild(2).gameObject;
        }

        if (TaskManager.Instance.CurrentTask() == TaskManager.Stages.SetMeetingPoint)
        {
            return transform.GetChild(1).GetChild(3).gameObject;
        }

        if (TaskManager.Instance.CurrentTask() == TaskManager.Stages.ReturnDog)
        {
            return transform.GetChild(2).GetChild(0).gameObject;
        }

        if (TaskManager.Instance.CurrentTask() == TaskManager.Stages.DeletePost)
        {
            return transform.GetChild(3).GetChild(0).gameObject;
        }

        return null;
    }

    public void SetAnnotation(GameObject ui)
    {
        ui.GetComponent<Image>().sprite = AnnotationUI;
        ui.transform.GetChild(0).gameObject.GetComponent<Text>().enabled = true;
    }

    public void SetChecked(GameObject ui)
    {
        ui.GetComponent<Image>().sprite = CheckUI;
        ui.transform.GetChild(0).gameObject.GetComponent<Text>().enabled = true;
    }

    public void DisableAllUI()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
    }

    public void Update()
    {
        Debug.Log($"Current Task: {TaskManager.Instance.CurrentTask()}");
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (GetTaskUI().GetComponent<Image>().sprite != AnnotationUI && hintText.GetComponent<TimerCountdown>().TimerOn == false)
            {
                SetAnnotation(GetTaskUI());
                hintText.GetComponent<TimerCountdown>().SetTimer(true);
            }
        }
    }
}