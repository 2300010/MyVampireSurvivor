using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadingManager : MonoBehaviour
{
    private static FadingManager instance;

    public static FadingManager Instance => instance;

    public delegate void OnFadeInCompleted();

    public static event OnFadeInCompleted FadeInIsCompleted;

    public delegate void OnFadeOutCompleted();

    public static event OnFadeOutCompleted FadeOutIsCompleted;

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
        CustomAssert.Ensure(fadeImage != null, "Fade Panel is null in current scene => " + SceneManager.GetActiveScene().name);
    }

    public void FadeOut(float fadeTime, Action onFadeComplete)
    {
        fadeImage.gameObject.SetActive(true);

        float elapsedTime = 0f;
        Color tempColor = fadeImage.color;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            tempColor.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeTime);
            fadeImage.color = tempColor;
        }

        tempColor.a = 1f;
        fadeImage.color = tempColor;

        Invoke("OnFadeOutComplete", 0f);

        void OnFadeOutComplete()
        {
            //FadeOutIsCompleted?.Invoke();
            onFadeComplete?.Invoke();
        }
    }

    public void FadeIn(float fadeTime, Action onFadeComplete)
    {
        float elapsedTime = 0f;
        Color tempColor = fadeImage.color;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            tempColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeTime);
            fadeImage.color = tempColor;
        }

        tempColor.a = 0f;
        fadeImage.color = tempColor;

        Invoke("OnFadeInComplete", 0f);

        void OnFadeInComplete()
        {
            fadeImage.gameObject.SetActive(false);

            //FadeInIsCompleted?.Invoke();
            onFadeComplete?.Invoke();
        }

    }
}
