using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    Transform player;

    private Animator animator;

    State currState;
    [SerializeField] Transform[] waitPoints;
    int currPoint;
    float dist2Player;
    float nextDist = 1F;

    [SerializeField] float moveRange;   //怪物追蹤玩家的距離
    [SerializeField] float attackRange;   //怪物攻擊玩家的距離
    [SerializeField] GameObject deathEnemy;
    EnemyHealth health;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        health = GetComponent<EnemyHealth>(); ;
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        animator.SetFloat("Speed", 0);

        if (waitPoints.Length > 0)
        {
            currState = State.Patrol;
            currPoint = 0;
            agent.destination = waitPoints[currPoint].position;
            agent.speed = 1f;
            animator.SetFloat("Speed", agent.speed);
        }
        else
        {
            currState = State.Idle;
        }


    }
    void Update()
    {
        dist2Player = Vector3.Distance(transform.position, player.position);
        //Debug.Log("dist2Player=" + dist2Player);
        switch (currState)
        {
            case State.Patrol:
                PatroState();
                break;
            case State.Locomotion:
                MoveState();
                break;
            case State.Attack:
                AttackState();
                break;

        }

    }
    void PatroState()
    {
        animator.SetFloat("Speed", agent.speed);

        if (dist2Player < moveRange)
        {
            currState = State.Locomotion;
            agent.speed = 3f;
            animator.SetFloat("Speed", agent.speed);
            MoveState();
        }
        else

         if (agent.remainingDistance < nextDist)
        {
            NextWaitPoint();
        }
    }
    void MoveState()
    {
        if (dist2Player < attackRange)
        {
            agent.stoppingDistance = attackRange;
            Attack();
        }
        else if (dist2Player > moveRange)
        {
            currState = State.Patrol;
            agent.destination = waitPoints[currPoint].position;
            agent.speed = 3f;
            animator.SetFloat("Speed", agent.speed);
        }
        else
            agent.destination = player.position;

    }
    void AttackState()
    {

        if (dist2Player < attackRange)
        {
            Attack();

        }
        else
        {

            currState = State.Patrol;
            agent.speed = 1;
            NextWaitPoint();
            agent.destination = waitPoints[currPoint].position;
            animator.SetFloat("Speed", agent.speed);
        }
    }
    void Attack()
    {

        if (health.Attack())
        {
            float ang = Vector3.Angle(transform.forward, to: player.position - transform.position);
            transform.RotateAroundLocal(Vector3.up, ang);
            animator.SetBool("Attack", true);
            currState = State.Attack;
            agent.speed = 0;
            animator.SetFloat("Speed", agent.speed);
        }
        else
            animator.SetBool("Attack", false);
    }
    


    void NextWaitPoint()
    {
        int id;
        do
        {
            id = Random.Range(0, waitPoints.Length);
        } while (id == currPoint);
        currPoint = id;
        agent.destination = waitPoints[currPoint].position;
    }

}