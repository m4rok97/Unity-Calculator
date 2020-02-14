using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Calculator : MonoBehaviour
{
    [SerializeField] private Text inputField;

    private string _previousValue;
    private bool _isShowingResult;
    private string _carryOperation;

    // Start is called before the first frame update
    void Start()
    {
        _previousValue = "0";
        _isShowingResult = true;
        _carryOperation = "";
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ButtonPressed()
    {
        var buttonValue = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        string currentValue = inputField.text;

        var usingIntegerValues = false;
        var previousValueIsInt = false;
        float previousFloatValue;
        int previousIntValue;

        if (int.TryParse(_previousValue, out previousIntValue))
            previousValueIsInt = true;
        previousFloatValue = float.Parse(_previousValue);

        var currentValueIsInt = false;
        float currentFloatValue;
        int currentIntValue;

        if (int.TryParse(currentValue, out currentIntValue))
            currentValueIsInt = true;
        currentFloatValue = float.Parse(currentValue);

        usingIntegerValues = previousValueIsInt && currentValueIsInt;

        if ("/*+-=".Any(x => x == buttonValue[0]))
        {
            string resultText;
            if (!_isShowingResult)
            {
                if (usingIntegerValues && _carryOperation != "/")
                {
                    if (_carryOperation != "")
                    {
                        var intResult = DoIntOperation(_carryOperation, previousIntValue, currentIntValue);
                        resultText = intResult.ToString();
                        _previousValue = resultText;
                        inputField.text = resultText;
                    }
                    else
                    {
                        _previousValue = inputField.text;
                    }
                }
                else
                {
                    if (_carryOperation != "")
                    {
                        var floatResult = DoFloatOperation(_carryOperation, previousFloatValue, currentFloatValue);
                        resultText = floatResult.ToString();
                        _previousValue = resultText;
                        inputField.text = resultText;
                    }
                    else
                    {
                        _previousValue = inputField.text;
                    }
                }
                _isShowingResult = true;
            }

            _carryOperation = buttonValue != "=" ? buttonValue : "";
            
        }
        else if(buttonValue == "C")
        {
            inputField.text = "0";
            _isShowingResult = true;
            _carryOperation = "";
            _previousValue = "0";
        }
        else if(buttonValue == "Log")
        {
            print("Entra al log");
            string resultText;
            if (usingIntegerValues)
            {
                var intResult = DoIntOperation(buttonValue, currentIntValue);
                print("Result es ");
                print(intResult);
                resultText = intResult.ToString();
                _previousValue = resultText;
                inputField.text = resultText;
                _carryOperation = "";
            }
            else
            {
                var floatResult = DoFloatOperation(buttonValue, currentFloatValue);
                resultText = floatResult.ToString();
                _previousValue = resultText;
                inputField.text = resultText;
                _carryOperation = "";
            }

            _isShowingResult = true;
        }
        else if(_isShowingResult)
        {
            inputField.text = buttonValue != "." ? buttonValue : "0.";
            _isShowingResult = false;
        }
        else
        {
            inputField.text += buttonValue;
        }








    }

    private float DoFloatOperation(string operation, float firstValue=0, float secondValue=0)
    {
        switch (operation)
        {
            case "+":
                return firstValue + secondValue;
            case "-":
                return firstValue - secondValue;
            case "*":
                return firstValue * secondValue;
            case "/":
                return firstValue / secondValue;
            case "Log":
                return Mathf.Log(firstValue);
            default:
                return 0;
        }
    }

    private int DoIntOperation(string operation, int firstValue=0, int secondValue=0)
    {
        switch (operation)
        {
            case "+":
                return firstValue + secondValue;
            case "-":
                return firstValue - secondValue;
            case "*":
                return firstValue * secondValue;
            case "/":
                return firstValue / secondValue;
            case "Log":
                return (int)Math.Log(firstValue);
            default:
                return 0;
        }
    }
}