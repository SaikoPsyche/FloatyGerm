using UnityEngine;

// Updated 2026-09-14 by Claude (Anthropic AI assistant):
// - Added a duplicate-instance guard in Awake() so a second SoundFX in the
//   scene destroys itself instead of silently overwriting "instance".
// - Left DontDestroyOnLoad out deliberately, same reasoning as
//   GameManager: whether this should persist across scene reloads is a
//   design decision, not a bug fix.
// - Left "deathSound" as-is (unused in this file, and public unlike the
//   other fields) rather than guessing at its intended purpose - it may
//   be referenced from a script outside this review.
public class SoundFX : MonoBehaviour
{
    public static SoundFX instance;

    [SerializeField] private AudioSource touchSound;
    [SerializeField] private AudioSource levelUpSound;
    [SerializeField] private AudioSource loseSound;
    public AudioSource deathSound;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

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
