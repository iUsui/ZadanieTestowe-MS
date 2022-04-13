using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private float flySpeed = 10.0f;
    [SerializeField] public int attackDamage = 25;

    private void Start() {
        rb.velocity = transform.forward * flySpeed;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("InvisibleWall")) { 
            Destroy(gameObject); 
            return;
        }
        if (other.CompareTag("Enemy")) {
            other.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
            Destroy(gameObject);
        }
         
    }
}
