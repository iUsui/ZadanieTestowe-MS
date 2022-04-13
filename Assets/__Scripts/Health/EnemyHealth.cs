using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    PlayerProps playerProps;
    private int ownerId = -1;
    [SerializeField] private int maxHealth;
    private int currentHealth = 0;
    [SerializeField] private int scoreValue = 100;
    [SerializeField] private int experienceValue = 20;
    public event Action OnDie;

    public static event Action<EnemyHealth> OnSpawn;
    public static event Action<EnemyHealth> OnDespawn;


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
        playerProps = PlayerProps.instance;
        OnSpawn?.Invoke(this);
        currentHealth = maxHealth;
    }

    private void OnDestroy() {
        OnDespawn?.Invoke(this);
    }

    public void TakeDamage(int value) {
        if (currentHealth <= 0) { return; }
        currentHealth -= value;
        if (TryGetComponent<SmallSphere>(out SmallSphere smallSphere)) {
            smallSphere.SmallSphereTookDamage();
        }
        if (currentHealth > 0) { return; }
        playerProps.GainExperience(experienceValue);
        playerProps.GainScore(scoreValue);
        OnDie?.Invoke();
    }
}
