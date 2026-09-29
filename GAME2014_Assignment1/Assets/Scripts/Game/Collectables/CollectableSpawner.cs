using UnityEngine;
using System.Collections.Generic;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> collectablePrefabs;

    public void SpawnCollectable(Vector2 position)
    {
        int index = Random.Range(0, collectablePrefabs.Count);
        var selectedCollectable = collectablePrefabs[index];

        Instantiate(selectedCollectable, position, Quaternion.identity);
    }
}
