using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RunTimeSceneManager : MonoBehaviour
{
    private static RunTimeSceneManager instance;

    public static RunTimeSceneManager Instance => instance;

    public delegate void OnFadeInFinished();

    public static event OnFadeInFinished FadeInIsDone;

    private float startingFadeTime = 4f;
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

        StartCoroutine(FadeIn(startingFadeTime));
    }

    private void FindFadePanel()
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

    public void SwitchScene(string scene, float fadeTime)
    {
        SceneManager.LoadScene(scene);

        StartCoroutine(FadeOut(fadeTime));
    }

    IEnumerator FadeIn(float fadeTime)
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

        FadeInIsDone?.Invoke();
    }

    IEnumerator FadeOut(float fadeTime)
    {
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
    }
}
