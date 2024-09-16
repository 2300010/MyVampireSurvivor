using UnityEngine;

public class MainMenuButtonManager : MonoBehaviour
{
    private static MainMenuButtonManager instance;

    public static MainMenuButtonManager Instance => instance;

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

    public void StartButtonClick()
    {
        SceneMasterManager.Instance.SwitchScene("Level1");
    }

    public void QuitButtonClick()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else

        Application.Quit();
#endif

    }
}
