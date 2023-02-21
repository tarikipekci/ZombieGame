using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform target;
    private NavMeshAgent _navMeshAgent;
    [SerializeField] private float chaseRange = 10f;
    private float _distanceToTarget = Mathf.Infinity;
    private bool _isProvoked;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (_isProvoked)
        {
            EngageTarget();
        }
        else if (_distanceToTarget <= chaseRange)
        {
            _isProvoked = true;
        }
    }

    private void EngageTarget()
    {
        if (_distanceToTarget >= _navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        else
        {
            AttackTarget();
        }
    }

    private void AttackTarget()
    {
        Debug.Log(name + " has seeked and is destroying " + target.name);
    }

    private void ChaseTarget()
    {
        _navMeshAgent.SetDestination(target.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}