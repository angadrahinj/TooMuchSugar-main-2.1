using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotDestroy : MonoBehaviour
{
    public AudioClip audioClip;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        DontDestroyOnLoad(gameObject); // Ensure the object persists between scenes

        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene loaded event
    }

    void Update()
    {
        // Check if audio is not playing and it should be played in the current scene
        if (!audioSource.isPlaying && ShouldPlayInCurrentScene())
        {
            audioSource.Play();
        }
    }

    // Function to determine if audio should be played in the current scene
    bool ShouldPlayInCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        return currentSceneName == "Splash Screen" || currentSceneName == "Main Menu";
    }

    // Event handler for scene loaded event
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Stop the audio if it's not supposed to play in the newly loaded scene
        if (!ShouldPlayInCurrentScene())
        {
            audioSource.Stop();
        }
    }
}
