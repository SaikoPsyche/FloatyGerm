using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 7.5f;

    private Rigidbody2D rb;
    // private ParticleSystem ParticleSystem;
    private PlayerInputActions playerInputActions;

    private bool isJumping = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // ParticleSystem = GetComponentInChildren<ParticleSystem>();

        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.Player.Jump.started += OnJump;
        playerInputActions.Player.Jump.performed += IsJumping;
        playerInputActions.Player.Jump.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Player.Jump.Disable();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Obstacle"))
        {
            EventManager.onScorePoint();
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isJumping == false)
            {
                isJumping = true;
                EventManager.onTouchScreen();
            }
            
            /*if (ParticleSystem != null)
            {
                ParticleSystem.Play(true);
            }*/
        }
    }

    public void IsJumping(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
                isJumping = false;
                rb.linearVelocity = Vector2.up * jumpHeight;
        }
    }
}
