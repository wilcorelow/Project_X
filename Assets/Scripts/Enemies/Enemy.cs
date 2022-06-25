using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    Transform target;
    CharacterStats targetStats;
    EnemyStats myStats;

    Kamikaze kamikaze;
    MeleeCombat meleeCombat;
    RangedAttack rangedAttack;
    
    [Header("Choose an attack style")]
    public bool isKamikaze;
    public bool isRanged;
    public bool isMelee;
    [Space]
    [SerializeField] bool isLookRadius;
    [SerializeField] float lookRadius;
    [SerializeField] float lookRotationSpeed = 5f;

    bool canAttack = true;

    int isRunningHash = Animator.StringToHash("isRunning");
    int shootHash = Animator.StringToHash("Shoot");

    WaitForSeconds attackSpeed;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        agent = GetComponent<NavMeshAgent>();

        meleeCombat = GetComponent<MeleeCombat>();

        kamikaze = GetComponent<Kamikaze>();

        rangedAttack = GetComponent<RangedAttack>();

        myStats = GetComponent<EnemyStats>();

        target = PlayerManager.Instance.player.transform;
        targetStats = target.GetComponent<CharacterStats>();

        agent.speed = myStats.movementSpeed.GetValue();

        attackSpeed = new WaitForSeconds(myStats.attackSpeed.GetValue());
    }

    void FixedUpdate()
    {
        GoToTheTarget();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void GoToTheTarget()
    {
        if (target != null)
        {
            bool isRunning = animator.GetBool(isRunningHash);

            float distance = Vector3.Distance(target.position, transform.position);
            
            if (!isLookRadius && distance > agent.stoppingDistance)
            {
                agent.SetDestination(target.position);

                if(!isRunning) animator.SetBool(isRunningHash, true);

            } 
            else if (isLookRadius)
            {
                if (distance <= lookRadius)
                {
                    agent.SetDestination(target.position);

                    if (!isRunning) animator.SetBool(isRunningHash, true);
                }
            }
            if (distance <= agent.stoppingDistance)
            {
                if (isRunning) animator.SetBool(isRunningHash, false);

                agent.updateRotation = false;
                
                bool isLookingEnough = FaceTarget();
                
                if (targetStats != null && isLookingEnough)
                {
                    Attack(targetStats);
                }
            }
            else agent.updateRotation = true;
        }
    }

    bool FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;

        float dotProd = Vector3.Dot(transform.TransformDirection(Vector3.forward), direction);

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);

        if (dotProd >= 0.99) return true;
        else return false;
    }

    void Attack(CharacterStats targetStats)
    {
        if (isKamikaze)
        {
            kamikaze.Detonation(targetStats);
        }
        else if (isRanged)
        {
            if (!canAttack) return;
            animator.SetTrigger(shootHash);
            StartCoroutine(ICanAttack());
        }
        else if (isMelee)
        {
            if (!canAttack) return;
            meleeCombat.Attack(targetStats);
            StartCoroutine(ICanAttack());
        }
        else
            Debug.LogWarning("Attack style is not chosen.");
    }

    IEnumerator ICanAttack()
    {
        canAttack = false;

        yield return attackSpeed;
        
        canAttack = true;
    }
}
