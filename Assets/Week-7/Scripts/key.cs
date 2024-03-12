using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class key : MonoBehaviour
{
    public GameObject door; // Reference to the door GameObject
    public TMP_Text keytext;

    private int keysCollected = 0; // Number of keys collected by the player

    void OnCollisionEnter(Collision collision)
    {
        TryOpenDoor();
        if (collision.gameObject.CompareTag("Player"))
        {
            
            DoorTrigger door = FindObjectOfType<DoorTrigger>(); // Find the Door script in the scene
            if (door != null)
            {
                
                door.Open(); // Call the Unlock method in the Door script
                Destroy(gameObject); // Destroy the key object after the player collects it
            }
            keysCollected++;
            UpdateKeyUI();
        }
    }

    void UpdateKeyUI()
    {
        // Update UI to display the number of keys collected
        keytext.text = "Keys: " + keysCollected;
    }

    public void TryOpenDoor()
    {
        // Check if the player has a key and the door exists
        if (keysCollected > 0 && door != null)
        {
            // Open the door if the player has a key
            Destroy(door);
            keysCollected--; // Deduct the used key from the player's inventory
            UpdateKeyUI();
        }
    }
}
