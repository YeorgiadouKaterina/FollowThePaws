using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopChracterMovement : MonoBehaviour
{
    public GameObject player;
    // Update is called once per frame
    void Update()
    {
        player.GetComponent<PlayerController>().canMove = false;
    }
}
