using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private GameObject stats;
    [SerializeField] private GameObject loseScreen;

    // Start is called before the first frame update
    private void OnEnable() => EventManager.OnPlayerDeath += PlayerDeath;
    private void OnDisable() => EventManager.OnPlayerDeath -= PlayerDeath;

    private void PlayerDeath()
    {
        loseScreen.SetActive(true);
        stats.SetActive(false);
    }
}
