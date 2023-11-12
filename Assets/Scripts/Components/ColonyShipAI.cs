using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColonyShipAI : AIController
{
    // Start is called before the first frame update
    public override void Start()
    {
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

                // If we have a Passive Target
                if (passiveTarget != null)
                {
                    ChangeState(AIState.Seek);
                }
                break;

            // SEEK STATE
            case AIState.Seek:
                DoSeekState();
                break;
        }
    }
}
