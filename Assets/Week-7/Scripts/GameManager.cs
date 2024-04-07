using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent GameOverEvent;
    private static GameManager instance;
    private bool activated = false;

    private void Awake()
    {
       instance = this;
    }

    [ContextMenu("Do Text GameOverEvent")]
    
    public void reset()
    {
        activated = false;
        gameObject.SetActive(true);
        GameOverEvent.Invoke();
        Debug.Log("Player restarted");
    }
    public static UnityEvent GetGameOverEvent()
    {
        return instance.GameOverEvent;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            reset();
        }
    }
}
