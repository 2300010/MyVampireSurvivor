using System;
using UnityEngine;

public class EnemyAISensor : MonoBehaviour
{
    [SerializeField] float rangeToAttack;
    const string TARGET_TAG = "Player";
    bool rangeCollider;

    public float RangeToAttack { get => rangeToAttack; set => rangeToAttack = value; }
    public bool RangeCollider { get => rangeCollider; set => rangeCollider = value; }

    public Action OnTriggerEnterAction;
    public Action OnTriggerExitAction;


    private void Start()
    {
        if (gameObject.CompareTag("RangeCollider"))
        {
            RangeCollider = true;
            gameObject.GetComponentInChildren<CircleCollider2D>().radius = rangeToAttack;
        }
        else
        {
            RangeCollider = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag(TARGET_TAG))
        {
            GameObject player = collision.gameObject;
            OnTriggerEnterAction?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TARGET_TAG))
        {
            OnTriggerExitAction?.Invoke();
        }
    }
}
