using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterUIManager : MonoBehaviour
{
    private static MasterUIManager instance;
    public static MasterUIManager Instance => instance;

    private const string CANVAS_ = "Canvas_";
    Canvas[] canvas;


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
        canvas = GetAllCanvas();

        SceneManager.sceneLoaded += HideAllUI;
    }

    public Canvas[] GetAllCanvas()
    {
        Canvas[] canvasFound = GetComponentsInParent<Canvas>();

        return canvasFound;
    }

    public void HideAllUI(Scene loadedScene, LoadSceneMode sceneMode)
    {
        if (loadedScene.name == "PersistentScene")
        {
            foreach (Canvas c in canvas)
            {
                if (c.name != "Canvas_Fade")
                {
                    c.gameObject.SetActive(false);
                }
            }
            SceneManager.sceneLoaded -= HideAllUI;
        }
    }

    public void HideCanvas(string canvasName)
    {
        string fullName = CANVAS_ + canvasName;

        if (!string.IsNullOrEmpty(canvasName))
        {
            foreach (Canvas c in canvas)
            {
                if (c.name == fullName)
                {
                    c.gameObject.SetActive(true);
                    break;
                }
                Debug.LogError("Error! The canvas " + canvasName + " does not exists.");
            }
        }
        else
        {
            Debug.LogWarning("Warning! The name of the canvas you are searching for is null.");
        }
    }

    public void ShowCanvas(string canvasName)
    {
        string fullName = CANVAS_ + canvasName;

        if (!string.IsNullOrEmpty(canvasName))
        {
            foreach (Canvas c in canvas)
            {
                if (c.name == fullName)
                {
                    c.gameObject.SetActive(true);
                    break;
                }
                Debug.LogError("Error! The canvas " + canvasName + " does not exists.");
            }
        }
        else
        {
            Debug.LogWarning("Warning! The name of the canvas you are searching for is null.");
        }
    }
}
