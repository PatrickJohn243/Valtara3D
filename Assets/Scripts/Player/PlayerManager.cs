using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputHandler inputHandler;
    private Locomotion locomotion;
    private AnimationHandler animationHandler;

    float delta;
    void Awake()
    {
        //note: set all getcomponents in awake
        inputHandler = GetComponent<InputHandler>();
        locomotion = GetComponent<Locomotion>();
        animationHandler = GetComponentInChildren<AnimationHandler>();
    }
    void Update()
    {
        delta = Time.deltaTime;

        inputHandler.TickInput(delta);
    }
    void FixedUpdate()
    {
        locomotion.InitializeAction(delta);

        PlayerAnimations();
    }

    void PlayerAnimations()
    {
        animationHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0f);
    }
}
