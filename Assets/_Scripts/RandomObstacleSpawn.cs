using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObstacleSpawn : MonoBehaviour
{
    [SerializeField] private GameObject obstacle0;
    [SerializeField] private GameObject obstacle1;

    private bool GetRandomBool()
    {
        return (Random.value > 0.5f);
    }

    private void Awake()
    {
        if (GetRandomBool())
        {
            obstacle0.SetActive(true);
            obstacle1.SetActive(false);
        } else
        {
            obstacle0.SetActive(false);
            obstacle1.SetActive(true);
        }
    }
}
