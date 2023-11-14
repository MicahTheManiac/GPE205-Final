using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyAI : AIController
{
    // Start is called before the first frame update
    public override void Start()
    {
        // Add Enemy to LevelManager
        if (LevelManager.instance != null)
        {
            if (LevelManager.instance.enemies != null)
            {
                LevelManager.instance.enemies.Add(this);
            }

            passiveTarget = LevelManager.instance.colonyShipPawn.gameObject;
            activeTarget = LevelManager.instance.playerPawn.gameObject;
        }

        // Call Base Start
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Check for Pawn
        CheckForPawn();

        // Check for Target
        CheckForTargets();

        // Make Decisions
        MakeDecisions();

        // Call Base Update
        base.Update();
    }
}
