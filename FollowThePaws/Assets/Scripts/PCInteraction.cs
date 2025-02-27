using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInteraction : MonoBehaviour
{
    private GameObject interactionUI;
    private GameObject cameraPC;

    public void Start()
    {
        interactionUI = GameObject.Find("Canvas/Interaction UI");
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            cameraPC = GameObject.Find("CameraPC");
            cameraPC.GetComponent<Camera>().enabled = true;
            GameObject.Find("Player").GetComponent<CharacterController>().enabled = false;
            GameObject.Find("Player").GetComponent<PlayerController>().canMove = false;
            SetInteractionUI(false);
            GetComponent<Collider>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SetInteractionUI(true);
    }

    private void OnTriggerExit(Collider other)
    {
        SetInteractionUI(false);
    }

    private void SetInteractionUI(bool flag)
    {
        foreach (Transform child in interactionUI.transform)
        {
            child.gameObject.SetActive(flag);
        }
    }
}

