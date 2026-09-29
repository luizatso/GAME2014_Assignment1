using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MainMenu : MonoBehaviour
{
    public AudioClip clickSound;

    [Range(0f, 1f)]
    public float clickVolume = 0.5f;

    private AudioSource audioSource;
    private bool isTransitioning;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0f;
    }

    public void StartGame()
    {
        ChangeScene("GameScene");
    }

    public void OpenInstructions()
    {
        ChangeScene("InstructionsScene");
    }

    public void OpenQuitScene()
    {
        ChangeScene("QuitScene");
    }

    public void BackToMenu()
    {
        ChangeScene("MenuScene");
    }

    private void ChangeScene(string sceneName)
    {
        if (isTransitioning)
            return;

        isTransitioning = true;
        StartCoroutine(PlayClickThenChangeScene(sceneName));
    }

    private IEnumerator PlayClickThenChangeScene(string sceneName)
    {
        yield return PlayClick();

        SceneManager.LoadSceneAsync(sceneName);
    }

    private IEnumerator PlayClick()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound, clickVolume);
            yield return new WaitForSecondsRealtime(clickSound.length);
        }
    }

    public void QuitGame()
    {
        if (isTransitioning)
            return;

        isTransitioning = true;
        StartCoroutine(PlayClickThenQuit());
    }

    private IEnumerator PlayClickThenQuit()
    {
        yield return PlayClick();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}