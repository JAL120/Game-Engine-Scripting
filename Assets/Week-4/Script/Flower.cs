using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public float productionRate = 5f; // Rate of nectar production
    public Color readyColor = Color.yellow; // Color when nectar is ready
    public Color notReadyColor = Color.white; // Color when nectar is not ready

    private bool hasNectar = false;
    private float productionTimer = 0f;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateFlowerColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasNectar)
        {
            // Count down to produce nectar
            productionTimer += Time.deltaTime;
            if (productionTimer >= productionRate)
            {
                ProduceNectar();
            }
        }
    }

    private void UpdateFlowerColor()
    {
        spriteRenderer.color = hasNectar ? readyColor : notReadyColor;
    }

    private void ProduceNectar()
    {
        hasNectar = true;
        productionTimer = 0f;
        UpdateFlowerColor();
    }

    // Function to communicate to Bees if nectar is available
    public bool HasNectar()
    {
        return hasNectar;
    }

    // Function to allow Bees to "take" nectar
    public bool TakeNectar()
    {
        if (hasNectar)
        {
            hasNectar = false;
            UpdateFlowerColor();
            return true;
        }
        else
        {
            return false;
        }
    }
}
