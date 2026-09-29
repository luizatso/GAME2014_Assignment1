using UnityEngine;

public class EnemyScoreAllocator : MonoBehaviour
{
    [SerializeField] private int killScore;

    private ScoreController scoreController;

    private void Awake()
    {
        scoreController = FindAnyObjectByType<ScoreController>();
    }

    public void AllocateScore()
    {
        if (scoreController != null)
        {
            scoreController.AddScore(killScore);
        }
    }
}