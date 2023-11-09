using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMover : Mover
{
    private float currentSpeed = 0;

    // Start is called before the first frame update
    public override void Start()
    {
        // Do Nothing
    }

    // Move Function
    public override void Move(Vector3 direction, float speed)
    {
        // Get a Movement Vector and Add
        Vector3 moveVector = direction.normalized * speed * Time.deltaTime;
        transform.position += moveVector;
    }

    // Overload: Move Function
    public override void Move(Vector3 direction, float speed, float acceleration)
    {
        // If Speed > 0 We are going Forward
        if (speed > 0)
        {
            if (currentSpeed < speed)
            {
                // Increase our Speed by Acceleration
                currentSpeed += acceleration * Time.deltaTime;

                // Clamp our Value
                currentSpeed = Mathf.Clamp(currentSpeed, 0, speed);
            }
        }
        // Otherwise We are going Backward
        else
        {
            if (currentSpeed > speed)
            {
                // Increase our Speed by Acceleration
                currentSpeed -= acceleration * Time.deltaTime;

                // Clamp our Value
                currentSpeed = Mathf.Clamp(currentSpeed, speed, 0);
            }
        }

        // Get a Movement Vector and Add
        Vector3 moveVector = direction.normalized * currentSpeed * Time.deltaTime;
        transform.position += moveVector;
    }

    // Rotate Function
    public override void Rotate(float speed)
    {
        // Rotate by Turn Speed * Delta Time
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }

    public override void ResetPrivateVars()
    {
        currentSpeed = 0;
    }
}
