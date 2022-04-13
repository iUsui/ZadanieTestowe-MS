using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private float flySpeed = 10.0f;
    [SerializeField] private GameObject explosionPrefab = null;
    private int attackDamage = 25;

    public static event Action<SpecialProjectile> OnSpawned;
    public static event Action<SpecialProjectile> OnDespawned;

    private void OnEnable() {
        OnSpawned?.Invoke(this);
    }
    private void OnDestroy() {
        OnDespawned?.Invoke(this);
    }

    public void SetAttackDamage(int newAttackDamage) {
        attackDamage = newAttackDamage;
    }
    private void Start() {
        rb.velocity = transform.forward * flySpeed;
    }

    public void Explode() {
        GameObject newExplosion = explosionPrefab;
        Explosion explosion = newExplosion.GetComponent<Explosion>();
        explosion.attackDamage = attackDamage;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("InvisibleWall")) { 
            Destroy(gameObject);
        }
    }
}
