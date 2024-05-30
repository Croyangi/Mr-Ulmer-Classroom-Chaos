using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Ulmer_Animator : MonoBehaviour
{
    [Header("General References")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Rigidbody2D rb;

    [Header("State References")]
    public string currentState;
    public string ULMER_MOVING = "Anim_Ulmer_Moving";
    public string ULMER_IDLE = "Anim_Ulmer_Idle";


    private void Awake()
    {
        ChangeAnimationState(ULMER_IDLE);
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
        {
            return;
        }
        animator.Play(newState);
        currentState = newState;
    }

    private void FixedUpdate()
    {
        SpriteFlipUpdate();
    }

    private void SpriteFlipUpdate()
    {
        if (rb.velocity.x > 0.1)
        {
            sr.flipX = false;
        }
        else if (rb.velocity.x < -0.1)
        {
            sr.flipX = true;
        }
    }
}
