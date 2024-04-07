using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class key : MonoBehaviour
{
    public GameObject door; // Reference to the door GameObject
    public TMP_Text keytext;
    private bool activated = false;

    private int keysCollected = 0; // Number of keys collected by the player

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            keysCollected++;
            UpdateKeyUI();
            gameObject.SetActive(false);
        }
    }

    public void Open(DoorTrigger doorTrigger)
    {
        if (keysCollected > 0)//unlocking the door
        {
            doorTrigger.Destroy();
            keysCollected--;
            UpdateKeyUI();
            Debug.Log("I unlocked the door");
        }
    }
   
    void UpdateKeyUI()
    {
        // Update UI to display the number of keys collected
        keytext.text = "Keys:"  + keysCollected;
    }


    private void Start()
    {
        GameManager.GetGameOverEvent().AddListener(reset);
    }

    private void OnDestroy()
    {
        GameManager.GetGameOverEvent().RemoveListener(reset);
    }

    void reset()
    {
        activated = false;
        gameObject.SetActive(true);
        keysCollected = 0;
        UpdateKeyUI();
    }
}
