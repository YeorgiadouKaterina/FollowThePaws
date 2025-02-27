using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InterfaceManagementPC : MonoBehaviour
{
    int textNum, response;
    IEnumerator coroutine;

    private void Start()
    {
        textNum = -1;
        response = -1;

        coroutine = popUpReaction();
    }

    public void EnableGameObject(GameObject go)
    {
        go.SetActive(true);
    }
    
    public void DisableGameObject(GameObject go)
    {
        go.SetActive(false);
    }

    public void ChangeColourToGrey(GameObject go)
    {
        go.GetComponent<Image>().color = Color.grey;
    }
    
    
    public void ChangeColourToWhite(GameObject go)
    {
        go.GetComponent<Image>().color = Color.white;
    }

    public void ChangeColourToGreyTransparent(GameObject go)
    {
        go.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.4f);
    }

    public void ChangeColourToWhiteTransparent(GameObject go)
    {
        go.GetComponent<Image>().color = new Color(1, 1, 1, 0f);
    }

    public void NonClickable(GameObject go)
    {
        Destroy(go.GetComponent<EventTrigger>());
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MuteButton(bool flag)
    {
        var soundComponent = GameObject.Find("Canvas");
        soundComponent.GetComponent<AudioSource>().mute = !flag;
        if (GameObject.Find("Rover")) GameObject.Find("Rover").GetComponent<AudioSource>().mute = !flag;
    }

    public void ReturnToGameButton()
    {
        var menu = GameObject.Find("Canvas/Menu");
        menu.GetComponent<CanvasGroup>().alpha = 0;
        menu.GetComponent<CanvasGroup>().blocksRaycasts = false;
        var returnState = GameObject.Find("Player").GetComponent<PlayerController>().ReturnMovementState();
        if (returnState)
        {
            GameObject.Find("Player").GetComponent<PlayerController>().canMove = true;
        }
    }

    public void TeleportPark()
    {
        MySceneManagement.Instance.ChangeScene("Park",new Vector3(-25f,10f,-11.5f));
    }

    public void TeleportHouse()
    {
        MySceneManagement.Instance.ChangeScene("House",new Vector3(-25f,10f,-11.5f));
    }

    public void ChatTimerOn(GameObject go)
    {
        //Start the coroutine
        StartCoroutine(TimerOn(go));
    }
    IEnumerator TimerOn(GameObject go)
    {
        yield return new WaitForSeconds(5);
        go.SetActive(true);
    }

    public void ChatTimerOff(GameObject go)
    {
        //Start the coroutine
        StartCoroutine(TimerOff(go));
    }
    IEnumerator TimerOff(GameObject go)
    {
        yield return new WaitForSeconds(5);
        go.SetActive(false);
    }

    public void SetAnnotation()
    {
        var taskUI = GameObject.Find("Canvas/Tasks"); 
        taskUI.GetComponent<TaskManagerUI>().SetChecked(taskUI.GetComponent<TaskManagerUI>().GetTaskUI());
    }

    public void NextTask()
    {
        TaskManager.Instance.NextTask();
    }
    
    public void SetCustomTask(int stage)
    { 
        TaskManager.Instance.SetTask(stage);
    }

    public void ActivatePlayerCamera()
    {
        var cameraPlayer = GameObject.Find("Player/Camera");
        cameraPlayer.GetComponent<Camera>().enabled = true;
        GameObject.Find("Player").GetComponent<CharacterController>().enabled = true;
        GameObject.Find("Player").GetComponent<PlayerController>().canMove = true;
        TaskManager.Instance.SetTask(TaskManager.Stages.ReturnDog);
    }

    public void SetPlayerMovement(bool flag)
    {
        GameObject.Find("Player").GetComponent<PlayerController>().canMove = flag;
    }

    public void SetTask(TaskManager.Stages stage)
    {
        TaskManager.Instance.SetTask(stage);
    }

    public void TeleportWithButtonMessage()
    {
        var dialogueMan = GameObject.Find("MANAGERS/DialogueManager");
        if (dialogueMan.GetComponent<DialogueManager>().IsDialogueFinished() == false)
            return;
        
        if (TaskManager.Instance.CurrentTask() == TaskManager.Stages.SearchInternet)
        {
            StartCoroutine(TeleportAfterSeconds("House"));
        }
        else if (TaskManager.Instance.CurrentTask() == TaskManager.Stages.ReturnDog)
        {
            StartCoroutine(TeleportAfterSeconds("ParkMeetUp"));
        }
        else if (TaskManager.Instance.CurrentTask() == TaskManager.Stages.DeletePost)
        {
            StartCoroutine(TeleportAfterSeconds("HouseDelete"));
        }
        else
        {
            Debug.Log($"Not implemented yet.");
        }
    }

    public void CorrectFeedback(int text)
    {
        GameObject instFeedback = GameObject.Find("InstantFeedbackMessage");
        GameObject sceneFeedback;

        textNum = text;
        response = 0;

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            if (instFeedback.GetComponent<CanvasGroup>().alpha == 1)
            {

                Debug.Log($"CoroutineStoped");
                sceneFeedback = instFeedback.transform.GetChild(0).gameObject; //To apply the disablement of all the messages for other scenes except "House", set properly this command inside the for loop.
                for (int i = 0; i <= 1; i++)
                {
                    sceneFeedback.transform.GetChild(i).gameObject.SetActive(false);
                    DisableFeedbackMessage(sceneFeedback.transform.GetChild(i).gameObject);
                }
            }
            coroutine = popUpReaction();
            StartCoroutine(coroutine);
        }
        else
        {
            coroutine = popUpReaction();
            StartCoroutine(coroutine);
        }
    }

    public void IncorrectFeedback(int text)
    {
        GameObject instFeedback = GameObject.Find("InstantFeedbackMessage");
        GameObject sceneFeedback;

        textNum = text;
        response = 1;

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            if (instFeedback.GetComponent<CanvasGroup>().alpha == 1)
            {

                Debug.Log($"CoroutineStoped");
                sceneFeedback = instFeedback.transform.GetChild(0).gameObject; //To apply the disablement of all the messages for other scenes except "House", set properly this command inside the for loop.
                for (int i = 0; i <= 1; i++)
                {
                    sceneFeedback.transform.GetChild(i).gameObject.SetActive(false);
                    DisableFeedbackMessage(sceneFeedback.transform.GetChild(i).gameObject);
                }
            }
            coroutine = popUpReaction();
            StartCoroutine(coroutine);
        }
        else
        {
            coroutine = popUpReaction();
            StartCoroutine(coroutine);
        }
    }

    public void DisableFeedbackMessage(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void CompletionBoxWithButtonMessage()
    {
        var dialogueMan = GameObject.Find("MANAGERS/DialogueManager");
        if (dialogueMan.GetComponent<DialogueManager>().IsDialogueFinished() == false)
            return;

        GameObject taskCompl = GameObject.Find("TaskCompletionMessage");
        taskCompl.GetComponent<CanvasGroup>().alpha = 1;
        if (TaskManager.Instance.CurrentTask() == TaskManager.Stages.SearchInternet)
        {
            taskCompl.transform.GetChild(0).gameObject.SetActive(true);
            taskCompl.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (TaskManager.Instance.CurrentTask() == TaskManager.Stages.DeletePost)
        {
            taskCompl.transform.GetChild(1).gameObject.SetActive(false);
            taskCompl.transform.GetChild(2).gameObject.SetActive(false);
            taskCompl.transform.GetChild(0).gameObject.SetActive(true);
            taskCompl.transform.GetChild(3).gameObject.SetActive(true);
        }
        else
        {
            taskCompl.transform.GetChild(0).gameObject.SetActive(false);
            taskCompl.transform.GetChild(3).gameObject.SetActive(false);
            taskCompl.SetActive(false);
            var message = taskCompl.transform.GetChild(4).gameObject;
            ChatTimerOn(taskCompl);
            ChatTimerOn(message);
        }
        
    }

    public void TaskCompletionMessage()
    {
        if (TaskManager.Instance.CurrentTask() == TaskManager.Stages.SearchInternet)
        {
            GameObject taskCompl = GameObject.Find("TaskCompletionMessage");
            taskCompl.GetComponent<CanvasGroup>().alpha = 0;
            taskCompl.transform.GetChild(0).gameObject.SetActive(false);
            taskCompl.transform.GetChild(1).gameObject.SetActive(false);
            StartCoroutine(TeleportAfterSeconds("House"));
        }
    }
    
    IEnumerator TeleportAfterSeconds(string sceneName)
    {
        var tasksUI = GameObject.Find("Canvas/Tasks");

        MySceneManagement.Instance.loadingScreen.SetActive(true);
        MySceneManagement.Instance.loadingScreen.GetComponent<LoadingScreen>().StartLoading();
        yield return new WaitForSeconds(5);
        MySceneManagement.Instance.ChangeScene(sceneName,new Vector3(-13f,2f,-7.3f));
        
        if(sceneName.Equals("HouseDelete"))
            tasksUI.transform.GetChild(3).gameObject.SetActive(true);
        else
        {
            yield return new WaitForSeconds(1);
            tasksUI.transform.GetChild(1).gameObject.SetActive(true);
        }

    }

    public void ShowTwoNextHouseActions()
    {
        StartCoroutine(SlowlyAppear());
    }

    IEnumerator SlowlyAppear()
    {
        yield return new WaitForSeconds(3f);
        var go = GameObject.Find("Canvas/Tasks/House Tasks").gameObject;
        go.transform.GetChild(0).gameObject.SetActive(false);
        go.transform.GetChild(1).gameObject.SetActive(false);
        
        go.transform.GetChild(2).gameObject.SetActive(true);
        go.transform.GetChild(3).gameObject.SetActive(true);
    }

    IEnumerator popUpReaction()
    {
        GameObject instFeedback = GameObject.Find("InstantFeedbackMessage");
        GameObject sceneFeedback;

        instFeedback.GetComponent<CanvasGroup>().alpha = 1;
        
        if (TaskManager.Instance.CurrentTask() == TaskManager.Stages.DeletePost)
            sceneFeedback = instFeedback.transform.GetChild(1).gameObject;
        else
            sceneFeedback = instFeedback.transform.GetChild(0).gameObject;

        sceneFeedback.SetActive(true);
        sceneFeedback.transform.GetChild(response).gameObject.SetActive(true);
        sceneFeedback.transform.GetChild(response).GetChild(textNum).gameObject.SetActive(true);
        yield return new WaitForSeconds(10);
        Debug.Log($"yield finished");
        sceneFeedback.SetActive(false);
        sceneFeedback.transform.GetChild(response).gameObject.SetActive(false);
        sceneFeedback.transform.GetChild(response).GetChild(textNum).gameObject.SetActive(false);
        
        instFeedback.GetComponent<CanvasGroup>().alpha = 0;
        coroutine = null;
        //textNum = -1;
        //response = -1;
    }
}