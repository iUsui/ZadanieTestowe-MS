using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private GameObject projectilePrefab = null;
    [SerializeField] private Transform shootPoint = null;

    private Controls controls;
    private Vector2 previousInput;
    private void OnEnable() {
        controls = new Controls();
        controls.Player.PlayerMovement.performed += SetPreviousInput;
        controls.Player.PlayerMovement.canceled += SetPreviousInput;
        controls.Player.PlayerShotting.started += _ => ShotProjectile();
        controls.Enable();
    }

    private void Update() {
        Vector3 currentPosition = transform.position;
        if (previousInput != Vector2.zero) {
            currentPosition += new Vector3(previousInput.x, 0f, 0f) * movementSpeed * Time.deltaTime;
        }
        transform.position = currentPosition;
    }

    private void SetPreviousInput(InputAction.CallbackContext ctx) {
        previousInput = ctx.ReadValue<Vector2>();
    }

    private void ShotProjectile() {
        Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
    }
}
