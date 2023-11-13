using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColonyShipAI : AIController
{
    // Variables
    public float currentOffloadProgress = 0;
    public float maxOffloadProgress = 100;
    public Image progressIndicator;

    // Start is called before the first frame update
    public override void Start()
    {
        // Add to LevelManager
        if (LevelManager.instance != null)
        {
            LevelManager.instance.colonyShipPawn = this.pawn;
            passiveTarget = LevelManager.instance.destination;
        }

        // Get Radius of Planet
        float xScale = passiveTarget.transform.localScale.x;
        float yScale = passiveTarget.transform.localScale.y;
        float zScale = passiveTarget.transform.localScale.z;
        float avgScale = (xScale + yScale + zScale) / 3;
        float avgRadius = avgScale / 2;

        trackingRange = trackingRange + avgRadius;

        // Set Progress Image
        CalculateImageFill();

        // Call Base Start
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        // Make Decisions
        MakeDecisions();
    }

    new void MakeDecisions()
    {
        switch (currentState)
        {
            // IDLE STATE
            case AIState.Idle:
                // Do Idle
                DoIdleState();

                // If we are in range of the planet
                if (IsDistanceLessThan(passiveTarget, trackingRange))
                {
                    Debug.Log("Offloading");
                }
                // If we have a Passive Target
                else if (passiveTarget != null)
                {
                    ChangeState(AIState.Seek);
                }
                break;

            // SEEK STATE
            case AIState.Seek:
                // If we are in range of the planet
                if (IsDistanceLessThan(passiveTarget, trackingRange))
                {
                    ChangeState(AIState.Idle);
                }

                // Do Seek
                DoSeekState();
                break;
        }
    }

    // Calculate Image Fill
    private void CalculateImageFill()
    {
        // Perform Calculation and Clamp for good measure
        float progress = currentOffloadProgress / maxOffloadProgress;
        progress = Mathf.Clamp01(progress);

        // Set Fill
        progressIndicator.fillAmount = progress;
    }
}
