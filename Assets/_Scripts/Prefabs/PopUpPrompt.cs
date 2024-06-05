using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PopUpPrompt : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private bool isDialogueCompleted;
    [SerializeField] private Collider2D promptCollider;

    [Header("Technical References")]
    [SerializeField] private PlayerInput playerInput = null;

    [Header("Building Block References")]
    [SerializeField] private Computer _computer;
    [SerializeField] private PopUpPrompt_Effects _dialoguePrompt_Effects;

    [Header("Tags")]
    [SerializeField] private ScriptObj_Tag _playerTag;

    private void Awake()
    {
        playerInput = new PlayerInput(); // Instantiate new Unity's Input System
    }

    private void OnEnable()
    {
        //// Subscribes to Unity's input system
        playerInput.Enable();
    }

    private void OnDestroy()
    {
        //// Unubscribes to Unity's input system
        playerInput.Player.Interact.performed -= OnInteractPerformed;
        playerInput.Disable();
    }

    public void OnPopUpStart()
    {
        playerInput.Player.Interact.performed -= OnInteractPerformed;
        _dialoguePrompt_Effects.OnPopUpStart();
    }

    public void OnPopUpEnd()
    {
        if (CheckExistingObjects() == true)
        {
            playerInput.Player.Interact.performed -= OnInteractPerformed;
            _dialoguePrompt_Effects.OnPopUpEnd();
        }
    }

    private void OnInteractPerformed(InputAction.CallbackContext value)
    {
        _computer.OnInteract();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Tags>(out var _tags))
        {
            if (_tags.CheckTags(_playerTag.name) == true)
            {
                playerInput.Player.Interact.performed += OnInteractPerformed;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Tags>(out var _tags))
        {
            if (_tags.CheckTags(_playerTag.name) == true)
            {
                playerInput.Player.Interact.performed -= OnInteractPerformed;
            }
        }
    }

    private bool CheckExistingObjects()
    {
        List<Collider2D> colliders = new List<Collider2D>();
        Physics2D.OverlapCollider(promptCollider, new ContactFilter2D(), colliders);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.TryGetComponent<Tags>(out var _tags))
            {
                if (_tags.CheckTags(_playerTag.name) == true)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
