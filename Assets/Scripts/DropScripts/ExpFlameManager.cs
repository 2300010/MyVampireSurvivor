using System;
using UnityEngine;

public class ExpFlameManager : MonoBehaviour
{
    public static Action<int> OnExpPickup;

    int expGiven;
    Color expColor;
    GameObject deadEnemy;

    public int ExpGiven { get => expGiven; set => expGiven = value; }
    public Color ExpColor { get => expColor; set => expColor = value; }
    public GameObject DeadEnemy { get => deadEnemy; set => deadEnemy = value; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D otherObject = collision.collider;

        //Debug.LogError("There is a collision!");

        if (otherObject.CompareTag("Player"))
        {
            //Debug.LogError("Collision with the player!");
            OnExpPickup?.Invoke(expGiven);
            gameObject.SetActive(false);
        }
    }
}
