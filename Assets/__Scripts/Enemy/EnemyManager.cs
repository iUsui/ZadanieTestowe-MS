using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    [SerializeField] private int myId = -1;

    [SerializeField] private List<Health> myFoes = new List<Health>();
    [SerializeField] private List<Cube> cubes = new List<Cube>();
    [SerializeField] private List<Sphere> spheres = new List<Sphere>();
    [SerializeField] private List<SmallSphere> smallSpheres = new List<SmallSphere>();

    public int GetMyId() {
        return myId;
    }

    private void OnEnable() {
        instance = this;
        Health.OnSpawn += HandleOnSpawn;
        Health.OnDespawn += HandleOnDespawn;
    }

    private void OnDisable() {
        Health.OnSpawn -= HandleOnSpawn;
        Health.OnDespawn -= HandleOnDespawn;
    }

    private void HandleOnSpawn(Health health)
    {
        if (health.GetOwnerId() == myId) {
            myFoes.Add(health);
            GameObject newFoe = health.gameObject;
            if (newFoe.TryGetComponent<Cube>(out Cube cube)) {
                cubes.Add(cube);
            }
            else if (newFoe.TryGetComponent<Sphere>(out Sphere sphere)) {
                spheres.Add(sphere);
            }
            if (newFoe.TryGetComponent<SmallSphere>(out SmallSphere smallSphere)) {
                smallSpheres.Add(smallSphere);
            }
        }
    }

    private void HandleOnDespawn(Health health)
    {
        if (health.GetOwnerId() == myId) {
            myFoes.Remove(health);
            GameObject newFoe = health.gameObject;
            if (newFoe.TryGetComponent<Cube>(out Cube cube)) {
                cubes.Remove(cube);
            }
            else if (newFoe.TryGetComponent<Sphere>(out Sphere sphere)) {
                spheres.Remove(sphere);
            }
            if (newFoe.TryGetComponent<SmallSphere>(out SmallSphere smallSphere)) {
                smallSpheres.Remove(smallSphere);
            }
        }
    }

    public void SmallCubeReachedRedLine() {
        foreach (var cube in cubes) {
            Health cubeHealth = cube.GetComponent<Health>();
            int addHealth = (int)(cubeHealth.GetMaxHealth() * 0.1);
            cubeHealth.SetMaxHealth(cubeHealth.GetMaxHealth() + addHealth);
            cubeHealth.SetCurrentHealth(cubeHealth.GetCurrentHealth() + addHealth);
        }
    }

    public void BigCubeDied() {
        Debug.Log("Im here 1..");
        foreach (var enemy in myFoes) {
            Debug.Log("Im here fe..");
            float enemyHealth = enemy.GetCurrentHealth() / enemy.GetMaxHealth();
            if (enemyHealth < 0.5f) {
                Debug.Log("Im here..");
                enemy.SetCurrentHealth(enemy.GetMaxHealth());
            }
        }
    }

    public void SmallSphereDied() {

    }
    
    public void BigSphereDied() {

    }
}
