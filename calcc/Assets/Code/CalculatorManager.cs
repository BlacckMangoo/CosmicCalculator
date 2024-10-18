using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CalculatorManager : MonoBehaviour
{
    int NumberOfNumericals;

    public string input1;
    public string input2;
    public TextMeshProUGUI radOrDegText;
    public TextMeshProUGUI inputText;
    public TextMeshProUGUI outputText;
    [SerializeField] Button[] buttonNumberArray;
    char currentOperatorChar;

    public bool isTypingSecondNumber;

    [SerializeField] AudioClip[] clickSound;
    [SerializeField] AudioClip[] errorClick;


    public Button addButton;
    public Button subtractButton;
    public Button multiplyButton;
    public Button divideButton;
    public Button clearButton;
    public Button sin;
    public Button cos;
    public Button tan;
    public Button percentButton;
    [SerializeField] Button equalsTo;

    public bool hastypedDecimalPoint = false;
    angleMode currentangleMode; 

    private Func<float, float, float> currentOperator;

    void Start()
    {
        
        currentangleMode = angleMode.radian;
        input1 = " ";
        input2 = " ";
        isTypingSecondNumber = false;
        HandleNumberText();
        addButton.onClick.AddListener(() => OnButtonClick(addButton));
        subtractButton.onClick.AddListener(() => OnButtonClick(subtractButton));
        multiplyButton.onClick.AddListener(() => OnButtonClick(multiplyButton));
        divideButton.onClick.AddListener(() => OnButtonClick(divideButton));
        equalsTo.onClick.AddListener(() => OnButtonClick(equalsTo));
        percentButton.onClick.AddListener(() =>OnButtonClick(percentButton));
    }
    private void Update()
    {
        if (isTypingSecondNumber)
        {
            inputText.text = input1 + " " + currentOperatorChar
                + input2;
        }
        else
        {
            inputText.text = input1;
        }
        UpdateangleModeText();
       // Debug.Log(currentangleMode);




    }
    enum angleMode
        {
          radian ,
          degrees
        }

void HandleNumberText()
    {

        inputText.text = "";
        NumberOfNumericals = buttonNumberArray.Length;

        for (int i = 0; i < NumberOfNumericals; i++)
        {
            TextMeshProUGUI tmpText = buttonNumberArray[i].GetComponentInChildren<TextMeshProUGUI>();
            if (tmpText != null)
            {
                tmpText.text = i.ToString();
            }
        }

    }

    //OnClickingOperator
    void OnButtonClick(Button op)
    {
        if( !(input1 == " "))
        {
            isTypingSecondNumber = true;



            Debug.Log(op.gameObject);
            if (op == addButton)
            {
                inputText.text = input1;
                StoreOperator('+');
            }
            else if (op == subtractButton)
            {
                inputText.text = input1;
                StoreOperator('-');
            }
            else if ( op == percentButton)
            {
                inputText.text = input1;
                StoreOperator('%'); // finds input2 percent of input1
            }
            else if (op == multiplyButton)
            {
                inputText.text = input1;
                StoreOperator('*');
            }
            else if (op == divideButton)
            {
                inputText.text = input1;
                StoreOperator('/');
            }
           
            else if (op == equalsTo)
            {
                if (input1 == " " || input2 == " ")
                {
                    SoundManager.instance.PlayRandomSoundFX(1, errorClick, transform);

                    return;
                }

                float a = float.Parse(input1);
                float b = float.Parse(input2);
                float result = PerformOperation(a, b);
                outputText.text = result.ToString();
                input1 = result.ToString();

                input2 = " ";
                isTypingSecondNumber = false;
            }
            

        }
        else
        {
            SoundManager.instance.PlayRandomSoundFX(1, errorClick, transform);
            
        }

    }
    void StoreOperator(char operation)
    {
        currentOperatorChar = operation;
        switch (operation)
        {
            case '+':
                currentOperator = (a, b) => a + b;
                break;
            case '-':
                currentOperator = (a, b) => a - b;
                break;
            case '*':
                currentOperator = (a, b) => a * b;
                break;
            case '/':
                currentOperator = (a, b) => a / b;
                break;
            case '%':
                currentOperator = (a, b) => a * b / 100;
                break;
            case ' ':
                break;
            default:         
                break;
        }
    }

    float PerformOperation(float a, float b)
    {
        SoundManager.instance.PlayRandomSoundFX(1, clickSound, transform);
        return currentOperator(a, b);

    }

    public void ClearEverything()
    {
        SoundManager.instance.PlayRandomSoundFX(1, clickSound, transform);
        Debug.Log("clicked");
        input1 = " ";
        input2 = " ";
        outputText.text = " ";
        StoreOperator(' ');
        isTypingSecondNumber = false;
        input1 = outputText.text;
       hastypedDecimalPoint = false;
    }
    // I am calling these trignometric functions in the unity editor ( button Onclick())
 
    public void Sin()
    {


        if ((input1 == " " && !isTypingSecondNumber) || input2 == " " && isTypingSecondNumber)
        {
            SoundManager.instance.PlayRandomSoundFX(1, errorClick, transform);
            Debug.Log("Input1 is null");
            return;
        }
        SoundManager.instance.PlayRandomSoundFX(1, clickSound, transform);
        float angle = float.Parse(inputText.text);
        if (currentangleMode == angleMode.degrees)
        {
            outputText.text = Mathf.Sin(angle * (MathF.PI / 180)).ToString();
        }
        else if (currentangleMode == angleMode.radian)
        {
            outputText.text = Mathf.Sin(angle ).ToString();
        }
        input1 = outputText.text;

    }

    public void Cos()
    {
        if ((input1 == " " && !isTypingSecondNumber) || input2 == " " && isTypingSecondNumber)
        {
            SoundManager.instance.PlayRandomSoundFX(1, errorClick, transform);
            Debug.Log("Input1 is null");
            return;
        }
        SoundManager.instance.PlayRandomSoundFX(1, clickSound, transform);
        float angle = float.Parse(inputText.text);
        if (currentangleMode == angleMode.degrees)
        {
            outputText.text = Mathf.Cos(angle * ( MathF.PI/180)).ToString();
        }
        else if (currentangleMode == angleMode.radian)
        {
            outputText.text = Mathf.Cos(angle ).ToString();
        }
        input1 = outputText.text;
    }

    public void Tan()
    {
        if ((input1 == " " && !isTypingSecondNumber) || input2 == " " && isTypingSecondNumber)
        {
            SoundManager.instance.PlayRandomSoundFX(1, errorClick, transform);
            Debug.Log("Input1 is null");
            return;
        }
        SoundManager.instance.PlayRandomSoundFX(1, clickSound, transform);
        float angle = float.Parse(inputText.text);
        if (currentangleMode == angleMode.degrees)
        {
            outputText.text = Mathf.Tan(angle * (MathF.PI / 180)).ToString();
        }
        else if ( currentangleMode == angleMode.radian)
        {
            outputText.text = Mathf.Tan(angle).ToString();
        }
        input1 = outputText.text;
    }

    public void Log()
    {
        if ((input1 == " " && !isTypingSecondNumber) || input2 == " " && isTypingSecondNumber)
        {
            SoundManager.instance.PlayRandomSoundFX(1, errorClick, transform);
            Debug.Log("Input1 is null");
            return;
        }
        SoundManager.instance.PlayRandomSoundFX(1, clickSound, transform);
        float angle = float.Parse(inputText.text);
        float angleDeg = float.Parse(inputText.text);
        outputText.text = Mathf.Log10(angle).ToString() ;
        input1 = outputText.text;
    }

    public void DecimalPoint()
    {
        if(hastypedDecimalPoint == false)
        {
            SoundManager.instance.PlayRandomSoundFX(1, clickSound, transform);
            if (isTypingSecondNumber)
            {
                input2 += ".";
                hastypedDecimalPoint = true;
            }
            else
            {
                input1 += ".";
                hastypedDecimalPoint = true;
            }
        }
        else
        {
            SoundManager.instance.PlayRandomSoundFX(1, errorClick, transform);
        }

       
       

    }

    public void ChangeangleUnit()
    {
           if(currentangleMode == angleMode.radian)
            {
            currentangleMode = angleMode.degrees;
            }
           else if( currentangleMode == angleMode.degrees)
            {
                currentangleMode = angleMode.radian;
            }
        
    }

    public void UpdateangleModeText()
    {
        if (currentangleMode == angleMode.radian)
        {
            radOrDegText.text = " rad";
        }


        if (currentangleMode == angleMode.degrees)
        {
            radOrDegText.text = "deg";
        }
    }

   
    
    public void TypeE()
    {
        if (isTypingSecondNumber && input2 == " ")
        {
            input2 +=  MathF.E.ToString();
        }
        else if(!isTypingSecondNumber && input1 == " ") 
        {
            input1 += MathF.E.ToString();
        }
        else
        {
            SoundManager.instance.PlayRandomSoundFX(1, errorClick, transform);
        }
    }

    public void TypePi()
    {
        if (isTypingSecondNumber && input2 == " ")
        {
            input2 += MathF.PI.ToString();
        }
        else if(!isTypingSecondNumber && input1 == " ")
        {
            input1 += MathF.PI.ToString();
        }
        else
        {
            SoundManager.instance.PlayRandomSoundFX(1, errorClick, transform);
        }
    }

}
