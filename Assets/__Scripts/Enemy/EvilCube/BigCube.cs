using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCube : Cube
{
    [SerializeField] private int damage = 10;
    public override void HandleOnDie()
    {
        enemyManager.BigCubeDied();
        Destroy(gameObject);
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall")) {
            player.GetHealth().TakeDamage(damage);
            health.TakeDamage(health.GetCurrentHealth());
        }
    }
}
