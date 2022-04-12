using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int ownerId = -1;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth = 0;
    public event Action OnDie;

    public static event Action<Health> OnSpawn;
    public static event Action<Health> OnDespawn;


#region Getters
    public int GetCurrentHealth() {
        return currentHealth;
    }
    public int GetMaxHealth() {
        return maxHealth;
    }
    public int GetOwnerId() {
        return ownerId;
    }
#endregion
#region Setters
    public void SetCurrentHealth(int newCurrentHealth) {
        currentHealth = newCurrentHealth;
    }
    public void SetMaxHealth(int newMaxHealth) {
        maxHealth = newMaxHealth;
    }
    public void SetOwnerId(int newOwnerId) {
        ownerId = newOwnerId;
    }
#endregion

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
