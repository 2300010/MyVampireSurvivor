using System;
using UnityEngine;

public class ExpFlameManager : MonoBehaviour
{
    public static Action<int> OnExpPickup;

    int expGiven;
    Color expColor;
    GameObject deadEnemy;

    [SerializeField] private AnimationManager animationManager;

    public int ExpGiven { get => expGiven; set => expGiven = value; }
    public Color ExpColor { get => expColor; set => expColor = value; }
    public GameObject DeadEnemy { get => deadEnemy; set => deadEnemy = value; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D otherObject = collision.collider;

        //Debug.LogError("There is a collision!");

        if (otherObject.CompareTag(ObjectTag.Player.ToString()))
        {
            //Debug.LogError("Collision with the player!");
            OnExpPickup?.Invoke(expGiven);
            gameObject.SetActive(false);
        }
    }

    public void SetupOnEnable()
    {
        animationManager = GetComponent<AnimationManager>();
        animationManager.Animator.Play("GoblinFire_Idle");
    }
}
