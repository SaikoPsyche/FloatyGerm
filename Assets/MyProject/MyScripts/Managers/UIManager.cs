using UnityEngine;
using UnityEngine.InputSystem;

// Updated 2026-09-14 by Claude (Anthropic AI assistant):
// - Renamed the custom OnApplicationPause(bool) method to ApplyPauseState
//   (bool). OnApplicationPause is a real Unity engine callback that fires
//   automatically when the app loses/regains OS focus, so the old name
//   collided with it - pressing your pause button and the OS pausing the
//   app both called the same method, which could silently un-pause the
//   game on refocus. If anything in the Inspector was wired directly to
//   "OnApplicationPause", rewire it to "ApplyPauseState".
// - Removed the unused "PlayerMovement" InputAction field.
// - Removed the redundant "if (context.performed)" check inside
//   IfPauseGame() - it's only ever invoked because it's subscribed to
//   PauseGame.performed, so that condition was always true.
// - Moved the "Debug Settings" header so it actually sits above
//   gameIsPaused instead of above nothing.
public class UIManager : MonoBehaviour
{
    [Header("Input - Internal reference")]
    [SerializeField] private PlayerInputActions playerInputActions;
    [SerializeField] private InputAction PauseGame;

    [Header("Debug Settings - For Visibility Only")]
    [SerializeField] private bool gameIsPaused = false;

    private void Awake() => playerInputActions = new();

    private void OnEnable()
    {
        PauseGame = playerInputActions.UI.Pause;
        PauseGame.performed += IfPauseGame;
        PauseGame.Enable();
    }

    private void OnDisable()
    {
        PauseGame.performed -= IfPauseGame;
        PauseGame.Disable();
    }

    private void IfPauseGame(InputAction.CallbackContext context)
    {
        Pause();
    }

    public void Pause()
    {
        gameIsPaused = !gameIsPaused;
        ApplyPauseState(gameIsPaused);
    }

    public void ApplyPauseState(bool pause)
    {
        Time.timeScale = pause ? 0f : 1f;
    }
}
