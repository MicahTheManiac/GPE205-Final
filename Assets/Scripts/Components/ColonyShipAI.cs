using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColonyShipAI : AIController
{
    // Variables
    public float currentOffloadProgress = 0;
    public float maxOffloadProgress = 1;
    public bool isOffloading = false;

    private float offloadProgressCap;

    // Start is called before the first frame update
    public override void Start()
    {
        // Add to LevelManager
        if (LevelManager.instance != null)
        {
            LevelManager.instance.colonyShipController = this;
            LevelManager.instance.colonyShipPawn = this.pawn;
            passiveTarget = LevelManager.instance.destination;
            maxOffloadProgress = LevelManager.instance.maxWaves;
        }

        // Get Radius of Planet
        float xScale = passiveTarget.transform.localScale.x;
        float yScale = passiveTarget.transform.localScale.y;
        float zScale = passiveTarget.transform.localScale.z;
        float avgScale = (xScale + yScale + zScale) / 3;
        float avgRadius = avgScale / 2;

        trackingRange = trackingRange + avgRadius;

        // Set Progress & Health
        CalculateProgressFill();
        CalculateHealthFill();

        // Call Base Start
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Check For Pawn
        CheckForPawn();

        // Get Progress Cap
        if (LevelManager.instance != null)
        {
            offloadProgressCap = (LevelManager.instance.currentWave / (maxOffloadProgress + 1)) * maxOffloadProgress;
        }

        // Set Progress & Health
        CalculateProgressFill();
        CalculateHealthFill();

        // Make Decisions
        MakeDecisions();

        // Call Base Update
        base.Update();
    }

    new void MakeDecisions()
    {
        if (pawn != null)
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
                        isOffloading = true;

                        // Increase Progress
                        currentOffloadProgress += 0.25f * Time.deltaTime;
                        currentOffloadProgress = Mathf.Clamp(currentOffloadProgress, 0, offloadProgressCap);

                        if (currentOffloadProgress == maxOffloadProgress)
                        {
                            if (LevelManager.instance != null)
                            {
                                LevelManager.instance.DoGameWin();
                            }
                        }
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
    }

    // See if we have our Pawn
    new void CheckForPawn()
    {
        if (pawn == null)
        {
            if (LevelManager.instance != null)
            {
                LevelManager.instance.DoGameOver();
            }
            Destroy(gameObject);

        }
    }

    // Calculate Progress Fill
    private void CalculateProgressFill()
    {
        // Perform Calculation and Clamp for good measure
        float progress = currentOffloadProgress / maxOffloadProgress;
        progress = Mathf.Clamp01(progress);

        // Set Fill
        if (LevelManager.instance != null)
        {
            Slider shipProgressIndicator = LevelManager.instance.playerController.colonyShipProgress;
            if (shipProgressIndicator != null)
            {
                shipProgressIndicator.value = progress;
            }
        }
    }

    // Calculate Health Fill
    private void CalculateHealthFill()
    {
        if (pawn != null)
        {
            // Get Health Component
            Health myHealth = pawn.GetComponent<Health>();

            // Perform Calculation and Clamp for good measure
            float health = myHealth.currentHealth / myHealth.maxHealth;
            health = Mathf.Clamp01(health);

            // Set Fill
            if (LevelManager.instance != null)
            {
                Slider shipHealthIndicator = LevelManager.instance.playerController.colonyShipHealth;
                if (shipHealthIndicator != null)
                {
                    shipHealthIndicator.value = health;
                }
            }
        }
    }
}
