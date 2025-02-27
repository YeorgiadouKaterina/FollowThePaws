using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{
    public Text taskText;
    void Start()
    {
        taskText = GetComponent<Text>();
    }

    void Update()
    {
        taskText.text = "Current Task: " + TaskManager.Instance.CurrentTask();
    }
}
