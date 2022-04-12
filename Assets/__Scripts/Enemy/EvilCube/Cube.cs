using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cube : MonoBehaviour
{
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] public float movementSpeed = 5.0f;
    [SerializeField] public Health health = null;
    [NonSerialized] public EnemyManager enemyManager;
    private void OnEnable() {
        health.OnDie += HandleOnDie;
    }
    private void OnDisable() {
        health.OnDie -= HandleOnDie;
    }
    public float GetMovementSpeed() {
        return movementSpeed;
    }
    private void Start() {
        enemyManager = EnemyManager.instance;
    }

    private void Update() {
        rb.velocity = Vector3.back * movementSpeed;
    }

    public abstract void HandleOnDie();
}
