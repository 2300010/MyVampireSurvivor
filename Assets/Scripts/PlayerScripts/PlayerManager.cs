using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance => instance;


    float xPosition;
    float yPosition;

    public float XPosition { get => xPosition; }
    public float YPosition { get => yPosition; }

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerPosition();
    }

    private void GetPlayerPosition()
    {
        xPosition = gameObject.transform.localPosition.x;
        yPosition = gameObject.transform.localPosition.y;
    }
}
