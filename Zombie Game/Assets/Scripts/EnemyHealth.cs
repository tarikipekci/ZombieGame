using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float hitPoints = 100f;
    
    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        
        hitPoints -= damage;
        if (hitPoints <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
