using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public List<GameObject> images;
    private void Start()
    {
        StartCoroutine(LoadingScreenCoroutine());
    }

    public void StartLoading()
    {
        StartCoroutine(LoadingScreenCoroutine());
    }
    
    IEnumerator LoadingScreenCoroutine()
    {
        GameObject.Find("Canvas/Tasks").GetComponent<TaskManagerUI>().DisableAllUI();
        //GameObject.Find("Canvas/Hint").GetComponent<CanvasGroup>().alpha = 0;
        if (GameObject.Find("Player"))
        {
            GameObject.Find("Player").GetComponent<PlayerController>().canMove = false;
        }
        
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1);
            images[i].SetActive(true);
        }

        yield return null;
        GameObject.Find("Player").GetComponent<PlayerController>().canMove = true;
        //GameObject.Find("Canvas/Hint").GetComponent<CanvasGroup>().alpha = 1;
        
        for (int i = 0; i < 5; i++)
        {
            images[i].SetActive(false);
        }
        
        gameObject.SetActive(false);
    }
}
