using UnityEngine;

// Updated 2026-09-14 by Claude (Anthropic AI assistant):
// - Replaced the direct "obstacle" prefab + Instantiate/Destroy with an
//   ObjectPool reference ("obstaclePool") so obstacles are pooled instead
//   of constantly instantiated and destroyed.
// - Moved the spawnRate decrease out of the per-frame Update loop and into
//   GetLevel(), so it now fires once per level-up instead of every single
//   frame (previously it crashed to its floor almost instantly once level
//   reached 2). Clamped with Mathf.Max(minSpawnRate, ...).
// - SpawnObstacle() now calls obstaclePool.Get(...) and passes the current
//   level to the obstacle via ObstacleMovement.Initialize(...).
// - Added a null-check on obstaclePool before the initial Start() spawn
//   (previously an unassigned prefab would throw immediately).
public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool obstaclePool;

    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float minSpawnRate = 0.3f;
    [SerializeField] private float heightOffset = 2.3f;
    [SerializeField] private float decreaseSpawnRate = 0.1f;

    private float timer = 0f;
    private int level = 1;

    private void Start()
    {
        SpawnObstacle();
    }

    private void Update()
    {
        Tick();
    }

    private void OnEnable()
    {
        EventManager.OnPlayerDeath += StopGameOnPlayerDeath;
        EventManager.OnLevelIncrease += GetLevel;
    }

    private void OnDisable()
    {
        EventManager.OnLevelIncrease -= GetLevel;
        EventManager.OnPlayerDeath -= StopGameOnPlayerDeath;
    }

    private void Tick()
    {
        if (obstaclePool == null)
        {
            return;
        }

        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    private void GetLevel(int newLevel)
    {
        level = newLevel;
        // Decrease once per level-up event, not once per frame -
        // previously this ran inside Update() and crashed spawnRate to
        // its floor within a fraction of a second of reaching level 2.
        spawnRate = Mathf.Max(minSpawnRate, spawnRate - decreaseSpawnRate);
    }

    private void SpawnObstacle()
    {
        if (obstaclePool == null)
        {
            return;
        }

        float highestPoint = transform.position.y + heightOffset;
        float lowestPoint = transform.position.y - heightOffset;
        Vector3 spawnPosition = new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0f);

        GameObject spawned = obstaclePool.Get(spawnPosition, Quaternion.identity);
        if (spawned.TryGetComponent(out ObstacleMovement movement))
        {
            movement.Initialize(obstaclePool, level);
        }
    }

    private void StopGameOnPlayerDeath()
    {
        gameObject.SetActive(false);
    }
}
