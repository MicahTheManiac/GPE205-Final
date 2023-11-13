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
        base.Update();

        // Remove enemy if Dead
        Health health = pawn.GetComponent<Health>();

        /*if (health.currentHealth <= 0)
        {
            // Add Enemy from LevelManager
            if (LevelManager.instance != null)
            {
                if (LevelManager.instance.enemies != null)
                {
                    LevelManager.instance.enemies.Remove(this);
                }
            }
        }*/

        // Make Decisions
        MakeDecisions();
    }
}
