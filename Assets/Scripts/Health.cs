using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float MaxHealth;

    private float currentHealth;

    public bool Dead;

    public UnityAction onDie;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        Dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDamage(float defence, float attack)
    {
        currentHealth -= attack * defence;

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        if (Dead)
            return;

        if (onDie != null)
            Dead = true;
            onDie.Invoke();
    }

    public float GetHeath()
    {
        return currentHealth;
    }
}
