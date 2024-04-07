using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] GameObject door;

    // Number of keys collected by the player
    public int keysCollected = 0;
    private bool activated = false;

    Vector3 origin;
    bool isOpening;
    float alpha;
    Vector3 target;

    private void Awake()
    {
       origin = transform.position;
        target = origin + (Vector3.up * 5);
    }

    private void Update()
    {
        alpha += isOpening ? Time.deltaTime : -Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);
        door.transform.position = Vector3.Lerp(origin, target, alpha);
    }

    private void OnTriggerEnter(Collider other)
    {
        //door.gameObject.SetActive(false);
        isOpening = true;

        if (other.CompareTag("Player"))
        {
            // Check if keysCollected is greater than 0
            if (keysCollected > 0)
            {
                // Call the Open() method of the doorController
                
            }
            else
            {
                // If the player doesn't have enough keys, show a message or perform some other action
                Debug.Log("You need more keys to open this door!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //door.gameObject.SetActive(true);
        isOpening = false;
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        GameManager.GetGameOverEvent().AddListener(reset);
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to prevent memory leaks
        GameManager.GetGameOverEvent().RemoveListener(reset);
    }

    void reset()
    {
        activated = false;
        gameObject.SetActive(true);
    }
}
