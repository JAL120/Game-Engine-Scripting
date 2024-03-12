using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyCollision : MonoBehaviour
{
    public Material materialDamaged;
    public Material materialNormal;

    private MeshRenderer mr;

    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        mr.material = materialDamaged;
        DOVirtual.DelayedCall(0.1f, () =>
        {
            mr.material = materialNormal;
        });
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    public void OnTriggerExit(Collider other)
    {
        
    }

    public void Damage()
    {

    }
}
