using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProps : MonoBehaviour
{
    public static PlayerProps instance;
    [SerializeField] private Health health = null;

    public Health GetHealth() {
        return health;
    }
    private void OnEnable() {
        instance = this;
        health.OnDie += HandleOnDie;
    }
    private void OnDisable() {
        health.OnDie -= HandleOnDie;
    }

    private void HandleOnDie()
    {
        //do something
    }
}
