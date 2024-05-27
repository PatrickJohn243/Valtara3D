using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsHandler : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public int attackDamage;

    [SerializeField] private Image healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        //print(currentHealth);
        SetHealthUI();
    }
    public void TakeDamage(int amount)
    {
        print("player Damaged");
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
    private void SetHealthUI()
    {
        healthBar.fillAmount = (currentHealth * .01f);
    }
}
