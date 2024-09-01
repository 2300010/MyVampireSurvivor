using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    private static PlayerMouvement instance;

    public static PlayerMouvement Instance() => instance;

    void Awake()
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
    
}
