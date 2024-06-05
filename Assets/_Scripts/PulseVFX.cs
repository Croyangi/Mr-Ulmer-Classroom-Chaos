using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseVFX : MonoBehaviour
{
    [SerializeField] private float timeBetween;
    [SerializeField] private float scaledUp;
    [SerializeField] private float scaledDown;

    private void Awake()
    {
        StartCoroutine(PulseEffect());
    }

    private IEnumerator PulseEffect()
    {
        LeanTween.scale(gameObject, Vector3.one * scaledUp, 0.05f);
        LeanTween.scale(gameObject, Vector3.one * scaledDown, 0.1f).setEaseInOutBounce().setDelay(0.05f);
        yield return new WaitForSeconds(timeBetween);
        StartCoroutine(PulseEffect());
    }
}
