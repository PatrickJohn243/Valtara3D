using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private InputHandler inputHandler;
    private Locomotion locomotion;

    private Animator animator;

    int horizontal;
    int vertical;
    void Awake()
    {
        animator = GetComponent<Animator>();
        inputHandler = GetComponentInParent<InputHandler>();
        locomotion = GetComponentInParent<Locomotion>();

        //hashcodes
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }
    public void PlayTargetAnimation(string targetAnim, bool isInteracting, float transitionDuration)
    {
        animator.SetBool("IsInteracting", isInteracting);
        animator.CrossFade(targetAnim ,transitionDuration);
    }  
    public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement)
    {
        #region Horizontal
        float h;

        if (horizontalMovement > 0 && horizontalMovement < .55f)
        {
            h = 0.5f;
        }
        else if (horizontalMovement > .55f)
        {
            h = 1f;
        }
        else if (horizontalMovement > 0 && horizontalMovement < -.55f)
        {
            h = -.5f;
        }
        else if (horizontalMovement < .55f)
        {
            h = -1f;
        }
        else
        {
            h = 0f;
        }
        #endregion

        #region Vertical
        float v;

        if (verticalMovement > 0 && verticalMovement < .25f)
        {
            v = 0.5f;
        }
        else if (verticalMovement > .25f)
        {
            v = 1f;
        }
        else if (verticalMovement > 0 && verticalMovement < -.25f)
        {
            v = -.5f;
        }
        else if (verticalMovement < .25f)
        {
            v = -1f;
        }
        else
        {
            v = 0f;
        }
        #endregion

        //updates animation value from idle-running animation in blend tree
        animator.SetFloat(horizontal, h, .15f, Time.deltaTime);
        animator.SetFloat(vertical, v, .15f, Time.deltaTime);
    }
}
