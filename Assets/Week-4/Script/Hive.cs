using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour
{
    public float honeyProductionRate = 10f; // Rate of honey production
    public int startingBees = 5; // Starting number of bees
    public GameObject beePrefab; // Reference to the bee prefab

    private int nectarCount = 0;
    private int honeyCount = 0;
    private float honeyTimer = 0f;
    private bool isCountingDown = false;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate bees
        for (int i = 0; i < startingBees; i++)
        {
            GameObject beeObject = Instantiate(beePrefab, transform.position, Quaternion.identity);
            Bee bee = beeObject.GetComponent<Bee>();
            bee.Init(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (nectarCount > 0 && !isCountingDown)
        {
            honeyTimer = honeyProductionRate;
            isCountingDown = true;
        }

        if (isCountingDown)
        {
            honeyTimer -= Time.deltaTime;
            if (honeyTimer <= 0)
            {
                ProduceHoney();
            }
        }
    }

    private void ProduceHoney()
    {
        if (nectarCount > 0)
        {
            nectarCount--;
            honeyCount++;
            honeyTimer = honeyProductionRate;
        }
        else
        {
            isCountingDown = false;
        }
    }

    // Function for bees to give nectar to the hive
    public void GiveNectar()
    {
        nectarCount++;
        if (!isCountingDown)
        {
            honeyTimer = honeyProductionRate;
            isCountingDown = true;
        }
    }
}
