using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour
{
    Button yourButton;
     CalculatorManager calculatorManager;
    TextMeshProUGUI numberText;
  
    [SerializeField] AudioClip[] clickSound;
    void Start()
    {
        calculatorManager = FindObjectOfType<CalculatorManager>();
        numberText = GetComponentInChildren<TextMeshProUGUI>();
         yourButton = GetComponent<Button>();
        yourButton.onClick.AddListener(TaskOnClick);
        
       
    }

    void TaskOnClick()
    {
        SoundManager.instance.PlayRandomSoundFX(1, clickSound, transform);
        if (calculatorManager.isTypingSecondNumber)
        {
            calculatorManager.input2 += numberText.text;
        }
       else
        {
            calculatorManager.input1 += numberText.text;
        }
           
       


    }

    




}
