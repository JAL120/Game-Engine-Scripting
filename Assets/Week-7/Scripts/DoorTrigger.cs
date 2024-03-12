using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] GameObject door;

    Vector3 origin;
    Vector3 target;

    bool isOpening;
    float alpha;
    private bool isUnlocked;

    private void Awake()
    {
       origin = transform.position;
        target = origin + (Vector3.up * 5);
    }

    private void Update()
    {
        alpha += isOpening ? Time.deltaTime : -Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        door.transform.position = Vector3.Lerp(origin, target, alpha);
    }

    private void OnCollison(Collider collison)
    {
        if (isUnlocked)
        {
            isOpening = false;
        }
        //door.gameObject.SetActive(false);        
    }

    private void OnTriggerExit(Collider other)
    {
    ///oor.gameObject.SetActive(true);
    }

    public void Open()
    {
        isUnlocked = true;
    }
}
