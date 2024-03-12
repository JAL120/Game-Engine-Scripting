using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Coins : MonoBehaviour
{
    public int coinsCollected = 0; // Number of coins collected by the player
    public TMP_Text coinsText; // TextMeshPro UI element to display player's coins

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Collect the coin and increase the player's coin count
            Destroy(gameObject); // Destroy the coin GameObject
            coinsCollected++;
            UpdateCoinsUI();
        }
    }

    void UpdateCoinsUI()
    {
        // Update UI to display the number of coins collected
        coinsText.text = "Coins: " + coinsCollected;
    }
}
