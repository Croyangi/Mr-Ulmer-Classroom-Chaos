using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Handler_MainMenu : MonoBehaviour
{
    [Header("Tabs")]
    [SerializeField] private GameObject[] tabs;

    [Header("References")]
    [SerializeField] private GameObject story;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject controls;


    private void Awake()
    {
        story.SetActive(false);
        credits.SetActive(false);
        controls.SetActive(false);
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

    public void OnStoryButtonPressed()
    {
        story.SetActive(!story.activeSelf);
        credits.SetActive(false);
        controls.SetActive(false);
    }

    public void OnControlsButtonPressed()
    {
        controls.SetActive(!controls.activeSelf);
        credits.SetActive(false);
        story.SetActive(false);
    }


    public void OnCreditsButtonPressed()
    {
        credits.SetActive(!credits.activeSelf);
        controls.SetActive(false);
        story.SetActive(false);
    }

    public void OnExitButtonPressed()
    {
        Application.Quit();
    }
}
