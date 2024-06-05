using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    [SerializeField] public float time;

    public static ScoreCalculator instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Sanity Manager in the scene.");
        }
        instance = this;

        DontDestroyOnLoad(transform.gameObject);
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;
    }

    public float CalculateScore()
    {
        return time * 500f;
    }
}
