using UnityEngine;

public class SoundFX : MonoBehaviour
{
    public static SoundFX instance;

    [SerializeField] private AudioSource touchSound;
    [SerializeField] private AudioSource levelUpSound;
    [SerializeField] private AudioSource loseSound;
    public AudioSource deathSound;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        EventManager.OnTouchScreen += TouchScreenSound;
        EventManager.OnLevelIncrease += LevelUpSound;
        EventManager.OnPlayerDeath += PlayerLoseSound;
    }

    private void OnDisable()
    {
        EventManager.OnTouchScreen -= TouchScreenSound;
        EventManager.OnLevelIncrease -= LevelUpSound;
        EventManager.OnPlayerDeath -= PlayerLoseSound;
    }

    private void TouchScreenSound()
    {
        touchSound.Play();
    }

    private void LevelUpSound(int level)
    {
        levelUpSound.Play();
    }

    private void PlayerLoseSound()
    {
        loseSound.Play();
    }
}
