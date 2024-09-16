using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMasterManager : MonoBehaviour
{
    private static SceneMasterManager instance;

    public static SceneMasterManager Instance => instance;

    public delegate void OnNewSceneLoaded();

    public static event OnNewSceneLoaded NewSceneIsLoaded;

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

    private void LoadNewSceneWithFade(string scene)
    {
        SceneManager.LoadScene(scene);

        FadingManager.FadeOutIsCompleted -= LoadNewSceneWithFade;

        NewSceneIsLoaded?.Invoke();
    }

    public void SwitchScene(string scene)
    {
        FadingManager.FadeOutIsCompleted += LoadNewSceneWithFade;

        FadingManager.Instance.StartFadeOutCoroutine(1.5f, scene);

        FadingManager.Instance.StartFadeInCoroutine(1.5f, scene);
    }
}
