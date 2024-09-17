using UnityEngine;

public class DeathMenuUIManager : MonoBehaviour
{
    private static DeathMenuUIManager instance;

    public static DeathMenuUIManager Instance => instance;

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

    public void OnEnable()
    {
        HpManager.PlayerDeath += ActivateUI;
    }

    private void OnDisable()
    {
        HpManager.PlayerDeath -= ActivateUI;
    }

    private void ActivateUI()
    {
        gameObject.SetActive(true);
    }
}
