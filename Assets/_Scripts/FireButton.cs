using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class FireButton : MonoBehaviour
{
    [SerializeField] private bool isBug;
    [SerializeField] private AudioClip sfx_fireOut;
    [SerializeField] private AudioClip sfx_bugOut;


    private void Awake()
    {
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(Random.Range(-300, 300), Random.Range(-150, 120), gameObject.GetComponent<RectTransform>().transform.position.z);
    }

    public void OnFireClick()
    {
        RectTransform rect = gameObject.GetComponent<RectTransform>();
        rect.localScale = new Vector3(rect.localScale.x - 0.4f, rect.localScale.y - 0.4f, rect.localScale.z);

        if (!isBug)
        {
            Manager_SFXPlayer.instance.PlaySFXClip(sfx_fireOut, transform, 1f, false);
        } else
        {
            Manager_SFXPlayer.instance.PlaySFXClip(sfx_bugOut, transform, 1f, false);
        }

        if (rect.localScale.x <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
