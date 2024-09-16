using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadingManager : MonoBehaviour
{
    private static FadingManager instance;

    public static FadingManager Instance => instance;

    public delegate void OnFadeInCompleted(string scene);

    public static event OnFadeInCompleted FadeInIsCompleted;

    public delegate void OnFadeOutCompleted(string scene);

    public static event OnFadeOutCompleted FadeOutIsCompleted;

    private float startingFadeTime = 1.5f;
    [SerializeField] private Image fadeImage = null;


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
        FindFadePanel();

        SceneMasterManager.NewSceneIsLoaded += FindFadePanel;

        StartFadeInCoroutine(startingFadeTime, "MainMenu");
    }

    public void FindFadePanel()
    {
        Image[] images = FindObjectsOfType<Image>();

        foreach (Image img in images)
        {
            if (img.gameObject.name == "FadePanel")
            {
                fadeImage = img;
                break;
            }
        }

        CustomAssert.Ensure(fadeImage != null, "Fade Panel is null in current scene => " + SceneManager.GetActiveScene().name);
    }

    public void StartFadeOutCoroutine(float fadeTime, string scene)
    {
        StartCoroutine(FadeOut(fadeTime, scene));
    }

    public void StartFadeInCoroutine(float fadeTime, string scene)
    {
        StartCoroutine(FadeIn(fadeTime, scene));
    }

    IEnumerator FadeIn(float fadeTime, string scene)
    {
        float elapsedTime = 0f;
        Color tempColor = fadeImage.color;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            tempColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeTime);
            fadeImage.color = tempColor;
            yield return null;
        }

        tempColor.a = 0f;
        fadeImage.color = tempColor;

        fadeImage.gameObject.SetActive(false);

        FadeInIsCompleted?.Invoke(scene);
    }

    IEnumerator FadeOut(float fadeTime, string scene)
    {
        fadeImage.gameObject.SetActive(true);

        float elapsedTime = 0f;
        Color tempColor = fadeImage.color;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            tempColor.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeTime);
            fadeImage.color = tempColor;
            yield return null;
        }

        tempColor.a = 1f;
        fadeImage.color = tempColor;

        FadeOutIsCompleted?.Invoke(scene);
    }
}
