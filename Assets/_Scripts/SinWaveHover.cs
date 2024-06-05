using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinWaveHover : MonoBehaviour
{
    [Header("General References")]

    [Header("Rise/Fall Visual Settings")]
    [SerializeField] private float _amplitude = 0;
    [SerializeField] private float _frequency = 1;
    [SerializeField] private float time;
    [SerializeField] private float offsetTime;

    private void Awake()
    {
        time += offsetTime;
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;
        float y = Mathf.Sin(time * _frequency) * _amplitude;
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + y);
    }
}
