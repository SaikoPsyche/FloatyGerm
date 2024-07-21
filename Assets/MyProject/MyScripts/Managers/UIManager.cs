using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [Header("Debug Settings - For Visibility Only")]

    [Header("Input - Internal reference")]

    [SerializeField] private PlayerInputActions playerInputActions;
    [SerializeField] private InputAction PlayerMovement;
    [SerializeField] private InputAction PauseGame;

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
        if (context.performed)
        {
            Pause();
        }
    }

    public void Pause()
    {
        gameIsPaused = !gameIsPaused;

        OnApplicationPause(gameIsPaused);
    }

    public void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

}
