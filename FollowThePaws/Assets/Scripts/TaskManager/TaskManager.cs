using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    #region Singleton Initialization

    private static TaskManager _instance; //1 --> taskmanager

    public static TaskManager Instance => _instance;

    #endregion

    [Serializable]
    public enum Stages
    {
        Collar = 0,
        Neighbours = 1, 
        SearchInternet,
        PostOnline,
        UserVerification,
        FindUser, 
        SetMeetingPoint, //6
        ReturnDog,  //7
        DeletePost
    }

    private Stages _stage;
    [SerializeField] private GameObject tasksUI;

    // knownUI = Resources.Load<Sprite>("UI/Tasks/hint_message_tab");
    // knownUI = Resources.Load<Sprite>("UI/Tasks/success_message_tab");

    public void Start()
    {
        _instance = new TaskManager();
        _stage = Stages.Collar;
        tasksUI = GameObject.Find("Canvas/Tasks");
    }

    /// <summary>
    /// Gets the Current Task in the stages Enum
    /// </summary>
    public Stages CurrentTask()
    {
        return _stage;
    }
    
    /// <summary>
    /// Finishes the current task and goes to the next one.
    /// </summary>
    public void NextTask()
    {
        //var taskUI = GameObject.Find("Canvas/Tasks"); 
        //taskUI.GetComponent<TaskManagerUI>().SetChecked(taskUI.GetComponent<TaskManagerUI>().GetTaskUI());
        _stage++;
    }
    public Stages PreviousTask()
    {
        return _stage--;
    }

    public void SetTask(Stages stage)
    {
        _stage = stage;
    }

    public void SetTask(int stage)
    {
        _stage = (Stages) stage;
    }

    public void CheckForDoneTasks()
    {
        if (_stage == Stages.SearchInternet)
        {
            GameObject currentTask = GetCurrentTaskUI();
            currentTask.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Tasks/success_message_tab");
            currentTask.transform.GetChild(0).GetChild(0).GetComponent<Text>().enabled = true;

            GameObject taskCompl = GameObject.Find("Canvas/TaskCompletionMessage");
            taskCompl.GetComponent<CanvasGroup>().alpha = 1;
            taskCompl.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void GetHint()
    {
        GameObject currentTask = GetCurrentTaskUI();
        if (currentTask.transform.childCount == 1)
        { 
            currentTask.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Tasks/hint_message_tab");
            currentTask.transform.GetChild(0).GetChild(0).GetComponent<Text>().enabled = true;
        }
    }

    public GameObject GetCurrentTaskUI()
    {
        if (_stage == Stages.Collar || _stage == Stages.Neighbours)
        {
            return  GameObject.Find("Canvas/Tasks").transform.GetChild(0).gameObject;
        }
        if (_stage == Stages.SearchInternet || _stage == Stages.PostOnline || _stage == Stages.UserVerification
                  || _stage == Stages.FindUser || _stage == Stages.SetMeetingPoint)
        {
            return GameObject.Find("Canvas/Tasks").transform.GetChild(1).gameObject;
        }
        if (_stage == Stages.ReturnDog)
        {
            return GameObject.Find("Canvas/Tasks").transform.GetChild(2).gameObject;
        }
        if (_stage == Stages.DeletePost)
        {
            return GameObject.Find("Canvas/Tasks").transform.GetChild(3).gameObject;
        }
        
        return null;
    }
}
