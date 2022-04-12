using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Small cube properties")]
    [SerializeField] private GameObject smallCubePrefab = null;
    [SerializeField] private float smallCubeSpawnRatio = 0.5f;
    [Header("Big cube properties")]
    [SerializeField] private GameObject bigCubePrefab = null;
    [SerializeField] private float bigCubeSpawnRatio = 0.15f;
    [Header("Small sphere properties")]
    [SerializeField] private GameObject smallSpherePrefab = null;
    [SerializeField] private float smallSphereSpawnRatio = 0.25f;
    [Header("Big sphere properties")]
    [SerializeField] private GameObject bigSpherePrefab = null;
    [SerializeField] private float bigSphereSpawnRatio = 0.1f;
    [Header("Spawn points")]
    [SerializeField] private Transform bottomLeftCornerPoint = null;
    [SerializeField] private Transform topRightCornerPoint = null;
    [Header("Other properties")]
    [SerializeField] private float minTimeToSpawn = 0.25f;
    [SerializeField] private float maxTimeToSpawn = 1.0f;
    
    private bool canSpawn = true;

    private EnemyManager enemyManager = null;
    private Health smallCubeHealthScript = null;
    private Cube smallCubeScript = null;

    private void Start() {
        if (minTimeToSpawn > maxTimeToSpawn) { 
            this.enabled = false;
            throw new Exception("max time to spawn has to be bigger than min time to spawn");
        }
        enemyManager = EnemyManager.instance;
        smallCubeHealthScript = smallCubePrefab.GetComponent<Health>();
        smallCubeScript = smallCubePrefab.GetComponent<Cube>();
    }

    private void Update() {
        if (canSpawn) {
            float randomNumber = UnityEngine.Random.Range(0.0f, 1.0f);
            Vector3 bottomLeftPoint = bottomLeftCornerPoint.position;
            Vector3 topRightPoint = topRightCornerPoint.position;
            Vector3 spawnPosition = new Vector3(
                UnityEngine.Random.Range(bottomLeftPoint.x, topRightPoint.x),
                0f,
                UnityEngine.Random.Range(bottomLeftPoint.z, topRightPoint.z)
            );
            if (randomNumber <= smallCubeSpawnRatio) {
                GameObject newSmallCube = smallCubePrefab;
                newSmallCube.GetComponent<Health>().SetOwnerId(enemyManager.GetMyId());
                Instantiate(smallCubePrefab, spawnPosition, smallCubePrefab.transform.rotation);
            }
            else if (randomNumber > smallCubeSpawnRatio && randomNumber < (smallCubeSpawnRatio+bigCubeSpawnRatio)) {
                GameObject newBigCube = bigCubePrefab;
                newBigCube.GetComponent<Health>().SetOwnerId(enemyManager.GetMyId());
                Cube cube = newBigCube.GetComponent<Cube>();
                cube.movementSpeed = smallCubeScript.GetMovementSpeed() * 0.8f;
                cube.health.SetMaxHealth(smallCubeHealthScript.GetMaxHealth() + (smallCubeHealthScript.GetMaxHealth()/2));
                Instantiate(newBigCube, spawnPosition, smallCubePrefab.transform.rotation);
            }
            else if (randomNumber > (smallCubeSpawnRatio + bigCubeSpawnRatio) && 
                randomNumber < (smallCubeSpawnRatio + bigCubeSpawnRatio + smallSphereSpawnRatio)) {
                //spawn small sphere
                Debug.Log("Spawning small sphere");
            }
            else if (randomNumber > (smallCubeSpawnRatio + bigCubeSpawnRatio + smallSphereSpawnRatio) &&
                randomNumber <= 1.0f) {
                //spawn big sphere
                Debug.Log("Spawning big sphere");
            }

            StartCoroutine(SetCanSpawn());
        }
    }

    private IEnumerator SetCanSpawn() {
        canSpawn = false;
        float randomTime = UnityEngine.Random.Range(minTimeToSpawn, maxTimeToSpawn);
        yield return new WaitForSeconds(randomTime);
        canSpawn = true;
    }

    


}
