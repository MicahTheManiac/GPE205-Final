using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    // Start is called before the first frame update
    public abstract void Start();


    // Update is called once per frame
    public abstract void Update();

    // Shoot Function
    public abstract void Shoot(GameObject projectilePrefab, float force, float damage, float lifespan);

}
