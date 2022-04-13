using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cube : MonoBehaviour
{
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] public float movementSpeed = 5.0f;
    [SerializeField] public EnemyHealth health = null;
    [SerializeField] private new Renderer renderer = null;
    private Color color;
    [NonSerialized] public EnemyManager enemyManager = null;
    [NonSerialized] public PlayerProps player = null;
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
        player = PlayerProps.instance;
        renderer.material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    private void Update() {
        rb.velocity = Vector3.back * movementSpeed;
    }

    

    public abstract void HandleOnDie();
    public abstract void OnTriggerEnter(Collider other);
}
