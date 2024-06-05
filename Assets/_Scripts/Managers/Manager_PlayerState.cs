using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_PlayerState : MonoBehaviour
{
    public GameObject player;

    public static Manager_PlayerState instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Sanity Manager in the scene.");
        }

        instance = this;
    }
}
