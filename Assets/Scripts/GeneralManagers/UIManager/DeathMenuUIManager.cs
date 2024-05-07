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

    // Start is called before the first frame update
    void Start()
    {
        HpManager.PlayerDeath += ActivateUI;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        HpManager.PlayerDeath -= ActivateUI;
    }

    private void ActivateUI()
    {
        if (gameObject.name == "DeathMenu")
        {
            gameObject.SetActive(true);
        }
    }
}
