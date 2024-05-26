using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    private InputHandler inputHandler;
    private Locomotion locomotion;
    private AnimationHandler animationHandler;
    private InteractionHandler interactionHandler;

    private float delta;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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

        inputHandler.FixedTickInput(delta);
        inputHandler.TickInput(delta);
        locomotion.InitializeAction(delta);

        PlayerAnimations();
    }

    void PlayerAnimations()
    {
        animationHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0f);
    }
    public static PlayerManager GetPlayerManager()
    {
        return instance;
    }
}
