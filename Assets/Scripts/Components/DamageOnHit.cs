using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    public float damageDone;
    public Pawn owner;

    public void OnTriggerEnter(Collider other)
    {
        // Get Health Component from other GameObject that has Trigger we're Entering
        Health otherHealth = other.gameObject.GetComponent<Health>();

        if (other.gameObject != owner.gameObject)
        {
            // Only Damage other if it has Health Component
            if (otherHealth != null)
            {
                // Do Damage
                otherHealth.TakeDamage(damageDone, owner);
            }

            // Destroy this Projectile
            Destroy(gameObject);
        }
    }
}
