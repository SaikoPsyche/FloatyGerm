using System;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float boundary = -25f;
    [SerializeField] private float speedMultiplier = 0.5f;

    private void Start()
    {
        EventManager.OnLevelIncrease += LevelIncrease;
    }

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < boundary)
        {
            Destroy(gameObject);
        }
    }

    private void LevelIncrease(int level)
    {
        moveSpeed += speedMultiplier * (level - 1);
    }
}
