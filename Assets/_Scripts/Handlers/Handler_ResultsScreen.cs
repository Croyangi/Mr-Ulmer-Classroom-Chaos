using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Handler_ResultsScreen : MonoBehaviour
{
    [Header("Tabs")]
    [SerializeField] private GameObject[] tabs;

    [Header("References")]
    [SerializeField] private GameObject score;
    [SerializeField] private GameObject credits;

    [SerializeField] private TextMeshProUGUI tmp_time;
    [SerializeField] private TextMeshProUGUI tmp_score;

    [SerializeField] private AudioClip music_boombox;


    private void Awake()
    {
        Manager_SFXPlayer.instance.PlaySFXClip(music_boombox, transform, 1f, true);
        score.SetActive(true);
        credits.SetActive(false);
        CalculateScore();
    }

    private void CalculateScore()
    {
        float score = ScoreCalculator.instance.CalculateScore();
        tmp_score.text = "Score: " + Mathf.RoundToInt(score);

        TimeSpan speedrunTime = TimeSpan.FromSeconds(ScoreCalculator.instance.time);
        string time = speedrunTime.Minutes.ToString("00") + ":" +
                              speedrunTime.Seconds.ToString("00") + "." +
                              speedrunTime.Milliseconds.ToString("000");
        tmp_time.text = "Time Spent Sane: " + time;
    }

    // Set tab active
    public void SetActiveTabVFX(int index)
    {
        GameObject tab = tabs[index];

        LeanTween.moveX(tab.GetComponent<RectTransform>(), 600f, 0.5f).setEaseInBack().setEaseOutBounce();
    }

    // Set tab deactive
    public void SetDeactiveTabVFX(int index)
    {
        GameObject tab = tabs[index];

        LeanTween.moveX(tab.GetComponent<RectTransform>(), 400f, 0.5f).setEaseInBack().setEaseOutBounce();
    }

    //// Buttons
    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene("ClassroomArcade");
    }

    public void OnScoreButtonPressed()
    {
        score.SetActive(!score.activeSelf);
        credits.SetActive(false);
    }


    public void OnCreditsButtonPressed()
    {
        credits.SetActive(!credits.activeSelf);
        score.SetActive(false);
    }

    public void OnExitButtonPressed()
    {
        Application.Quit();
    }

    public void OpenLink()
    {
        Application.OpenURL("https://discord.gg/DVh5eCWNfs");
    }
}
