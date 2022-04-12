using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProps : MonoBehaviour
{
    [SerializeField] private Health health = null;

    private void OnEnable() {
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
