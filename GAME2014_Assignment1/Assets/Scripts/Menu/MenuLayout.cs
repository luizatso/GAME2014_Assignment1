using UnityEngine;

public class MenuLayout : MonoBehaviour
{
    [Header("Menu Elements")]
    public RectTransform title;
    public RectTransform startButton;
    public RectTransform instructionsButton;
    public RectTransform quitButton;

    private Vector2 portraitTitlePosition;
    private Vector2 portraitStartPosition;
    private Vector2 portraitInstructionsPosition;
    private Vector2 portraitQuitPosition;

    private bool previousLandscape;
    private bool layoutApplied;

    private void Awake()
    {
        // Save the original positions set in the scene.
        portraitTitlePosition = title.anchoredPosition;
        portraitStartPosition = startButton.anchoredPosition;
        portraitInstructionsPosition = instructionsButton.anchoredPosition;
        portraitQuitPosition = quitButton.anchoredPosition;
    }

    private void Start()
    {
        UpdateLayout();
    }

    private void Update()
    {
        bool isLandscape = Screen.width > Screen.height;

        if (!layoutApplied || isLandscape != previousLandscape)
        {
            UpdateLayout();
        }
    }

    private void UpdateLayout()
    {
        bool isLandscape = Screen.width > Screen.height;

        if (isLandscape)
        {
            title.anchoredPosition = new Vector2(0f, -10f);
            startButton.anchoredPosition = new Vector2(0f, 30f);
            instructionsButton.anchoredPosition = new Vector2(0f, -50f);
            quitButton.anchoredPosition = new Vector2(0f, -130f);
        }
        else
        {
            title.anchoredPosition = portraitTitlePosition;
            startButton.anchoredPosition = portraitStartPosition;
            instructionsButton.anchoredPosition =
                portraitInstructionsPosition;
            quitButton.anchoredPosition = portraitQuitPosition;
        }

        previousLandscape = isLandscape;
        layoutApplied = true;
    }
}