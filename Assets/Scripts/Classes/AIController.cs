using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    // AI States
    public enum AIState { Idle, Seek, Chase, Attack };
    public AIState currentState;

    // Targets
    public GameObject passiveTarget; // Constantly Seeking this Target until we get the Active
    public GameObject activeTarget; // Active meaning our Player taking more Priority

    // Distance Variables
    public float trackingRange = 100.0f;
    public float attackingRange = 30.0f;
    public float fov = 60.0f;

    // Raycasting
    public LayerMask layerToRaycast;
    Ray lineOfSite; // This is Private

    // Start is called before the first frame update
    public override void Start()
    {
        // Set Ray **This is Important**
        lineOfSite = new Ray(transform.position, transform.forward);

        // Set State to Idle
        currentState = AIState.Idle;

        // Call Base Start
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Call Base Update
        base.Update();
    }

    // Override to Satisfy Controller.cs
    public override void ProcessInputs()
    {
        // Send Message
        Debug.Log("AIController.cs takes no inputs!");
    }

    // Make Decisions
    public void MakeDecisions()
    {
        // Switch
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

                // If we detect Active Target
                if (IsDistanceLessThan(activeTarget, trackingRange))
                {
                    ChangeState(AIState.Chase);
                }
                break;

            // SEEK STATE
            case AIState.Seek:
                // Do Seek
                DoSeekState();

                // If we detect Active Target
                if (IsDistanceLessThan(activeTarget, trackingRange))
                {
                    ChangeState(AIState.Chase);
                }

                // If we are in Range of Passive (Let Attack Handle CanSee)
                if (IsDistanceLessThan(passiveTarget, attackingRange))
                {
                    ChangeState(AIState.Attack);
                }
                break;

            // CHASE STATE
            case AIState.Chase:
                // Do Chase
                DoChaseState();

                // If we are in Attack Range of Active
                if (IsDistanceLessThan(activeTarget, attackingRange))
                {
                    ChangeState(AIState.Attack);
                }

                // If we are out of Tracking Range
                if (!IsDistanceLessThan(activeTarget, trackingRange))
                {
                    ChangeState(AIState.Idle); // Will send us to Seek
                }
                break;

            // ATTACK STATE
            case AIState.Attack:
                // Attack Passive Target
                if (CanSee(passiveTarget, fov))
                {
                    // Do Attack: Pass in Passive
                    DoAttackState(passiveTarget);
                    Debug.Log("Working");
                }
                // Attack Active Target
                //else if (CanSee(activeTarget, fov))
                //{
                //    // Do Attack: Pass in Active
                //    DoAttackState(activeTarget);
                //}
                break;
        }
    }

    // Is Distance Less-Than
    protected bool IsDistanceLessThan(GameObject target, float distance)
    {
        if (Vector3.Distance(pawn.transform.position, target.transform.position) < distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Can See
    protected bool CanSee(GameObject target, float angle)
    {
        // Find the Vector from Us to Target
        Vector3 usToTargetVector = target.transform.position - pawn.transform.position;

        // Find Angle between Dir We're Facing and Vector to Target
        float angleToTarget = Vector3.Angle(usToTargetVector, pawn.transform.forward);

        // If that Distance is < our FOV
        if (angleToTarget < angle)
        {
            // Raycast Magic
            if (Physics.Raycast(lineOfSite, out RaycastHit hit, attackingRange, layerToRaycast))
            {
                Debug.Log(hit.collider.name);
                // By my logic: If we hit an Object with the same name as the Target, then we hit the Target
                if (hit.collider.name == target.name)
                {
                    return true;
                }
                // Names don't match
                else
                {
                    return false;
                }
            }
            // Couldn't Raycast
            else
            {
                Debug.Log("False");
                return false;
            }
        }
        // aTT !< angle
        else
        {
            return false;
        }
    }

    // See if we have our Pawn
    public void CheckForPawn()
    {
        if (pawn == null)
        {
            Debug.Log(gameObject.name + ": Self Destruct, I am missing my Pawn!");
            Destroy(gameObject);

        }
    }

    // Shooting
    public void Shoot()
    {
        // Tell our Pawn to Shoot
        pawn.Shoot();
    }

    // Change State
    public virtual void ChangeState(AIState newState)
    {
        // Change Current State
        currentState = newState;
    }

    // Idle State
    protected virtual void DoIdleState()
    {
        // Do Nothing
    }

    // Seek State
    protected virtual void DoSeekState()
    {
        // Seek the Passive
        Seek(passiveTarget);
    }

    // Chase State
    protected virtual void DoChaseState()
    {
        // Seek the Active
        Seek(activeTarget);
    }

    protected virtual void DoAttackState(GameObject target)
    {
        // Seek the Target (Can be Active or Passive)
        Seek(target);

        // Shoot
        Shoot();
    }

    // Seek Function -- Target Position
    public void Seek(Vector3 targetPos)
    {
        // Rotate Towards Target & Move
        pawn.RotateTowards(targetPos);
        pawn.MoveForward();
    }

    // Overload: Seek Function -- Target Transform
    public void Seek(Transform targetTf)
    {
        // Seek Position of Transform
        Seek(targetTf.position);
    }

    // Overload: Seek Function -- Pawn Transform
    public void Seek(Pawn targetPawn)
    {
        // Seek the Pawn Transform
        Seek(targetPawn.transform);
    }

    // Overload: Seek Function -- Controller Pawn
    public void Seek(Controller controller)
    {
        // Seek the Controller's Pawn Transform
        Seek(controller.pawn);
    }

    // Overload: Seek Function -- GameObject Transform
    public void Seek(GameObject gameObject)
    {
        // Seek GameObject's Transform
        Seek(gameObject.transform);
    }
}
