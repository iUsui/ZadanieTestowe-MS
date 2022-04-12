using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<Health> myFoes = new List<Health>();
    private List<Cube> cubes = new List<Cube>();

    private void OnEnable() {
        Health.OnSpawn += HandleOnSpawn;
        Health.OnDespawn += HandleOnDespawn;
    }

    private void OnDisable() {
        Health.OnSpawn -= HandleOnSpawn;
        Health.OnDespawn -= HandleOnDespawn;
    }

    private void HandleOnSpawn(Health health)
    {
        
    }

    private void HandleOnDespawn(Health health)
    {
        
    }
}
