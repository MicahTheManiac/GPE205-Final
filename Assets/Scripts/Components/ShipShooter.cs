using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShooter : Shooter
{
    public Transform firePoint;

    // Start is called before the first frame update
    public override void Start()
    {
        if (firePoint == null)
        {
            firePoint = transform;
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        // Do Nothing
    }

    public override void Shoot(GameObject projectilePrefab, float force, float damage, float lifespan)
    {
        // Instantiate our Projectile
        GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, transform.rotation) as GameObject;

        // Get Damage on Hit
        DamageOnHit doh = newProjectile.GetComponent<DamageOnHit>();

        // If it has DoH
        if (doh != null)
        {
            // Set damageDone in DoH to value passed in
            doh.damageDone = damage;

            // Set the owner to the Pawn that shot the Projectile. Otherwise owner is null
            doh.owner = GetComponent<Pawn>();
        }

        // Get Projectile Rigidbody
        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();

        // If it has Rigidbody
        if (rb != null)
        {
            // Add Force to move Projetile Forward
            rb.AddForce(transform.forward * force);
        }

        // Destroy after a set time
        Destroy(newProjectile, lifespan);
    }
}
