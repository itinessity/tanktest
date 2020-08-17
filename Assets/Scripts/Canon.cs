using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public Transform Barrel;

    public Rigidbody bullet;

    public int bulletSpeed;

    Attack attack;

    // Start is called before the first frame update
    void Start()
    {
        attack = GetComponent<Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Attack GetAttack()
    {
        return attack;
    }
}
