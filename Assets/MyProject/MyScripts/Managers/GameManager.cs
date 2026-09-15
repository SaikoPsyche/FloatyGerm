using UnityEngine;
using UnityEngine.SceneManagement;

// Updated 2026-09-14 by Claude (Anthropic AI assistant):
// - StartGame() now checks the next build index exists before loading it,
//   instead of assuming activeScene + 1 is always valid.
// - QuitGame() now stops Play Mode in the Editor instead of silently doing
//   nothing (Application.Quit() is a no-op in the Editor).
// - Left DontDestroyOnLoad out deliberately: whether this GameManager
//   should persist across scene loads is an architecture decision, not a
//   bug fix, and adding it without a duplicate-instance guard could cause
//   multiple GameManagers to pile up.
public class GameManager : MonoBehaviour
{
    private int activeScene;

    private void Awake() => activeScene = SceneManager.GetActiveScene().buildIndex;

    public void StartGame()
    {
        int nextScene = activeScene + 1;
        if (nextScene < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Debug.LogWarning($"GameManager: no scene at build index {nextScene} - check Build Settings.");
        }
    }

    public void RestartScene() => SceneManager.LoadScene(activeScene);

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
