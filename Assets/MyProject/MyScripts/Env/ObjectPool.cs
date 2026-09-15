using System.Collections.Generic;
using UnityEngine;

// Added 2026-09-14 by Claude (Anthropic AI assistant):
// New file - simple reusable object pool created to replace
// Instantiate/Destroy calls in ObstacleSpawner/ObstacleMovement.
/// <summary>
/// Simple, reusable single-prefab object pool.
///
/// Setup: put this component on the same GameObject as ObstacleSpawner,
/// assign the obstacle prefab to "prefab" here, then drag this component
/// into ObstacleSpawner's "obstaclePool" field.
/// </summary>
public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int initialSize = 10;

    private readonly Queue<GameObject> available = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < initialSize; i++)
        {
            available.Enqueue(CreateInstance());
        }
    }

    private GameObject CreateInstance()
    {
        GameObject instance = Instantiate(prefab, transform);
        instance.SetActive(false);
        return instance;
    }

    public GameObject Get(Vector3 position, Quaternion rotation)
    {
        GameObject instance = available.Count > 0 ? available.Dequeue() : CreateInstance();
        instance.transform.SetPositionAndRotation(position, rotation);
        instance.SetActive(true);
        return instance;
    }

    public void Release(GameObject instance)
    {
        instance.SetActive(false);
        available.Enqueue(instance);
    }
}
