using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsHandler : MonoBehaviour
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
        //print(currentHealth);
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth == 0)
        {
            Debug.Log("Player died");
            PlayerDied();
        }
    }
    public void RestoreHealth(int amount)
    {
        currentHealth += amount;
    }
    private void PlayerDied()
    {
        //play down animation
        //enable restart UI
    }
}
