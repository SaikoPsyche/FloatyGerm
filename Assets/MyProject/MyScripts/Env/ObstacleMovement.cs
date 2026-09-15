using UnityEngine;

// Updated 2026-09-14 by Claude (Anthropic AI assistant):
// - Moved EventManager.OnLevelIncrease subscription from Start() to
//   OnEnable()/OnDisable() to fix a memory leak (obstacles were never
//   unsubscribing before being destroyed) and to support object pooling.
// - LevelIncrease() now applies a flat speedMultiplier step per event
//   instead of speedMultiplier * (level - 1), so obstacles no longer end
//   up at inconsistent speeds depending on when they spawned.
// - Added Initialize(pool, currentLevel) so newly spawned/reused obstacles
//   start at the correct speed for the current level.
// - Added ReturnToPool()/sourcePool so obstacles are released back to an
//   ObjectPool instead of being destroyed (falls back to Destroy() if no
//   pool was assigned).
public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float boundary = -25f;
    [SerializeField] private float speedMultiplier = 0.5f;

    private float baseMoveSpeed;
    private ObjectPool sourcePool;

    private void Awake()
    {
        // Cache the Inspector value once; OnEnable resets to this every
        // time the obstacle is reused from the pool.
        baseMoveSpeed = moveSpeed;
    }

    private void OnEnable()
    {
        moveSpeed = baseMoveSpeed;
        EventManager.OnLevelIncrease += LevelIncrease;
    }

    private void OnDisable()
    {
        // Subscribing/unsubscribing on enable/disable (instead of Start
        // with no unsubscribe) means pooled-and-reused obstacles never
        // pile up extra subscriptions, and released obstacles stop
        // listening immediately - fixes the memory leak.
        EventManager.OnLevelIncrease -= LevelIncrease;
    }

    private void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < boundary)
        {
            ReturnToPool();
        }
    }

    /// <summary>
    /// Called by the spawner right after Get()-ing this obstacle, so it
    /// starts at the correct speed for the current level instead of
    /// always starting from the base speed.
    /// </summary>
    public void Initialize(ObjectPool pool, int currentLevel)
    {
        sourcePool = pool;
        moveSpeed = baseMoveSpeed + speedMultiplier * (currentLevel - 1);
    }

    private void LevelIncrease(int level)
    {
        // Flat step per level-up event (it fires once per level change),
        // so every obstacle alive at a given level ends up at the same
        // speed regardless of when it spawned.
        moveSpeed += speedMultiplier;
    }

    private void ReturnToPool()
    {
        if (sourcePool != null)
        {
            sourcePool.Release(gameObject);
        }
        else
        {
            // Fallback in case this obstacle wasn't spawned through a pool.
            Destroy(gameObject);
        }
    }
}
