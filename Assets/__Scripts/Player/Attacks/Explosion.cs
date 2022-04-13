using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float timeBeforeDestroying = 0.75f;
    [SerializeField] private new SphereCollider collider = null;
    public int attackDamage;
    
    public void SetAttackDamage(int newAttackDamage) {
        attackDamage = newAttackDamage;
    }

    private void Start() {
        collider.enabled = true;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            Debug.Log(other.GetComponent<Health>().GetCurrentHealth());
            other.GetComponent<Health>().TakeDamage(attackDamage);
            Debug.Log($"dealt damage..{other.GetComponent<Health>().GetCurrentHealth()}");
        }
    }

    private IEnumerator DestroyAfterTime() {
        yield return new WaitForSeconds(timeBeforeDestroying);
        Destroy(gameObject);
    }
}
