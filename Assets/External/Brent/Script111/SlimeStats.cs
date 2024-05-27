using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeStats : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int attackDamage;
    [SerializeField] private int knockbackForce;

    private Rigidbody rb;
    [Header("Detection")]
    [SerializeField] private float radius = 0.05f;
    [SerializeField] private Collider[] overlappingColliders;

    private PlayerStatsHandler playerStatsHandler;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
    }
    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        //DetectPlayer();
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth == 0)
        {
            EnemyDied();
        }
        KnockedBack();
    }
    private void EnemyDied()
    {
        Destroy(gameObject);
    }
    private void KnockedBack()
    {
        //print("knocked");
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    playerStatsHandler = GetComponent<Collider>().GetComponent<PlayerStatsHandler>();
        //    print(playerStatsHandler);
        //    if (playerStatsHandler != null)
        //    {
        //        playerStatsHandler.TakeDamage(attackDamage);
        //    }
        //}
        DetectPlayer(); 
    }
    void DetectPlayer()
    {
        overlappingColliders = Physics.OverlapSphere(transform.position, radius); // Adjust the radius as needed
        foreach (Collider collider in overlappingColliders)
        {
            if (collider.CompareTag("Player"))
            {
                playerStatsHandler = collider.GetComponent<PlayerStatsHandler>();
                print(playerStatsHandler);
                if (playerStatsHandler != null)
                {
                    playerStatsHandler.TakeDamage(attackDamage);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
