using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private static SceneManager instance;

    public static SceneManager Instance => instance;

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
        
    }

    public void SwitchSceneWithFade(Scene scene)
    {
        scene.GetRootGameObjects();
        //StartCoroutine;
    }

    IEnumerator FadeIn()
    {

        yield return null;
    }

    IEnumerator FadeOut() 
    { 
        yield return null; 
    }
}
