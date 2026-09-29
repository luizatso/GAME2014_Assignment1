using UnityEngine;

public class InstructionsLayout : MonoBehaviour
{
    [Header("Instructions Elements")]
    public RectTransform backButton;
    public RectTransform instructionImage;
    public RectTransform instructionText;
    public RectTransform instructionScriptText;

    private Vector2 portraitBackPosition;
    private Vector2 portraitImagePosition;
    private Vector2 portraitScriptTextPosition;

    private Vector3 portraitImageScale;
    private Vector3 portraitTextScale;
    private Vector3 portraitScriptTextScale;

    private bool previousLandscape;
    private bool layoutApplied;

    private void Awake()
    {
        // Save the original portrait positions and scales.
        portraitBackPosition = backButton.anchoredPosition;
        portraitImagePosition = instructionImage.anchoredPosition;
        portraitScriptTextPosition = instructionScriptText.anchoredPosition;

        portraitImageScale = instructionImage.localScale;
        portraitTextScale = instructionText.localScale;
        portraitScriptTextScale = instructionScriptText.localScale;
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
            backButton.anchoredPosition = new Vector2(0f, 40f);

            instructionImage.anchoredPosition = new Vector2(0f, 30f);
            instructionImage.localScale = new Vector3(1.75f, 0.5f, 1f);

            instructionText.localScale = new Vector3(1f, 1.5f, 1f);

            instructionScriptText.anchoredPosition = new Vector2(0f, -145f);
            instructionScriptText.localScale = new Vector3(1f, 1.5f, 1f);
        }
        else
        {
            backButton.anchoredPosition = portraitBackPosition;

            instructionImage.anchoredPosition = portraitImagePosition;
            instructionImage.localScale = portraitImageScale;

            instructionText.localScale = portraitTextScale;

            instructionScriptText.anchoredPosition = portraitScriptTextPosition;
            instructionScriptText.localScale = portraitScriptTextScale;
        }

        previousLandscape = isLandscape;
        layoutApplied = true;
    }
}