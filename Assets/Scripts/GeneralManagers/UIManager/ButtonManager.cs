using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private static ButtonManager instance;

    public static ButtonManager Instance => instance;

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

    public void StartGameButtonClick()
    {
        LoadingManager.Instance.SwitchScene("Level1", 1.5f, UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    public void QuitAppButtonClick()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else

        Application.Quit();
#endif

    }
}
