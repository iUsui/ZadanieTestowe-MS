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
    private int attackDamage = 25;
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private GameObject projectilePrefab = null;
    [SerializeField] private GameObject specialProjectilePrefab = null;
    private float specialProjectileCooldown = 2.0f;
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
    public void SetSpecialProjectileCooldown(float newSpecialProjectileCooldown) {
        specialProjectileCooldown = newSpecialProjectileCooldown;
    }
#endregion

    private Controls controls;
    private Vector2 previousInput;
    private void OnEnable() {
        instance = this;
        controls = new Controls();
        controls.Player.PlayerMovement.performed += SetPreviousInput;
        controls.Player.PlayerMovement.canceled += SetPreviousInput;
        controls.Player.PlayerShotting.performed += _ => ShotProjectile();
        controls.Enable();
        SpecialProjectile.OnSpawned += HandleOnSpawnedSpecialProjectile;
        SpecialProjectile.OnDespawned += HandleOnDespawnedSpecialProjectile;
    }
    private void OnDisable() {
        controls.Player.PlayerMovement.performed -= SetPreviousInput;
        controls.Player.PlayerMovement.canceled -= SetPreviousInput;
        controls.Player.PlayerShotting.performed -= _ => ShotProjectile();
        controls.Disable();
        SpecialProjectile.OnSpawned -= HandleOnSpawnedSpecialProjectile;
        SpecialProjectile.OnDespawned -= HandleOnDespawnedSpecialProjectile;
    }

    private void Update() {
        Vector3 currentPosition = transform.position;
        if (previousInput != Vector2.zero) {
            currentPosition += new Vector3(previousInput.x, 0f, 0f) * movementSpeed * Time.deltaTime;
        }
        transform.position = currentPosition;

        HandleShootingSpecialProjectile();
    }

    

    private void SetPreviousInput(InputAction.CallbackContext ctx) {
        previousInput = ctx.ReadValue<Vector2>();
    }

    private void ShotProjectile() {
        GameObject newProjectileGameObject = projectilePrefab;
        Projectile newProjectileScript = newProjectileGameObject.GetComponent<Projectile>();
        newProjectileScript.attackDamage = attackDamage;
        Instantiate(newProjectileGameObject, shootPoint.position, shootPoint.rotation);
    }

// special projectile
    private void HandleShootingSpecialProjectile()
    {
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
    private void ShootingSpecialProjectile() {
        if (specialProjectiles.Count != 0) {
                foreach (var specialProjectile in specialProjectiles) {
                    specialProjectile.Explode();
                }
                shootTime = Time.realtimeSinceStartup;
                canShootSpecialProjectile = false;
                return;
            }
            GameObject newSpecialProjectile = specialProjectilePrefab;
            SpecialProjectile specialProjectileScript = newSpecialProjectile.GetComponent<SpecialProjectile>();
            specialProjectileScript.attackDamage = attackDamage;
            Instantiate(newSpecialProjectile, shootPoint.position, shootPoint.rotation);
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
