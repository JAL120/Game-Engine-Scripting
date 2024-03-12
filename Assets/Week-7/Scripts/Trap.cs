using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Trap : MonoBehaviour
{
    public int damageAmount = 50; // Amount of damage the trap inflicts
    public TMP_Text healthText; // TextMeshPro UI element to display player's health
    private int currentHealth;

    void OnCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player playerHealth = collision.gameObject.GetComponent<Player>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                
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
    }
}
