using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputHandler inputHandler;
    private Locomotion locomotion;
    private AnimationHandler animationHandler;
    private InteractionHandler interactionHandler;

    float delta;
    void Awake()
    {
        //note: set all getcomponents in awake
        inputHandler = GetComponent<InputHandler>();
        locomotion = GetComponent<Locomotion>();
        interactionHandler = GetComponent<InteractionHandler>();
        animationHandler = GetComponentInChildren<AnimationHandler>();
    }
    void Update()
    {
        
        interactionHandler.Interact();
    }
    void FixedUpdate()
    {
        delta = Time.deltaTime;

        inputHandler.TickInput(delta);
        locomotion.InitializeAction(delta);

        PlayerAnimations();
    }

    void PlayerAnimations()
    {
        animationHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0f);
    }
}
