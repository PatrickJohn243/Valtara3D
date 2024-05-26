using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public PlayerControls playerController;
    private Locomotion locomotion;

    //InputValues
    Vector2 moveInput;

    //Placeholoders
    public float moveAmount;
    public Vector3 moveDirection;

    [Header("Flags")]
    public bool rollInputPressed = false;
    public bool attackInputPressed = false;
    public bool interactInputPressed = false;

    public bool isInventoryPressed = false;
    public bool isQuestTabPressed = false;

    void Awake()
    {
        playerController = new PlayerControls();
        locomotion = GetComponent<Locomotion>();
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
    private void Start()
    {
        //UpdateLeftButton();
    }
    public void FixedTickInput(float delta)
    {
        //Player Actions
        MovementInputHandler(delta);
        RollInputHandler();
        InteractInputHandler();
             
    }
    public void TickInput(float delta)
    {
        //Features
        OpenInventoryHandler();
        OpenQuestHandler();
        AttackInputHandler();
    }

    void MovementInputHandler(float delta)
    {
        float horizontal = moveInput.x;
        float vertical = moveInput.y;
        //used for setting as a animation parameter for blend tree

        if (!locomotion.isTalking)
        {
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        }

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
    void AttackInputHandler()
    {
        if (playerController.Player.Attack.triggered)
        {
            attackInputPressed = true;
            print("attack");
        }
        else
        {
            attackInputPressed = false;
        }   
    }
    void OpenInventoryHandler()
    {
        if (playerController.Player.OpenInventory.triggered)
        {
            isInventoryPressed = !isInventoryPressed;
        }
    }
    void OpenQuestHandler()
    {
        if(playerController.Player.OpenQuest.triggered)
        {
            isQuestTabPressed = !isQuestTabPressed;
        }
    }
}

