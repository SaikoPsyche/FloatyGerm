using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;

    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float heightOffset = 2.3f;
    [SerializeField] private float decreaseSpawnRate = 0.1f;

    private float timer = 0f;

    private int level;

    
    // Start is called before the first frame update
    private void Start()
    {
        SpawnObstacle();
    }

    // Update is called once per frame
    private void Update()
    {
        SpawnRate();
    }

    private void OnEnable()
    {
        EventManager.OnPlayerDeath += StopGameOnPlayerDeath;
        EventManager.OnLevelIncrease += GetLevel;
    }

    

    private void SpawnRate()
    {
        if (obstacle != null)
        {
            if (spawnRate > 0.3f)
            {
                spawnRate -= decreaseSpawnRate * (level - 1);
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
    }

    private void GetLevel(int level)
    {
        this.level = level;
    }


    private void SpawnObstacle()
    {
        float highestPoint = transform.position.y + heightOffset;
        float lowestPoint = transform.position.y - heightOffset;
        Vector3 spawnPosition = new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0f);

        Instantiate(obstacle, spawnPosition, Quaternion.identity, this.transform);
    }

    private void StopGameOnPlayerDeath()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        EventManager.OnLevelIncrease -= GetLevel;
        EventManager.OnPlayerDeath -= StopGameOnPlayerDeath;
    }
}
