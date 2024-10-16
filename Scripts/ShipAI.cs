using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAI : MonoBehaviour
{
    [SerializeField] string currentStateString;
    [SerializeField] SpaceShip myShip;
    [SerializeField] SpaceShip targetShip;
    
    [Header("Config")]
    [SerializeField] float sightDistance = 10;

    delegate void AIState();
    AIState currentState;

    //trackers==========================================
    float stateTime = 0;
    bool justChangedState = false;
    Vector3 lastTargetPos;

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(IdleState);
    }

    void ChangeState(AIState newAIState) {
        currentState = newAIState;
        justChangedState = true;
    }

    bool CanSeeTarget() {
        return Vector3.Distance(myShip.transform.position, targetShip.transform.position) < sightDistance;
    }

    void IdleState() {
        if(stateTime == 0) {
            currentStateString = "IdleState";
        }
        if(CanSeeTarget()) {
            ChangeState(AttackState);
            return;
        }
    }

    void AttackState() {
        myShip.MoveToward(targetShip.transform.position);
        myShip.AimShip(targetShip.transform);
        if(stateTime == 0) {
            currentStateString = "AttackState";
        }

        if(stateTime > 1.5f) {
            myShip.LaunchWithShip(); //shoot at player!
        }

        if(myShip.GetProjectileLauncher().GetAmmo() == 0) {
            myShip.GetProjectileLauncher().Reload();
        }

        if(!CanSeeTarget()) {
            lastTargetPos = targetShip.transform.position;
            ChangeState(GetBackToTargetState);
            return;
        }
    }

    void GetBackToTargetState() { //if we lose sight of the player, go back to the position where we last saw the player
        if(stateTime == 0) {
            currentStateString = "GetBackToTargetState";
        }

        myShip.MoveToward(lastTargetPos);
        myShip.AimShip(lastTargetPos);

        if(stateTime < 2) {
            return;
        }

        if(CanSeeTarget()) {
            ChangeState(AttackState);
            return;
        }
        if(Vector3.Distance(myShip.transform.position, lastTargetPos) < 1f) {
            ChangeState(PatrolState);
            return;
        }
    }

    Vector3 patrolPos;
    Vector3 patrolPivot; //Where we started Patrolling
    void PatrolState() {
        //pick a random position
        //move towards it
        //once we reach it, choose a new Random Position
        if(stateTime == 0) {
            currentStateString = "PatrolState";
            patrolPivot = myShip.transform.position;
            patrolPos = myShip.transform.position + new Vector3(Random.Range(-sightDistance,sightDistance), Random.Range(-sightDistance,sightDistance));
        }

        myShip.MoveToward(patrolPos);
        myShip.AimShip(patrolPos);

        if(CanSeeTarget()) {
            ChangeState(AttackState);
            return;
        }
        if(Vector3.Distance(myShip.transform.position, patrolPos) < 1f) {
            patrolPos = patrolPivot + new Vector3(Random.Range(-sightDistance,sightDistance), Random.Range(-sightDistance,sightDistance));
            return;
        }
    }

    void AITick() {
        if(justChangedState) {
            stateTime = 0;
            justChangedState = false;
        }
        currentState();
        stateTime += Time.deltaTime;
    }

    void FixedUpdate() {
        //Move ship inside here
        //Set the movement instead of calling move ship
    }

    // Update is called once per frame
    void Update()
    {
        AITick();
    }
}