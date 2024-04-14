using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public TMP_Text healthText; // TextMeshPro UI element to display player's health
    public int currentHealth;
    public GameObject gameoverscreen;

    public void PlayerDeath()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;

        }
    }

    void GameOverScreen()
    {
        gameoverscreen.SetActive(true);
        Time.timeScale = 1.0f;
    }
}
