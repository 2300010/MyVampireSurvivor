using System;
using UnityEngine;

public enum AnimationState 
{ 
    Idle,
    Move,
    Attack,
    Hurt,
    Death
}

public class AnimationManager : MonoBehaviour
{
    private Animator animator;
    private AnimationState currentState;

    public Animator Animator { get => animator; set => animator = value; }

    private void OnEnable()
    {
        Animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(Enum characterName, AnimationState newState)
    {
        if (currentState == newState) return;

        string newAnimation = characterName.ToString() + "_" + newState.ToString();

        Animator.Play(newAnimation);

        currentState = newState;
    }

    public AnimatorStateInfo GetAnimationState()
    {
        return Animator.GetCurrentAnimatorStateInfo(0);
    }
}
