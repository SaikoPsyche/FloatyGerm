using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int activeScene;

    private void Awake() => activeScene = SceneManager.GetActiveScene().buildIndex;

    public void StartGame() => SceneManager.LoadScene(activeScene + 1);

    public void RestartScene() => SceneManager.LoadScene(activeScene);

    public void QuitGame() => Application.Quit();
}
