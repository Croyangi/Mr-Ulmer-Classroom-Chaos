using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp_Fire : MonoBehaviour
{
    [SerializeField] private GameObject[] fires;
    [SerializeField] private int totalFires;

    private void Awake()
    {
        totalFires = fires.Length;
        LeanTween.scale(gameObject, Vector3.zero, 0f);
        LeanTween.scale(gameObject, Vector3.one, 0.1f);
    }

    private void FixedUpdate()
    {
        int temp = 0;
        for (int i = 0; i < totalFires; i++) 
        { 
            if (fires[i] == null)
            {
                temp++;
            }
        }

        if (temp >= totalFires)
        {
            Debug.Log("nice");
            Destroy(gameObject);
        }
    }
}
