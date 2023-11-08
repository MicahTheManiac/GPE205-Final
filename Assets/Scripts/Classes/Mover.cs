using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : MonoBehaviour
{
    // Start is called before the first frame update
    public abstract void Start();

    // Move Function
    public abstract void Move(Vector3 direction, float speed);

    // Rotate Function (Speed will be in Degrees)
    public abstract void Rotate(float speed);

}
