using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPawn : Pawn
{
    private float secondsPerShot;
    private float shootEventTime;

    // Start is called before the first frame update
    public override void Start()
    {
        // Init Shooting Vars.
        secondsPerShot = 1 / shotsPerSecond;
        shootEventTime = Time.time + secondsPerShot;

        // Call Base Start
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Call Base Update
        base.Update();
    }

    // Move Forward
    public override void MoveForward()
    {
        mover.Move(transform.forward, moveSpeed, accelAmount);
    }

    // Move Backward
    public override void MoveBackward()
    {
        mover.Move(transform.forward, -moveSpeed, accelAmount);
    }

    // Rotate Clockwise
    public override void RotateClockwise()
    {
        mover.Rotate(turnSpeed);
    }

    // Rotate Counter-Clockwise
    public override void RotateCounterClockwise()
    {
        mover.Rotate(-turnSpeed);
    }

    // Rotate Towards (Mainly used in AI)
    public override void RotateTowards(Vector3 targetPosition)
    {
        // Get our Vector and Rotation
        Vector3 vectorToTarget = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);

        // Do Rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    // Shooting
    public override void Shoot()
    {
        // Check our Event Time
        if (Time.time >= shootEventTime)
        {
            Debug.Log("Shots Fired!");
            shootEventTime = Time.time + secondsPerShot;

            // Shoot Projectile
            shooter.Shoot(projectilePrefab, fireForce, damageDone, projectileLifespan);
        }
    }
}
