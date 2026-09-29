using UnityEngine;

public class EnemyCollectableDrop : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)]
    private float chanceOfCollectableDrop;

    private CollectableSpawner collectableSpawner;

    private void Awake()
    {
        collectableSpawner = FindAnyObjectByType<CollectableSpawner>();
    }

    public void RandomlyDropCollectable()
    {
        if (collectableSpawner == null)
        {
            return;
        }

        float random = Random.Range(0f, 1f);

        if (random < chanceOfCollectableDrop)
        {
            collectableSpawner.SpawnCollectable(transform.position);
        }
    }
}