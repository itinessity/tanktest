using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

        void OnTriggerEnter(Collider coll)
	{
       if (coll !=  null && coll.GetComponent<Enemy>() != null)
                coll.transform.GetComponent<Enemy>().Hit(player.GetDamage());

        Destroy(gameObject);
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() == null)
            Destroy(gameObject);
    }
}
