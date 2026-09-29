using UnityEngine;

public class EnemyScoreAllocator : MonoBehaviour
{
    [SerializeField] private int killScore;

    private ScoreController scoreController;

    private void Awake()
    {
        scoreController = FindObjectOfType<ScoreController>();
    }

    public void AllocateScore()
    {
        scoreController.AddScore(killScore);
    }
}
