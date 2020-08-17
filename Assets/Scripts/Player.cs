using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    private MoveSpeed speed;

    private Health health;

    private Defence defence;

    private Cursor cursor;

    private Canon gun;

    public List<Canon> gunList;

    public Transform CanonPosition;

    private int ndxgun;

    private float curTimeout;
    public float timeout = 0.5f;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        speed = GetComponent<MoveSpeed>();
        health = GetComponent<Health>();
        defence = GetComponent<Defence>();

        cursor = FindObjectOfType<Cursor>();
        navMeshAgent.updateRotation = false;

        health.onDie += Die;

        ndxgun = 0;

        SpawnGun(ndxgun);
    }

    public float GetDamage()
    {
        return gun.GetAttack().Damage;
    }

    public void Die()
    {
        speed.Speed = 0;
    }

    void Update()
    {
        Vector3 forward = cursor.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(forward.x, 0, forward.z));

        if (!health.Dead)
        {
            Move();
            Attack();
            ChangeGun();
        }
    }

    private void Move()
    {
        Vector3 dir = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow))
            dir.x = -1.0f;
        if (Input.GetKey(KeyCode.RightArrow))
            dir.x = 1.0f;
        if (Input.GetKey(KeyCode.UpArrow))
            dir.z = -1.0f;
        if (Input.GetKey(KeyCode.DownArrow))
            dir.z = 1.0f;

        navMeshAgent.velocity = dir.normalized * speed.Speed;
    }

    private void Attack()
    {
        if (Input.GetKey(KeyCode.X))
        {
            curTimeout += Time.deltaTime;
            if (curTimeout > timeout)
            {
                curTimeout = 0;
                Rigidbody bulletInstance = Instantiate(gun.bullet, gun.Barrel.position, Quaternion.identity) as Rigidbody;
                var to = gun.Barrel.forward;
                bulletInstance.velocity = to * gun.bulletSpeed;
            }
        }
        else
        {
            curTimeout = timeout + 1;
        }


    }

    public void Hit(float attack)
    {
        health.GetDamage(defence.DefenceValue, attack);
    }

    private void SpawnGun(int ndx)
    {
        var positiongun = CanonPosition.position;

        if (gun != null)
        {
            gun.transform.SetParent(null);
            Destroy(gun.gameObject);
        }

       gun =  Instantiate(gunList[ndx], positiongun, transform.rotation);

       gun.transform.SetParent(transform);

        ndxgun = ndx;
    }

    private void ChangeGun()
    {
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.W))
        {
            var ndxcurrent = ndxgun;

            var destchange = 0;

            if (Input.GetKey(KeyCode.Q))
            {
                destchange = -1;
            }

            if (Input.GetKey(KeyCode.W))
            {
                destchange = 1;
            }

            var ndx = ndxcurrent + destchange;
            if (ndx < 0)
                SpawnGun(gunList.Count - 1);

            else if (ndx > gunList.Count - 1)
                SpawnGun(0);
            else
                SpawnGun(ndx);
        }

    }               


    public Health GetHealth()
    {
        return health;
    }

    public bool IsAlive()
    {
        return !health.Dead;
    }
}
