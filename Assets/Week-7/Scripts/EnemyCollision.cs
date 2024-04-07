using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyCollision : MonoBehaviour
{
    public Material materialDamaged;
    public Material materialNormal;
    private bool activated = false;

    private MeshRenderer mr;

    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            mr.material = materialDamaged;

            DOVirtual.DelayedCall(0.1f, () =>
            {
                mr.material = materialNormal;
            });
            if (other.gameObject.CompareTag("Bullet"))
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);
        }
    }

    public void Damage()
    {

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
