using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private float flySpeed = 10.0f;
    [SerializeField] private int attackDamage = 25;

    private void Start() {
        rb.velocity = transform.forward * flySpeed;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("InvisibleWall")) { 
            Destroy(gameObject); 
            return;
        }
         
    }
}
