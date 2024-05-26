using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeStats : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int attackDamage;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        print(currentHealth);
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        KnockedBack();

        if (currentHealth == 0)
        {
            print("enemy died");
            EnemyDied();
        }
    }
    private void EnemyDied()
    {
        //play down animation
        //enable restart UI
    }
    private void KnockedBack()
    {
        print("knocked");
    }
}
