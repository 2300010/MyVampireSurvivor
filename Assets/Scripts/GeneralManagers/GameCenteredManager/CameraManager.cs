using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager instance;

    public static CameraManager Instance => instance;

    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {

    }
}
