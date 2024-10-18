using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStatsHandler : MonoBehaviour
{

    public GameObject playerPrefab;
    public Vector3 playerSpawnPosition;


    public int maxHealth;
    public int currentHealth;

    public int attackDamage;
    public GameObject deathUI;
    private AnimationHandler animationHandler;
    [SerializeField] private Image healthBar;
    Locomotion locomotion;
    private void Awake()
    {
        locomotion = GetComponent<Locomotion>();
        animationHandler = GetComponentInChildren<AnimationHandler>();
    }
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
        animationHandler.PlayTargetAnimation("Dying", true, 0.2f);
        StartCoroutine(OnDeathUI());
    }
    private void SetHealthUI()
    {
        healthBar.fillAmount = (currentHealth * .01f);
    }
    IEnumerator OnDeathUI()
    {
        yield return new WaitForSeconds(2f);
        deathUI.SetActive(true);
    }
    //public void RestartGame()
    //{
    //    locomotion.isInteracting = false;
    //    SceneManager.LoadScene("Level");
        
    //}
    public void RestartGame()
    {
        locomotion.isInteracting = false;

        // Destroy the existing player GameObject
        Destroy(gameObject);

        // Load the scene
        SceneManager.LoadScene("Level");

        // Instantiate a new player GameObject
        GameObject newPlayer = Instantiate(playerPrefab, playerSpawnPosition, Quaternion.identity);

        // Optionally, you can set up the new player GameObject here
        // ...
    }
}
