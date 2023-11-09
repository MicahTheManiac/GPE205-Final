using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    // Get Controls & Essentials
    public Mover mover;
    public Controller controller;
    public Shooter shooter;
    public GameObject projectilePrefab;

    // Movement
    public float accelAmount;
    public float moveSpeed;
    public float turnSpeed;

    // Shooting
    public float shotsPerSecond = 5.0f;
    public float fireForce;
    public float damageDone;
    public float projectileLifespan = 10.0f;


    // Start is called before the first frame update
    public virtual void Start()
    {
        mover = GetComponent<Mover>();
        shooter = GetComponent<Shooter>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    // Movement Functions
    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();

    // Rotate Towards (Mainly used in AI)
    public abstract void RotateTowards(Vector3 targetPosition);

    // Shoot Function
    public abstract void Shoot();

}
