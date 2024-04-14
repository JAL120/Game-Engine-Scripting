using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boarder : MonoBehaviour
{
    void OnCollisionEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RestartGame();
        }
    }

    void RestartGame()
    {
        // Restart the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
