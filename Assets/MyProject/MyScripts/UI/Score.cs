using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    // [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    [SerializeField] private int score = 0;
    [SerializeField] private int scoreCap = 15;
    [SerializeField] private int scoreMultiplier = 10;

    private static int level = 1;
    private static int highScore;
    private static string scoreName = "score";

    public static int Level
    {
        get { return level;}
        set { level = value; }
    }

    
    private void Start()
    {
        scoreText.text = $"Score: {score}";
        // levelText.text = $"Level: {level}";
    }

    private void Update()
    {
        LevelIncrease();
    }

    // Update is called once per frame
    private void OnEnable()
    {
        EventManager.OnScorePoint += ScorePoint;
        EventManager.OnPlayerDeath += SaveHighScore;
    }

    private void OnDisable()
    {
        EventManager.OnScorePoint -= ScorePoint;
        EventManager.OnPlayerDeath -= SaveHighScore;
    }

    private void ScorePoint()
    {
        score++;
        scoreText.text = $"Score: {score}";
    }

    private void LevelIncrease()
    {
        if (score >= scoreCap)
        {
            level++;
            // levelText.text = $"Level: {level}";
            EventManager.onLevelIncrease(level);
            scoreCap += scoreMultiplier;
            scoreMultiplier++;
        }
    }

    private void SaveHighScore()
    {
        // Display date and score
        if (score > highScore) // Save score if higher than previous highscore
        {
            PlayerPrefs.SetInt(scoreName, score);

            highScore = PlayerPrefs.GetInt(scoreName, 0);
        }

        highScoreText.text = $"Your highscore is \n {highScore} points!";
    }
}
