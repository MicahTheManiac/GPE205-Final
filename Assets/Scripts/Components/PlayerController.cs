using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerController : Controller
{
    // Define Keybinds
    public KeyCode moveFowardKey;
    public KeyCode moveBackwardKey;
    public KeyCode rotateClockwiseKey;
    public KeyCode rotateCounterClockwiseKey;
    public KeyCode shootKey;

    // Start is called before the first frame update
    public override void Start()
    {
        // Run Base Start
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Process Input
        ProcessInputs();

        // Run Base Update
        base.Update();
    }

    public override void ProcessInputs()
    {
        // Foward
        if (Input.GetKey(moveFowardKey))
        {
            pawn.MoveForward();
        }

        // Backward
        if (Input.GetKey(moveBackwardKey))
        {
            pawn.MoveBackward();
        }

        // Rotate Clockwise
        if (Input.GetKey(rotateClockwiseKey))
        {
            pawn.RotateClockwise();
        }

        // Rotate Counter-Clockwise
        if (Input.GetKey(rotateCounterClockwiseKey))
        {
            pawn.RotateCounterClockwise();
        }

        // Shoot
        if (Input.GetKey(shootKey))
        {
            pawn.Shoot();
        }
    }

}
