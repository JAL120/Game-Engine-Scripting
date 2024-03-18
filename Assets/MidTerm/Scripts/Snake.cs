using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;
using TMPro;
using Input = UnityEngine.Input;


public class Snake : MonoBehaviour
{
    public Transform SnakePrefab;
    
    private List<Transform> _tail = new List<Transform>();
    public Vector2 _direction = Vector2.right;
    private Vector2 input;
    public float rotationSpeed;
    public int initialSize = 3;

    public TMP_Text scoreText;
    private int Combo = 0;

    private AudioSource audio2;

    // Start is called before the first frame update
    private void Start()
    {
        audio2.Play();
        Reset();
        UpdateScoreUI();
    }
    public void Awake()
    {
        audio2 = GetComponent<AudioSource>();
    }

    //movement code and key assignments
    private void Update()
    {
        //2D snake movement credits: Game Dev Stack Exchange
        if (_direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                input = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                input = Vector2.down;
            }
        }

        if (_direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                input = Vector2.right;
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                input = Vector2.left;
            }
        }

    }

    void FixedUpdate()
    {
        //Credits: Game Dev Stack Exchange
        for (int i = _tail.Count - 1; i > 0; i--)
        {
            _tail[i].position = _tail[i - 1].position;
        }

        if (input != Vector2.zero)
        {
            _direction = input;
        }

        float x = Mathf.Round(transform.position.x) + _direction.x;
        float y = Mathf.Round(transform.position.y) + _direction.y;

        transform.position = new Vector3(x, y);

        if (_direction != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, _direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pineapple"))
        {
            GrowSnake();
            Combo++;
            UpdateScoreUI();
        }
        else if (other.gameObject.CompareTag("Boardr"))
        {
            Reset();
        }
    }

    void GrowSnake()
    {
        Transform tail = Instantiate(SnakePrefab);
        tail.position = _tail[_tail.Count -1].position;
        _tail.Add(tail);
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Combo: " + Combo.ToString();
    }

    public void Reset()
    {
        _direction = Vector2.right;
        transform.position = Vector3.zero;

        Combo = 0;
        UpdateScoreUI();

        for (int i = 1; i < _tail.Count; i++)
        {
            Destroy(_tail[i].gameObject);
        }

        _tail.Clear();
        _tail.Add(transform);

        for (int i = 0; i < initialSize - 1; i++)
        {
            GrowSnake();
        }
    }
}
