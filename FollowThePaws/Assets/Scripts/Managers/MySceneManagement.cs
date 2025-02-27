using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManagement : MonoBehaviour
{

    [SerializeField] private GameObject player;
    public GameObject loadingScreen;
    /// <summary>
    /// Loads a scene and teleports the player to the exact position
    /// </summary>
    /// <param name="sceneName">The new scene.</param>
    /// <param name="position">The position of the player on the new scene.</param>
    public void ChangeScene(string sceneName, Vector3 pos)
    {
        GameObject.Find("Canvas/Tasks").GetComponent<TaskManagerUI>().DisableAllUI();

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name.Equals("EmptyScene") == false)
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i).name);
                break;
            }
        }
        SceneManager.LoadScene(sceneName,LoadSceneMode.Additive);
        if(player != null){
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = pos;
            player.GetComponent<CharacterController>().enabled = true;
        }
    }
    
    #region Singleton Initialization
    public static MySceneManagement Instance { get; private set; }

    public void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    #endregion
}
