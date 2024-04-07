using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Success : MonoBehaviour
{
    Trap trap;
    GameManager gameManager;
    private bool activated = false;
    public GameObject gameoverscreen;
    Player player;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            trap.reset();
            Debug.Log("You Won!!!");
        }
    }

    private void Start()
    {
        GameManager.GetGameOverEvent().AddListener(reset);
        gameoverscreen.SetActive(false);
        reset();
    }

    public void reset()
    {
        activated = false;
        gameObject.SetActive(true);
        GameOverScreen();
    }

    private void OnDestroy()
    {
        GameManager.GetGameOverEvent().RemoveListener(reset);
    }

    void GameOverScreen()
    {
        gameoverscreen.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
