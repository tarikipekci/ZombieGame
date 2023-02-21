using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform target;
    private NavMeshAgent _navMeshAgent;
    [SerializeField] private float chaseRange = 10f;
    [SerializeField] private float turnSpeed = 5f;
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
        FaceTarget();
        
        if (_distanceToTarget >= _navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        else
        {
            AttackTarget();
        }
    }

    public void OnDamageTaken()
    {
        _isProvoked = true;
    }
    private void FaceTarget()
    {
        var direction = (target.position - transform.position).normalized;
        var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
    private void AttackTarget()
    {
        // ReSharper disable once Unity.PreferAddressByIdToGraphicsParams
        GetComponent<Animator>().SetBool("attack",true);
    }

    private void ChaseTarget()
    {
        // ReSharper disable once Unity.PreferAddressByIdToGraphicsParams
        GetComponent<Animator>().SetBool("attack",false);
        // ReSharper disable once Unity.PreferAddressByIdToGraphicsParams
        GetComponent<Animator>().SetTrigger("move");
        _navMeshAgent.SetDestination(target.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}