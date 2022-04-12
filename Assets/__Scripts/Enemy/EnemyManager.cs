using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    [SerializeField] private int myId = -1;

    private List<Health> myFoes = new List<Health>();
    private List<Cube> cubes = new List<Cube>();
    private List<Sphere> spheres = new List<Sphere>();
    private List<SmallSphere> smallSpheres = new List<SmallSphere>();

    public int GetEnemyId() {
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
}
