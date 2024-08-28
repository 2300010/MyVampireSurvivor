using UnityEngine;

enum AnimationState 
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
    private string currentState;
    public string thisObjectName;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}
