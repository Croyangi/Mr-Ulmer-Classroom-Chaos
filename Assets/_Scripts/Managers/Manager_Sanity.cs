using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager_Sanity : MonoBehaviour
{
    [SerializeField] private int sanityRate;
    [SerializeField] private float sanity;
    [SerializeField] private float maxSanity;
    [SerializeField] private int sanityRateSeconds;
    [SerializeField] private int startingSanity;

    [SerializeField] private int recoverySanityRate;

    [SerializeField] private Image sanityBarImage;
    [SerializeField] private GameObject sanityBar;

    [SerializeField] private float timeBetween;
    [SerializeField] private float scaledUp;
    [SerializeField] private float scaledDown;

    [SerializeField] private float time;
    [SerializeField] public float difficultyMultiplier;

    public static Manager_Sanity instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Sanity Manager in the scene.");
        }
        instance = this;

        sanity = startingSanity;
        StartCoroutine(SanityClock());
        StartCoroutine(PulseEffect());
    }


    // Sanity rate affects sanity every second
    public void ChangeSanityRate(int change)
    {
        sanityRate += change;
    }

    private IEnumerator SanityClock()
    {
        if (sanity < maxSanity && sanityRate == 0)
        {
            sanity += (recoverySanityRate * (1f - difficultyMultiplier));
        }

        sanity += (sanityRate * (1f + difficultyMultiplier));
        sanity = Mathf.Clamp(sanity, 0, maxSanity);

        yield return new WaitForSeconds(sanityRateSeconds);
        StartCoroutine(SanityClock());
    }

    private void SetSanityVFX()
    {
        sanityBarImage.fillAmount = (sanity / maxSanity);
        Debug.Log(sanity / maxSanity);
    }

    private void FixedUpdate()
    {
        SetSanityVFX();
        time += Time.deltaTime;
        difficultyMultiplier = (time / 160f);
        difficultyMultiplier = Mathf.Clamp(difficultyMultiplier, 0f, 0.80f);

        if (sanity <= 0f)
        {
            SceneManager.LoadScene("ResultsScreen");
        }
    }

    private IEnumerator PulseEffect()
    {
        if (sanity < (maxSanity / 3))
        {
            LeanTween.scale(sanityBar, Vector3.one * scaledUp * 1.01f, 0.05f);
            LeanTween.scale(sanityBar, Vector3.one * scaledDown, 0.1f).setEaseInOutBounce().setDelay(0.05f);
            yield return new WaitForSeconds(timeBetween * 0.5f);
        } else
        {
            LeanTween.scale(sanityBar, Vector3.one * scaledUp, 0.05f);
            LeanTween.scale(sanityBar, Vector3.one * scaledDown, 0.1f).setDelay(0.05f);
            yield return new WaitForSeconds(timeBetween);
        }
        StartCoroutine(PulseEffect());
    }
}
