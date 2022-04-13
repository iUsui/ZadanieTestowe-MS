using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float timeBeforeDestroying = 0.75f;
    [SerializeField] private float timeBeforeDisablingCollider = 0.6f;
    [SerializeField] private new SphereCollider collider = null;
    public int attackDamage;

    private void Start() {
        collider.enabled = true;
        StartCoroutine(DestroyAfterTime());
        Invoke(nameof(DisableCollider), timeBeforeDisablingCollider);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            other.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
    }

    private IEnumerator DestroyAfterTime() {
        yield return new WaitForSeconds(timeBeforeDestroying);
        Destroy(gameObject);
    }
    private void DisableCollider() {  
        collider.enabled = false;
    }
}
