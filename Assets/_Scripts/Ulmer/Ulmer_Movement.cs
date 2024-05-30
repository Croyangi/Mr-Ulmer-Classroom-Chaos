using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ulmer_Movement : MonoBehaviour
{
    [Header("General References")]
    public Rigidbody2D rb;

    [Header("Technical References")]
    [SerializeField] private PlayerInput playerInput = null;

    [Header("Building Block References")]
    [SerializeField] private Ulmer_MovementVariables _movementVars;
    [SerializeField] private Ulmer_Animator _animator;

    private void Awake()
    {
        playerInput = new PlayerInput(); // Instantiate new Unity's Input System
        _movementVars.rawInputMovement = Vector2.zero; // Prevents "sticky" inputs before the scene runs
    }

    private void OnEnable()
    {
        //// Subscribes to Unity's input system
        playerInput.Player.Movement.performed += OnMovementPerformed;
        playerInput.Player.Movement.canceled += OnMovementCancelled;
        playerInput.Enable();
    }

    private void OnDisable()
    {
        //// Unubscribes to Unity's input system
        playerInput.Player.Movement.performed -= OnMovementPerformed;
        playerInput.Player.Movement.canceled -= OnMovementCancelled;
        playerInput.Disable();
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        _movementVars.rawInputMovement = value.ReadValue<Vector2>(); // Reads input as Vector2
        _animator.ChangeAnimationState(_animator.ULMER_MOVING);
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        _movementVars.rawInputMovement = Vector2.zero; // Resets input
        _animator.ChangeAnimationState(_animator.ULMER_IDLE);
    }

    public void SetInputStall(bool state)
    {
        if (state)
        {
            //// Subscribes to Unity's input system
            playerInput.Player.Movement.performed += OnMovementCancelled;
            playerInput.Player.Movement.canceled += OnMovementCancelled;
        }
        else
        {
            // Resets player movement
            _movementVars.rawInputMovement = Vector2.zero;

            //// Unubscribes to Unity's input system
            playerInput.Player.Movement.performed -= OnMovementPerformed;
            playerInput.Player.Movement.canceled -= OnMovementCancelled;
        }
    }

    private void MovementInputProcessor()
    {
        _movementVars.processedInputMovement = new Vector2(Mathf.RoundToInt(_movementVars.rawInputMovement.x), Mathf.RoundToInt(_movementVars.rawInputMovement.y));

        if (_movementVars.isMovementStalled)
        {
            _movementVars.processedInputMovement = Vector2.zero;
        }
    }

    private void MainMovementMath()
    {
        // Math.Sign is because Unity's input can give float values if diagonal movement
        float xTargetSpeed = _movementVars.processedInputMovement.x * _movementVars.movementSpeed;
        float xSpeedDif = xTargetSpeed - rb.velocity.x;
        float xAccelRate = (Mathf.Abs(xTargetSpeed) > 0.01f) ? _movementVars.acceleration : _movementVars.deceleration;
        float xMovement = Mathf.Pow(Mathf.Abs(xSpeedDif) * xAccelRate, _movementVars.velocityPower) * Mathf.Sign(xSpeedDif);

        float yTargetSpeed = _movementVars.processedInputMovement.y * _movementVars.movementSpeed;
        float ySpeedDif = yTargetSpeed - rb.velocity.y;
        float yAccelRate = (Mathf.Abs(yTargetSpeed) > 0.01f) ? _movementVars.acceleration : _movementVars.deceleration;
        float yMovement = Mathf.Pow(Mathf.Abs(ySpeedDif) * yAccelRate, _movementVars.velocityPower) * Mathf.Sign(ySpeedDif);

        rb.AddForce(new Vector2(xMovement, yMovement));
    }

    private void FixedUpdate()
    {
        MovementInputProcessor(); // Processes Raw Input to Processed Input

        MainMovementMath(); // Main movement math
    }
}
