using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AnimationStateChanger : MonoBehaviour
{
    //Not doing this cause animator.Play() takes a string
    //public enum AnimationState{Idle, Walk};
    //[SerializeField] AnimationState currentEnumState = AnimationState.Idle;

    [SerializeField] Animator animator;
    [SerializeField] string currentState = "Idle";

    void Start()
    {
        ChangeAnimationState("Idle");
    }

    public void ChangeAnimationState(string newState, float speed = 1) {
        animator.speed = speed;
        if(currentState == newState) {
            return;
        }
        
        currentState = newState;
        animator.Play(currentState);
    }
}
