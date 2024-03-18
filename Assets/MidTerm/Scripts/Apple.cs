using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public GameObject PineapplePrefab; // Prefab for the food
    public Transform SnakePrefab;
    private List<Transform> _tail;
    public BoxCollider2D GridSpace;

    private AudioSource audio;

    private void Start()
    {
        RandomizePosition();
    }

    public void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RandomizePosition();
            audio.Play();
        }
    }

    void GrowSnake()
    {
        Transform tail = Instantiate(this.SnakePrefab);
        tail.position = _tail[_tail.Count - 1].position;
        _tail.Add(tail);
    }

    //Randomizes the pinepple's position everytime the player touches it
    private void RandomizePosition()
    {
        Bounds bounds = this.GridSpace.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }
    
}
