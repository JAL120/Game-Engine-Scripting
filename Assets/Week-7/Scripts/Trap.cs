using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Trap : MonoBehaviour
{
    public int damageAmount = 10; // Amount of damage the trap inflicts
    public TMP_Text healthText; // TextMeshPro UI element to display player's health
    public int currentHealth = 100;
    private bool activated = false;
    public GameObject gameoverscreen;
    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player playerHealth = other.gameObject.GetComponent<Player>();
            if (playerHealth != null)
            {
                currentHealth -= damageAmount;
                UpdateHealthUI();
                Debug.Log("I got hurt");
            }
        }
        if (currentHealth <= 0)
        {
            PlayerDeath();
            GameOverScreen();
            gameoverscreen.SetActive(true);
            UpdateHealthUI();
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString();
            Debug.Log($"display player health{currentHealth}");
        }
    }

    private void Start()
    {
        GameManager.GetGameOverEvent().AddListener(reset);
        gameoverscreen.SetActive(false);
        reset();
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to prevent memory leaks
        GameManager.GetGameOverEvent().RemoveListener(reset);
    }

    public void reset()
    {
        activated = false;
        gameObject.SetActive(true);
        GameOverScreen();
        currentHealth = 100;
        UpdateHealthUI();
    }

    public void PlayerDeath()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }

    void GameOverScreen()
    {
        gameoverscreen.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
