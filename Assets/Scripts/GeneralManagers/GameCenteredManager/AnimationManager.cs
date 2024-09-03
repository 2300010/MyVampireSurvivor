using System;
using UnityEngine;

public enum AnimationState 
{ 
    Idle,
    Walk,
    Attack,
    Hurt,
    Death
}

public class AnimationManager : MonoBehaviour
{
    Animator animator;
    private AnimationState currentState;
    public string thisObjectName;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(Enum characterName, AnimationState newState)
    {
        if (currentState == newState) return;

        string newAnimation = characterName.ToString() + "_" + newState.ToString();

        animator.Play(newAnimation);

        currentState = newState;
    }

    public AnimatorStateInfo GetAnimationState()
    {
        return animator.GetCurrentAnimatorStateInfo(0);
    }
}
