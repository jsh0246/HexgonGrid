using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class PI : MonoBehaviour
{
    private string pi;
    private int curDecimalPoint;

    [SerializeField] private Image frontNumber;
    [SerializeField] private Sprite[] numbers;
    [SerializeField] private TMP_InputField input;
    [SerializeField] private TMP_InputField teleportNumInput;

    private void Start()
    {
        InitVariables();
    }

    private void Update()
    {
        
    }

    public void HittheNumber()
    {
        int hitNum;

        if(input.text.Length > 0)
        {
            hitNum = int.Parse(input.text);

            if(hitNum == pi[curDecimalPoint]-'0')
            {

            }
        }
    }

    public void Next()
    {
        if (curDecimalPoint + 1 >= pi.Length)
        {
            print("final decimal point");
            return;
        }

        frontNumber.sprite = numbers[pi[++curDecimalPoint]-'0'];
    }

    public void Prev()
    {
        if (curDecimalPoint - 1 < 0)
        {
            print("this is first decimal point");
            return;
        }

        frontNumber.sprite = numbers[pi[--curDecimalPoint] - '0'];
    }

    public void Teleport()
    {
        if (teleportNumInput.text.Length > 0 )
        {
            curDecimalPoint = int.Parse(teleportNumInput.text);

            if (curDecimalPoint >= 1 && curDecimalPoint <= 200)
            {
                frontNumber.sprite = numbers[pi[curDecimalPoint-1] - '0'];
                --curDecimalPoint;
            }
        }
    }

    private void InitVariables()
    {
        pi = "14159265358979323846264338327950288419716939937510582097494459230781640628620899862803482534211706798214808651328230664709384460955058223172535940812848111745028410270193852110555964462294895493038196";
        curDecimalPoint = 0;
        frontNumber.sprite = numbers[pi[curDecimalPoint]-'0'];
    }
}