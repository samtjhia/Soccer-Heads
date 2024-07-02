using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private AudioSource audioSource;

    public AudioClip gameplayMusicClip;
    public AudioClip gameOverMusicClip;

    private bool isSceneBeingReloaded = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isSceneBeingReloaded = true;
    }

    void Update()
    {
        if (isSceneBeingReloaded)
        {
            isSceneBeingReloaded = false;
            PlayGameplayMusic();
        }
    }

    public void PlayGameplayMusic()
    {
        audioSource.clip = gameplayMusicClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayGameOverMusic()
    {
        audioSource.clip = gameOverMusicClip;
        audioSource.loop = false; 
        audioSource.Play();
    }
}
