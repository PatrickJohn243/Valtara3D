using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public PlayerControls playerController;

    //InputValues
    Vector2 moveInput;
    
    //Placeholoders
    public float moveAmount;
    public Vector3 moveDirection;

    public bool rollInputPressed = false;
    public bool interactInputPressed = false;
    
    void Awake()
    {
        playerController = new PlayerControls();
    }
    void OnEnable()
    {
        playerController.Enable();

        playerController.Player.Move.performed += playerController => moveInput = playerController.ReadValue<Vector2>();
        playerController.Player.Move.canceled += playerController => moveInput = Vector2.zero;
    }
    void OnDisable()
    {
        playerController.Disable();

        playerController.Player.Move.performed -= playerController => moveInput = playerController.ReadValue<Vector2>();
        playerController.Player.Move.canceled -= playerController => moveInput = Vector2.zero;
    }
    public void TickInput(float delta)
    {
        MovementInputHandler(delta);
        RollInputHandler();
        InteractInputHandler();
    }
    void MovementInputHandler(float delta)
    {
        float horizontal = moveInput.x;
        float vertical = moveInput.y;
        //used for setting as a animation parameter for blend tree

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        //used for setting direction for locomotion
        moveDirection = new Vector3(horizontal, 0f, vertical);
    }
    void RollInputHandler()
    {
        rollInputPressed = playerController.Player.Roll.phase == InputActionPhase.Performed;
    }
    void InteractInputHandler()
    {
        interactInputPressed = playerController.Player.Interact.phase == InputActionPhase.Performed;
    }
}

