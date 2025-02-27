using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform playerCamera = null;
    [SerializeField] private bool showCursor = false;
    [SerializeField] private float mouseSensitivity = 3.5f;
    [SerializeField] private float walkSpeed = 6.5f;
    [SerializeField] private float gravity = -13f;
    [SerializeField] private float moveSmoothTime = 0.3f;
    private Animator _animator;
    CharacterController controller = null;

    private bool _movementState;

    float velocityY;
    
    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    public bool canMove = true;
    public void Start()
    {
        controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        if(showCursor == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = showCursor;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.sceneCount != 1)
            {
                var menu = GameObject.Find("Canvas/Menu");
                if(menu.GetComponent<CanvasGroup>().alpha == 0){
                    SetMovementState(GameObject.Find("Player").GetComponent<PlayerController>().canMove);
                    GameObject.Find("Player").GetComponent<PlayerController>().canMove = false;
                    menu.GetComponent<CanvasGroup>().alpha = 1;
                    menu.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    //Cursor.lockState = CursorLockMode.None;
                    //Cursor.visible = true;
                }
                else
                {
                    
                    menu.GetComponent<CanvasGroup>().alpha = 0;
                    menu.GetComponent<CanvasGroup>().blocksRaycasts = false;
                    //Cursor.lockState = CursorLockMode.Locked;
                    //Cursor.visible = false;
                    if (ReturnMovementState())
                    {
                        GameObject.Find("Player").GetComponent<PlayerController>().canMove = true;
                    }
                    
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.X))
            TaskManager.Instance.NextTask();

        UpdateMouseLook();  
        UpdateMovement();
    }

    void UpdateMouseLook()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        if (canMove == false)
            mouseDelta = Vector2.zero;
        transform.Rotate((Vector2.up * mouseDelta.x * mouseSensitivity) );
    }

    void UpdateMovement()
    {
        
        Vector2 targetDir = new Vector2(0, Input.GetAxis("Vertical"));

        if (canMove == false)
            targetDir = Vector2.zero;
        targetDir.Normalize();
        
        currentDir = Vector2.SmoothDamp(currentDir, targetDir,ref currentDirVelocity,moveSmoothTime);
        
        if (controller.isGrounded)
            velocityY = 0f;
        velocityY += gravity * Time.deltaTime;

        if(targetDir.magnitude != 0)
            _animator.SetBool("Walking",true);
        else
            _animator.SetBool("Walking",false);

        
        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);
    }

    public void SetMovementState(bool flag)
    {
        _movementState = flag; 
    }

    public bool ReturnMovementState()
    {
        return _movementState;
    }
}
