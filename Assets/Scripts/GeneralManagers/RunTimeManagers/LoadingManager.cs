using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    private static LoadingManager instance;

    public static LoadingManager Instance => instance;

    public const string PERSISTENT_SCENE = "PersistentScene";

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

    private void Start()
    {
        SceneManager.sceneLoaded += GameOpening;
    }

    private void GameOpening(Scene persistentScene, LoadSceneMode sceneMode)
    {
        CustomAssert.Ensure(persistentScene.name != PERSISTENT_SCENE, "Persistent scene has not been loaded correctly.");

        SceneManager.sceneLoaded -= GameOpening;

        LoadNewScene("MainMenu", LoadSceneMode.Additive, () =>
        {
            MasterUIManager.Instance.ShowCanvas("MainMenu");
            FadingManager.Instance.FadeIn(2.5f, (null));
        });
    }

    private void LoadNewScene(string scene, LoadSceneMode sceneMode, Action onSceneLoaded)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, sceneMode);

        asyncLoad.completed += (AsyncOperation operation) =>
        {
            onSceneLoaded?.Invoke();
        };
    }

    private void UnloadCurrentScene(Action onSceneUnloaded)
    {
        Scene activeScene = SceneManager.GetActiveScene();
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(activeScene);


        asyncUnload.completed += (AsyncOperation operation) =>
        {
            onSceneUnloaded?.Invoke();
        };
    }

    public void SwitchScene(string newScene, float fadeTime, LoadSceneMode sceneMode)
    {
        FadingManager.Instance.FadeOut(fadeTime, () =>
        {
            UnloadCurrentScene(() =>
            {
                MasterUIManager.Instance.HideCanvas(SceneManager.GetActiveScene().name);
                LoadNewScene(newScene, sceneMode, () =>
                {
                    MasterUIManager.Instance.ShowCanvas(newScene);
                    FadingManager.Instance.FadeIn(fadeTime, (null));
                });
            });
        });
    }
}
