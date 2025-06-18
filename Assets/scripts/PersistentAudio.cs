using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentAudio : MonoBehaviour
{
    private static PersistentAudio instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Avoid duplicates
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // Subscribe to scene change event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Clean up
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Play")
        {
            Destroy(gameObject);
        }
    }
}
