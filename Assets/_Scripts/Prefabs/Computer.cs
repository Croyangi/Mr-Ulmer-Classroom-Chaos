using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Computer : MonoBehaviour, IComputerHandler
{

    [Serializable]
    public class ComputerHazard
    {
        public GameObject variant;
        public int sanityChange;
        public GameObject popUp;
    }

    [SerializeField] private ComputerHazard[] computerHazards;
    [SerializeField] private GameObject normalComputer;

    [SerializeField] private int minRandomHazardTimer;
    [SerializeField] private int maxRandomHazardTimer;
    public float hazardTime;
    [SerializeField] private float hazardTimeOffset;
    [SerializeField] private bool isHazardOn;

    [SerializeField] private ComputerHazard currentComputerHazard;

    [SerializeField] private GameObject dialoguePrompt;


    private void Awake()
    {
        hazardTime = Random.Range(minRandomHazardTimer, maxRandomHazardTimer);
        hazardTime += hazardTimeOffset;
    }

    private void FixedUpdate()
    {
        if (isHazardOn == false)
        {
            HazardUpdate();
        }
    }

    private void HazardUpdate()
    {
        hazardTime -= Time.deltaTime;
        if (hazardTime <= 0f)
        {
            float difficultyMultiplier = 1 - Manager_Sanity.instance.difficultyMultiplier;
            hazardTime = Random.Range(minRandomHazardTimer * difficultyMultiplier, maxRandomHazardTimer * difficultyMultiplier);
            SpawnRandomHazard();
        }
    }

    private void SpawnRandomHazard()
    {
        int index = Random.Range(0, computerHazards.Length);
        currentComputerHazard = computerHazards[index];

        OnHazardInitiated();
    }

    private void OnHazardInitiated()
    {
        Debug.Log("initiated");
        normalComputer.SetActive(false);
        currentComputerHazard.variant.SetActive(true);
        Manager_Sanity.instance.ChangeSanityRate(currentComputerHazard.sanityChange);
        dialoguePrompt.SetActive(true);
        isHazardOn = true;

        IEnumerable<IComputerHandler> computerHandlers = FindObjectsOfType<MonoBehaviour>()
            .OfType<IComputerHandler>();

        foreach (IComputerHandler handler in computerHandlers)
        {
            handler.OnHazardSpawned();
        }
    }

    public void OnInteract()
    {
        Manager_PlayerState.instance.player.GetComponent<Ulmer_MovementVariables>().isMovementStalled = true;
        GameObject popUp = Instantiate(currentComputerHazard.popUp, transform.position, Quaternion.identity);
        StartCoroutine(StandbyCheck(popUp));

    }

    private IEnumerator StandbyCheck(GameObject popUp)
    {
        yield return new WaitForFixedUpdate();
        if (popUp != null)
        {
            StartCoroutine(StandbyCheck(popUp));
        } else
        {
            OnHazardCompleted();
        }
    }

    [ContextMenu("End Hazard")]
    private void OnHazardCompleted()
    {
        Debug.Log("completed");
        isHazardOn = false;

        // Revert sanity change
        normalComputer.SetActive(true);
        currentComputerHazard.variant.SetActive(false);
        Manager_Sanity.instance.ChangeSanityRate(-currentComputerHazard.sanityChange);
        dialoguePrompt.SetActive(false);

        Manager_PlayerState.instance.player.GetComponent<Ulmer_MovementVariables>().isMovementStalled = false;
    }

    public void OnHazardSpawned()
    {
        float difficultyMultiplier = 1 - Manager_Sanity.instance.difficultyMultiplier;
        hazardTime += Random.Range(3f * difficultyMultiplier, 7f * difficultyMultiplier);
    }
}
