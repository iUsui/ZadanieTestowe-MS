using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int ownerId = -1;
    [SerializeField] private int maxHealth = 100;
    private int currentHealth = 0;
    public event Action OnDie;

    public static event Action<Health> OnSpawn;
    public static event Action<Health> OnDespawn;

    public int GetOwnerId() {
        return ownerId;
    }
    public void SetOwnerId(int newOwnerId) {
        ownerId = newOwnerId;
    }
    private void Start() {
        OnSpawn?.Invoke(this);
        currentHealth = maxHealth;
    }

    private void OnDestroy() {
        OnDespawn?.Invoke(this);
    }

    public void TakeDamage(int value) {
        if (currentHealth <= 0) { return; }
        currentHealth -= value;
        if (currentHealth > 0) { return; }
        OnDie?.Invoke();
    }
}
