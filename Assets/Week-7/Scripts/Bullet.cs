using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;

    private bool activated = false;

    private void Awake()
    {
        Destroy(gameObject, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (other.transform.name == "Enemy")
        {
            other.GetComponent<EnemyCollision>().Damage();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject) ;
        }
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
        activated = false;
        gameObject.SetActive(true);
    }
}
