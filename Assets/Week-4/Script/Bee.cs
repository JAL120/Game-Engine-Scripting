using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bee : MonoBehaviour
{
    private Hive hive;
    public Flower findflower;
    

    // Start is called before the first frame update
    void Start()
    {
        // Start searching for flowers when the bee is created
        CheckAnyFlower();
        
    }

    // Function to initialize the bee with a hive
    public void Init(Hive hive)
    {
        this.hive = hive;
    }

    // Function to search for flowers and collect nectar
    private void CheckAnyFlower()
    {
        // Find all Flower objects in the scene
        Flower[] flowers = FindObjectsOfType<Flower>();
        

        // If there are flowers, find the nearest one and go to it
        if (flowers.Length > 0)
        {
            Flower nearestFlower = null;
            float shortestDistance = Mathf.Infinity;
            foreach (Flower flower in flowers)
            {
                float distance = Vector2.Distance(transform.position, flower.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestFlower = flower;
                }
            }
            FlyToFlower(nearestFlower);
        }
        
    }

    // Function to fly to a flower and check nectar
    private void FlyToFlower(Flower flower)
    {
        // Fly to the flower
        transform.DOMove(flower.transform.position, 1f).OnComplete(() =>
        {
            // Check if the flower has nectar
            if (flower.TakeNectar())
            {
                // If flower has nectar, fly back to the hive and give nectar
                FlyToHiveAndGiveNectar();
            }
            else
            {
                // If flower has no nectar, continue searching for other flowers
                CheckAnyFlower();
            }
            FlyToHiveAndGiveNectar();
        }).SetEase(Ease.Linear);
    }

    // Function to fly to the hive and give nectar
    private void FlyToHiveAndGiveNectar()
    {
        // Fly to the hive
        Debug.Log(hive.transform.position);
        transform.DOMove(hive.transform.position, 1f).OnComplete(() =>
        {
            // Give nectar to the hive
            hive.GiveNectar();

            // After delivering nectar to hive, continue searching for other flowers
            CheckAnyFlower();
        }).SetEase(Ease.Linear);
    }
}
