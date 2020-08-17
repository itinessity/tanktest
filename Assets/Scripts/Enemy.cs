using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Player player;

    NavMeshAgent navMeshAgent;

    Animator animator;

    Attack attack;
    Health health;

    Defence defence;


    bool InCollisionWithPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
        animator = GetComponentInChildren<Animator>();
        attack = GetComponent<Attack>();
        health = GetComponent<Health>();
        defence = GetComponent<Defence>();

        health.onDie += Die;

    }

    public void Die()
    {
        animator.SetTrigger("die");
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!health.Dead)
        {
            if (InCollisionWithPlayer)
                Attack();
            else
            {
                Move();
            }
        };
    }

    private void Move()
    {
        if (player != null)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
    }

    public void Hit(float attack)
    {
        health.GetDamage(defence.DefenceValue, attack);
    }

    private void Attack()
    {
        if (player != null)
        {
            animator.SetTrigger("attack");
            player.Hit(attack.Damage);
        }
        else
        {
            InCollisionWithPlayer = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        InCollisionWithPlayer = false;

        if (other.GetComponent<Player>() == player)
            InCollisionWithPlayer = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() == player)
            InCollisionWithPlayer = false;
    }

}

