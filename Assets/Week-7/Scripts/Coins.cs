using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

public class Coins : MonoBehaviour
{
    private int coinsCollected = 0; // Number of coins collected by the player
    public TMP_Text coinsText; // TextMeshPro UI element to display player's coins
    public int totalCoins = 8;
    GameManager gameManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Collect the coin and increase the player's coin count
            gameObject.SetActive(false); // Destroy the coin GameObject
            coinsCollected++;
            UpdateCoinsUI();

            if (coinsCollected  >= totalCoins)
            {
                gameManager.reset();
            }
        }
    }

    public void UpdateCoinsUI()
    {
        // Update UI to display the number of coins collected
        coinsText.text = "Coins: " + coinsCollected++;
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
        gameObject.SetActive(true);
        coinsCollected = 0;
        UpdateCoinsUI();
    }
}
