using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float hitPoints = 100f;
    
    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
