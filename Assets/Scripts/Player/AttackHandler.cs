using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    public AudioSource attackAudio;
    public AudioClip attackClip;
    [SerializeField] private GameObject weapon;
    private bool setToggle;

    public int comboStep = -1;
    private float lastAttackTime;
    [SerializeField] private float comboResetTime = 0.5f; 

    [SerializeField] private AttackObject attackObject;

    public bool isAttacking = false;

    private InputHandler inputHandler;
    private AnimationHandler animationHandler;
    private Locomotion locomotion;
    private PlayerStatsHandler playerStatsHandler;

    private void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        animationHandler = GetComponentInChildren<AnimationHandler>();
        locomotion = GetComponent<Locomotion>(); 
        playerStatsHandler = GetComponent<PlayerStatsHandler>();
        
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && weapon != false && !locomotion.isTalking)
        {
            HandleCombo();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleWeaponItem();
        }
        if (Time.time - lastAttackTime > comboResetTime)
        {
            ResetCombo();
        }
        //if (!locomotion.isInteracting)
        //{
        //    isAttacking = false;
        //}
    }

    private void HandleCombo()
    {
        lastAttackTime = Time.time;
        switch (comboStep)
        {
            case 0:
                Attack1();
                break;
            case 1:
                Attack2();
                break;
            case 2:
                Attack3();
                break;
        }
    }
    private void Attack1()
    {
        // Implement the first attack logic here
        if (!locomotion.isInteracting)
        {
            //isAttacking = true;
            string attack1 = attackObject.attackCombo[0].name;
            animationHandler.PlayTargetAnimation(attack1, true, 0.2f);
            comboStep++;
        }   
    }

    private void Attack2()
    {
        // Implement the second attack logic here
        if (!locomotion.isInteracting)
        {
            //isAttacking = true;
            string attack2 = attackObject.attackCombo[1].name;
            animationHandler.PlayTargetAnimation(attack2, true, 0.2f);
            comboStep++;
            PlayAttackAudio();
        }
    }
    private void Attack3()
    {
        // Implement the third attack logic here
        if (!locomotion.isInteracting)
        {
            //isAttacking = true;
            string attack3 = attackObject.attackCombo[2].name;
            animationHandler.PlayTargetAnimation(attack3, true, 0.2f);
            comboStep++;
            PlayAttackAudio();
            ResetCombo();
        }
    }
    private void ResetCombo()
    {
        comboStep = 0;
    }
    private void ToggleWeaponItem()
    {
        setToggle = !setToggle;

        if (setToggle)
        {
            weapon.SetActive(true);
        }
        else
        {
            weapon.SetActive(false);
        }
    }
    void PlayAttackAudio()
    {
        attackAudio.clip = attackClip;
        attackAudio.Play();
    }
}
