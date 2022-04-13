using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    [SerializeField] private int myId = -1;

    [SerializeField] private List<EnemyHealth> myFoes = new List<EnemyHealth>();
    [SerializeField] private List<Cube> cubes = new List<Cube>();
    [SerializeField] private List<SphereScript> spheres = new List<SphereScript>();
    [SerializeField] private List<SmallSphere> smallSpheres = new List<SmallSphere>();

    public int GetMyId() {
        return myId;
    }

    private void OnEnable() {
        instance = this;
        EnemyHealth.OnSpawn += HandleOnSpawn;
        EnemyHealth.OnDespawn += HandleOnDespawn;
    }

    private void OnDisable() {
        EnemyHealth.OnSpawn -= HandleOnSpawn;
        EnemyHealth.OnDespawn -= HandleOnDespawn;
    }

    private void HandleOnSpawn(EnemyHealth health)
    {
        if (health.GetOwnerId() == myId) {
            myFoes.Add(health);
            GameObject newFoe = health.gameObject;
            if (newFoe.TryGetComponent<Cube>(out Cube cube)) {
                cubes.Add(cube);
            }
            else if (newFoe.TryGetComponent<SphereScript>(out SphereScript sphere)) {
                spheres.Add(sphere);
            }
            if (newFoe.TryGetComponent<SmallSphere>(out SmallSphere smallSphere)) {
                smallSpheres.Add(smallSphere);
            }
        }
    }

    private void HandleOnDespawn(EnemyHealth health)
    {
        if (health.GetOwnerId() == myId) {
            myFoes.Remove(health);
            GameObject newFoe = health.gameObject;
            if (newFoe.TryGetComponent<Cube>(out Cube cube)) {
                cubes.Remove(cube);
            }
            else if (newFoe.TryGetComponent<SphereScript>(out SphereScript sphere)) {
                spheres.Remove(sphere);
            }
            if (newFoe.TryGetComponent<SmallSphere>(out SmallSphere smallSphere)) {
                smallSpheres.Remove(smallSphere);
            }
        }
    }

    public void SmallCubeReachedRedLine() {
        foreach (var cube in cubes) {
            EnemyHealth cubeHealth = cube.GetComponent<EnemyHealth>();
            int addHealth = (int)(cubeHealth.GetMaxHealth() * 0.1);
            cubeHealth.SetMaxHealth(cubeHealth.GetMaxHealth() + addHealth);
            cubeHealth.SetCurrentHealth(cubeHealth.GetCurrentHealth() + addHealth);
        }
    }

    public void BigCubeDied() {
        foreach (var enemy in myFoes) {
            float enemyHealth = enemy.GetCurrentHealth() / enemy.GetMaxHealth();
            if (enemyHealth < 0.5f) {
                enemy.SetCurrentHealth(enemy.GetMaxHealth());
            }
        }
    }

    public void SmallSphereBoost() {
        foreach (var sphere in smallSpheres) {
            sphere.movementSpeed += (float)(sphere.movementSpeed * 0.1);
        }
    }
    
    public void BigSphereBoost() {
        foreach (var sphere in spheres) {
            sphere.movementSpeed -= (float)(sphere.movementSpeed * 0.1);
        }
    }
}
