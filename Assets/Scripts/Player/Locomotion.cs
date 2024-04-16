using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotion : MonoBehaviour
{
    private InputHandler inputHandler;
    private AnimationHandler animationHandler;
    private Animator animator;
    private Rigidbody rb;

    public Transform cameraTransform;

    [Header("Stats")]
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float forwardRollSpeed = 5f;
    [SerializeField] private float dampSpeed = 5f; //determines the time the player needs to rotate

    [Header("Conditions")]
    [SerializeField] private bool canRotate = true;
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool isInteracting;

    float currentVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        inputHandler = GetComponent<InputHandler>();
        animationHandler = GetComponentInChildren<AnimationHandler>();
    }
    public void InitializeAction(float delta)
    {
        HandleMove();
        HandleRotation(delta);
        HandleInteract();
        HandleRoll(delta);
        GetIsInteracting();
    }
    void HandleMove()
    {
        if (canMove)
        {
            Vector3 direction = inputHandler.moveDirection;
            direction = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * direction;
            direction.Normalize();
            transform.position += direction * Time.deltaTime * playerSpeed;         
        }
    }
    void HandleRotation(float delta)
    {
        Vector3 direction = inputHandler.moveDirection;


        if (canRotate)
        {
            if(direction != Vector3.zero)
            {
                // Rotate the input direction by the camera's rotation
                direction = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * direction;

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

                //rotates the player in a smooth angle
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, dampSpeed * delta);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }

    }
    void HandleRoll(float delta)
    {
        //remove input and use addforce parallel to the forward direction of the player
        print(isInteracting);
        if (inputHandler.rollInputPressed && !isInteracting)
        {
            DisableRotate();
            DisableMove();
            animationHandler.PlayTargetAnimation("Roll", true, .5f);
            rb.AddForce(transform.forward * forwardRollSpeed, ForceMode.Impulse);
        }
        else if(!isInteracting)
        {
            EnableRotate();
            EnableMove();
        }
    }
    void HandleInteract()
    {
        if(inputHandler.interactInputPressed)
        {
            DisableRotate();
            DisableMove();
            animationHandler.PlayTargetAnimation("Grab", true, .3f);
        }
        else if (!isInteracting)
        {
            EnableRotate();
            EnableMove();
        }
    }
    public void EnableRotate()
    {
        canRotate = true;
    }
    public void DisableRotate()
    {
        canRotate = false;
    }
    public void EnableMove()
    {
        canMove = true;
    }
    public void DisableMove()
    {
        canMove = false;
    }
    void GetIsInteracting()
    {
        isInteracting = animator.GetBool("IsInteracting");
    }
}
