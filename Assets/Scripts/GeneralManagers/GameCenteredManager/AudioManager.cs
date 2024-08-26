using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource myAudioSource;


    public static AudioManager GetInstance()
    {
        
        if (instance == null)
        {
            instance = FindObjectOfType<AudioManager>();
            if (instance == null)
            {
                GameObject gameObject = new GameObject("AudioManager");
                instance = gameObject.AddComponent<AudioManager>();
            }
        }
        return instance;
        
    }
    // Start is called before the first frame update
    void Awake()
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

        myAudioSource = GetComponent<AudioSource>();

        if (myAudioSource == null)
        {
            myAudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlaySound(AudioClip clip)
    {
        myAudioSource.clip = clip;
        myAudioSource.Play();
    }
}
