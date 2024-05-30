using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ulmer_MovementVariables : MonoBehaviour
{
    public bool isMovementStalled;

    [Header("Velocity")]
    public float acceleration;
    public float deceleration;
    public float velocityPower;
    public float movementSpeed;

    [Header("General Movement")]
    public Vector2 rawInputMovement;
    public Vector2 processedInputMovement;
}
