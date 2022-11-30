using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class AIWandering : MonoBehaviour
{
    public NavMeshAgent agent;

    public EnemyAnimator enemyAnim;

    [Range(0, 30)] public float speed;
    [Range(1, 10)] public float walkRadius;
    [Range(1, 10)] public float attackRadius;
    [Range(1, 10)] public float chaseRadius;


    [SerializeField] Collider[] col;
    [SerializeField] LayerMask layer;

    public float waitTime;

    float timer;
    public bool wandering;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DetectPlayer",1f,0.2f);

        agent = GetComponent<NavMeshAgent>();
        enemyAnim = GetComponent<EnemyAnimator>();

        if(agent != null)
        {
            agent.speed = speed;
            agent.SetDestination(RandomNavMeshLocation());
        }
        wandering = true;
    }

    public void DetectPlayer()
    {
        col = Physics.OverlapSphere(transform.position, attackRadius, layer);
    }

    public Vector3 RandomNavMeshLocation()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPostion = Random.insideUnitSphere * walkRadius;
        randomPostion += transform.position;
        if(NavMesh.SamplePosition(randomPostion, out NavMeshHit hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > waitTime)
        {
            if (!wandering)
                return;
            if (agent != null && agent.remainingDistance <= agent.stoppingDistance)
            {
                //enemyAnim.OnWander(true);
                agent.SetDestination(RandomNavMeshLocation());
            }
            timer = 0;
        }
    }
}
