using UnityEngine;
using UnityEngine.AI;


public class MeleeAi : MonoBehaviour
{
    //needs to stay idle and attack ofc
    //needs to retreat sometimes
    //needs to strafe in circle around the player
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public enum EnemyState { Idle, Chase, Attack, Retreat, Strafe }
    public EnemyState currentState = EnemyState.Idle;

    public Transform player;
    public float detectionRange = 15f;
    public float combatZoneRange = 5f;
    public float attackRange = 2f;
    private float distanceToPlayer => Vector3.Distance(transform.position, player.position);

    public float attackCooldown = 2f;
    private float nextAttackTime = 0f;
    private bool hasAttackedThisTurn = false;

    private float strafeTimer = 0f;
    private int strafeDirection = 1;
    private float retreatTimer = 0f;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; 
    }

    void Update()
    {
        if (currentState != EnemyState.Idle)
        {
            RotateTowardsPlayer();
        }

        switch (currentState)
        {
            case EnemyState.Idle:   HandleIdle();   break;
            case EnemyState.Chase:  HandleChase();  break;
            case EnemyState.Attack: HandleAttack(); break;
            case EnemyState.Retreat: HandleRetreat(); break;
            case EnemyState.Strafe: HandleStrafe(); break;
        }

        Debug.Log($"Current State: {currentState}");
    }

    private void HandleIdle()
    {
        // player yakınsa
        agent.ResetPath();
        if (distanceToPlayer < detectionRange)
            currentState = EnemyState.Chase;
    }

    private void HandleChase()
    {
        agent.SetDestination(player.position);

        if (distanceToPlayer < combatZoneRange) // oyuncu yakın
            DecideNextCombatMove();

        if (distanceToPlayer > detectionRange) // oyuncu uzaklaştı
            currentState = EnemyState.Idle;
    }

    private void HandleAttack()
    {
        
    }

    private void HandleRetreat()
    {
        agent.SetDestination(transform.position - (player.position - transform.position).normalized * 5f);
        retreatTimer -= Time.deltaTime;

        if (distanceToPlayer > combatZoneRange && retreatTimer <= 0) // oyuncu uzaklaştı
            currentState = EnemyState.Idle;
        if (distanceToPlayer < attackRange) // oyuncu çok yakın
            currentState = EnemyState.Attack;
        if (distanceToPlayer < combatZoneRange && distanceToPlayer > attackRange && retreatTimer <= 0) // oyuncu orta mesafede
            DecideNextCombatMove();
    }

    private void HandleStrafe()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 strafeDir = Vector3.Cross(direction, Vector3.up) * strafeDirection;
        agent.SetDestination(transform.position + strafeDir * 2f);

        strafeTimer -= Time.deltaTime;
        if (strafeTimer <= 0f)
        {
            DecideNextCombatMove();
        }
    }

    private void DecideNextCombatMove()
    {
        float x = Random.value;

        if (x <= 0.5f)  // %50 saldır
        {
            currentState = EnemyState.Attack;
        }
        else if (x <= 0.8f)  // %30 strafe
        {
            strafeTimer = Random.Range(1.5f, 3f);
            strafeDirection = Random.value > 0.5f ? 1 : -1;
            
            currentState = EnemyState.Strafe;
        }
        else  // %20 geri çekil
        {
            retreatTimer = Random.Range(2f, 4f);
            currentState = EnemyState.Retreat;
        }
    }

    private void RotateTowardsPlayer()
    {
        if (player == null) return;
        
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;
        
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 8f);
        }
    }
}
