using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private int attackDamage = 25;
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private GameObject projectilePrefab = null;
    [SerializeField] private GameObject specialProjectilePrefab = null;
    [SerializeField] private float specialProjectileCooldown = 2.0f;
    [SerializeField] private TMP_Text textCooldown = null;
    private bool canShootSpecialProjectile = true;
    private float shootTime = 0;
    
    private List<SpecialProjectile> specialProjectiles = new List<SpecialProjectile>();
    

#region Getters
#endregion
#region Setters
    public void SetAttackDamage(int newAttackDamage) {
        attackDamage = newAttackDamage;
    }
#endregion

    private Controls controls;
    private Vector2 previousInput;
    private void OnEnable() {
        instance = this;
        controls = new Controls();
        controls.Player.PlayerMovement.performed += SetPreviousInput;
        controls.Player.PlayerMovement.canceled += SetPreviousInput;
        controls.Player.PlayerShotting.started += _ => ShotProjectile();
        controls.Enable();
        SpecialProjectile.OnSpawned += HandleOnSpawnedSpecialProjectile;
        SpecialProjectile.OnDespawned += HandleOnDespawnedSpecialProjectile;
    }
    private void OnDisable() {
        SpecialProjectile.OnSpawned -= HandleOnSpawnedSpecialProjectile;
        SpecialProjectile.OnDespawned -= HandleOnDespawnedSpecialProjectile;
    }

    private void Update() {
        Vector3 currentPosition = transform.position;
        if (previousInput != Vector2.zero) {
            currentPosition += new Vector3(previousInput.x, 0f, 0f) * movementSpeed * Time.deltaTime;
        }
        transform.position = currentPosition;
        if (Input.GetKeyDown(KeyCode.Y) && canShootSpecialProjectile) {
            ShootingSpecialProjectile();
        }
        else if (!canShootSpecialProjectile) {
            float displayNumber = specialProjectileCooldown - (Time.realtimeSinceStartup - shootTime);
            textCooldown.text = displayNumber.ToString("F2");
            if (displayNumber <= 0) { 
                textCooldown.text = "READY!";
                canShootSpecialProjectile = true;
            }
        }
    }
    private void SetPreviousInput(InputAction.CallbackContext ctx) {
        previousInput = ctx.ReadValue<Vector2>();
    }

    private void ShotProjectile() {
        Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
    }

// special projectile
    private void ShootingSpecialProjectile() {
        if (specialProjectiles.Count != 0) {
                foreach (var specialProjectile in specialProjectiles) {
                    specialProjectile.Explode();
                }
                // StartCoroutine(SetCanShootSpecialProjectile());
                shootTime = Time.realtimeSinceStartup;
                canShootSpecialProjectile = false;
                return;
            }
            GameObject newSpecialProjectile = specialProjectilePrefab;
            newSpecialProjectile.GetComponent<SpecialProjectile>().SetAttackDamage(attackDamage);
            Instantiate(newSpecialProjectile, shootPoint.position, shootPoint.rotation);
    }
    private IEnumerator SetCanShootSpecialProjectile() {
        canShootSpecialProjectile = false;
        yield return new WaitForSeconds(specialProjectileCooldown);
        canShootSpecialProjectile = true;
    }

// event handlers
    private void HandleOnSpawnedSpecialProjectile(SpecialProjectile projectile)
    {
        specialProjectiles.Add(projectile);
    }

    private void HandleOnDespawnedSpecialProjectile(SpecialProjectile projectile)
    {
        specialProjectiles.Remove(projectile);
    }
}
