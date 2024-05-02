using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMouvement : MonoBehaviour
{
    Vector2 target;
    [SerializeField] float speed = 1.5f;

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetTargetDestination();
        ChasePlayer();
    }

    private void SetTargetDestination()
    {
        target = new Vector2(PlayerManager.Instance.XPosition, PlayerManager.Instance.YPosition);
    }

    private void ChasePlayer()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }
}
