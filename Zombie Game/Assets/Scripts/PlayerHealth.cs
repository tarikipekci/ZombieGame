using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float playerHealth = 100f;
    
    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0f)
        {
            GetComponent<DeathHandler>().HandleDeath();
        }
    }
}
