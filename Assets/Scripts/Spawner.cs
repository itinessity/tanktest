using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using System;

public class Spawner : MonoBehaviour
{
    public int MaxAliveEnemyCount;
    private int CurrentCount => CurrentEnemy.Count;

    public float Period;
    private float TimeUntilNextSpawn;

    List<GameObject> CurrentEnemy;

    public List<GameObject> EnemyTypes;

    private int prevndx;

    // Start is called before the first frame update
    void Start()
    {
        TimeUntilNextSpawn = UnityEngine.Random.Range(0, Period);
        CurrentEnemy = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentCount < MaxAliveEnemyCount)
        {
            TimeUntilNextSpawn -= Time.deltaTime;
            if (TimeUntilNextSpawn <= 0.0f)
            {
                TimeUntilNextSpawn = Period;
                CreateNewEnemy();
            }
        }

        ClearLinks();
    }

    private void ClearLinks()
    {
        var died = CurrentEnemy.Where(x => x == null).ToList();

        if (died.Count() != 0)
        {
           foreach(var d in died)
            {
                CurrentEnemy.Remove(d);
            }
        }
    }

    private void CreateNewEnemy()
    {
       var rnd = new System.Random();
       int ndx = rnd.Next(0, EnemyTypes.Count - 1);

        if (prevndx == ndx)
            ndx = Math.Min(EnemyTypes.Count - 1, ndx + 1);

        var enemytype = EnemyTypes[ndx];

       var enemy =  Instantiate(enemytype, transform.position, transform.rotation);
       CurrentEnemy.Add(enemy);

        prevndx = ndx;

    }
}
