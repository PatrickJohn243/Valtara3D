using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemy : MonoBehaviour
{
    private AttackHandler attackHandler;
    private PlayerStatsHandler playerStatsHandler;

    private SlimeStats slimeStats;

    private void Awake()
    {
        attackHandler = GetComponentInParent<AttackHandler>();
        playerStatsHandler = GetComponentInParent<PlayerStatsHandler>();
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    print(collision.collider.name);
    //    if(collision.collider.CompareTag("Enemy"))
    //    {
    //        slimeStats = collision.collider.GetComponent<SlimeStats>();

    //        if(slimeStats != null && attackHandler.isAttacking)
    //        {
    //            slimeStats.TakeDamage(playerStatsHandler.attackDamage);
    //        }
    //    }   
    //}
    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.CompareTag("Enemy"))
        {
            slimeStats = other.GetComponent<SlimeStats>();
            print(slimeStats);

            if (slimeStats != null && attackHandler.isAttacking)
            {
                slimeStats.TakeDamage(playerStatsHandler.attackDamage);
            }
        }
    }
}
