using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cube : MonoBehaviour
{
    [SerializeField] public float movementSpeed = 5.0f;
    [SerializeField] public Health health = null;
    

    private void OnEnable() {
        health.OnDie += HandleOnDie;
    }
    private void OnDisable() {
        health.OnDie -= HandleOnDie;
    }
    private void Start() {
        
    }

    public abstract void HandleOnDie();
}
