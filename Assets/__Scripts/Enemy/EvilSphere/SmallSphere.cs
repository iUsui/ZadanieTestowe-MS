using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSphere : SphereScript
{
    [SerializeField] private int damage = 10;
    public override void HandleOnDie()
    {
        Destroy(gameObject);
    }
    
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall")) {
            player.GetHealth().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void SmallSphereTookDamage() {
        enemyManager.SmallSphereBoost();
    }
}
