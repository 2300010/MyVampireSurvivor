using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public static InputManager Instance() => instance;

    //Actions for the keys pressed
    public static Action SpaceBarPressed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        OnKeyPressed();
    }

    private void OnKeyPressed()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { SpaceBarPressed?.Invoke(); }
    }
}
