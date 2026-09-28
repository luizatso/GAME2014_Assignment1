using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MenuMusic : MonoBehaviour
{
    private static MenuMusic instance;
    private AudioSource musicSource;

    private void Awake()
    {
        // Remove duplicate music players when returning to a menu.
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        musicSource = GetComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.Play();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Keep playing throughout these three scenes
        if (scene.name == "MenuScene" ||
            scene.name == "InstructionsScene" ||
            scene.name == "QuitScene")
        {
            return;
        }

        // Stop menu music when entering gameplay or another scene.
        musicSource.Stop();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            instance = null;
        }
    }
}