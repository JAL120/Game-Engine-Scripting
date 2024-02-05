using UnityEngine;
using TMPro;

public class Calculator : MonoBehaviour
{
    
    public TextMeshProUGUI text;
    private float prevInput;
    private bool clearPrevInput;
    private EquationType equationType;

    private void Start()
    {
        Clear();
    }

    public void AddInput(string input)
    {
        if (clearPrevInput)
        {
            text.text = string.Empty;
            clearPrevInput = false;
        }

        text.text = input;
    }

    public void SetEquationAsAdd()
    {
        prevInput = float.Parse(text.text);
        clearPrevInput = true;
        equationType = EquationType.ADD;
    }

    public void SetEquationAsSubtract()
    {
        prevInput = float.Parse(text.text);
        clearPrevInput = true;
        equationType = EquationType.SUBTRACT;
    }

    public void SetEquationAsMultiply()
    {
        prevInput = float.Parse(text.text);
        clearPrevInput = true;
        equationType = EquationType.MULTIPLY;
    }
    
    public void SetEquationAsDivide()
    {
        prevInput = float.Parse(text.text);
        clearPrevInput = true;
        equationType = EquationType.DIVIDE;
    }

    public void Add()
    {
        float currentInput = float.Parse(text.text);
        float result = prevInput + currentInput;
        text.text = result.ToString();
    }

    public void Subtract()
    {
        float currentInput = float.Parse(text.text);
        float result = prevInput - currentInput;
        text.text = result.ToString();
    }

    public void Multiply()
    {
        float currentInput = float.Parse(text.text);
        float result = prevInput * currentInput;
        text.text = result.ToString();
    }

    //TODO: Implement Divide function
    public void Divide()
    {
        float currentInput = float.Parse(text.text);
        float result = prevInput / currentInput;
        text.text = result.ToString();
    }

    public void Clear()
    {
       
        text.text = "0";
        clearPrevInput = true;
        prevInput = 0f;
        equationType = EquationType.None;        
    }

    public void Calculate()
    {
        if (equationType == EquationType.ADD)
            Add();
        if (equationType == EquationType.SUBTRACT)
            Subtract();
        if (equationType == EquationType.MULTIPLY)
            Multiply();
        if (equationType == EquationType.DIVIDE)
            Divide();
    }

    public enum EquationType
    {
        None = 0,
        ADD = 1,
        SUBTRACT = 2,
        MULTIPLY = 3,
        DIVIDE = 4
    }
}
