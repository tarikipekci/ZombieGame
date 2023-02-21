using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private PlayerHealth _target;
    [SerializeField] private float damage = 40f;

    private void Start()
    {
        _target = FindObjectOfType<PlayerHealth>();
    }

    public void AttackHitEvent()
    {
        if (_target == null) return;
        _target.TakeDamage(damage);
        Debug.Log("Bang bang");    
    }
}