using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    public int comboStep = -1;
    private float lastAttackTime;
    [SerializeField] private float comboResetTime = 0.5f; 
    [SerializeField] private float attackCooldown = 1f; 

    [SerializeField] private AttackObject attackObject;

    public bool canAttack = true;

    private InputHandler inputHandler;
    private AnimationHandler animationHandler;
    private Locomotion locomotion;

    private void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        animationHandler = GetComponentInChildren<AnimationHandler>();
        locomotion = GetComponentInChildren<Locomotion>();  
    }

    private void Update()
    {
        if (inputHandler.attackInputPressed && canAttack)
        {
            HandleCombo();
        }

        if (Time.time - lastAttackTime > comboResetTime)
        {
            ResetCombo();
        }
    }

    private void HandleCombo()
    {
        lastAttackTime = Time.time;
        if (comboStep > 3)
        {
            ResetCombo();
            StartCoroutine(Cooldown());
        }
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
            string attack1 = attackObject.attackCombo[0].name;
            animationHandler.PlayTargetAnimation(attack1, true, 0.2f);
            comboStep++;
            print(attack1);
        }
    }

    private void Attack2()
    {
        // Implement the second attack logic here
        if (!locomotion.isInteracting)
        {
            string attack2 = attackObject.attackCombo[1].name;
            animationHandler.PlayTargetAnimation(attack2, true, 0.2f);
            comboStep++;
            print(attack2);
        }
    }

    private void Attack3()
    {
        // Implement the third attack logic here
        if (!locomotion.isInteracting)
        {
            string attack3 = attackObject.attackCombo[2].name;
            animationHandler.PlayTargetAnimation(attack3, true, 0.2f);
            comboStep++;
            print(attack3);
        }
    }

    private void ResetCombo()
    {
        comboStep = 0;
    }

    private IEnumerator Cooldown()
    {
        if(!locomotion.isInteracting)
        {
            canAttack = false;
            yield return new WaitForSeconds(attackCooldown);
            canAttack = true;
        }
    }
}
