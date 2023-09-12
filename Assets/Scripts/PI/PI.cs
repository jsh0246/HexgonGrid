using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PI : MonoBehaviour
{
    private string pi;
    private int curDecimalPoint;

    [SerializeField] private Image frontNumber;
    [SerializeField] private TextMeshProUGUI input;
    [SerializeField] private Sprite[] numbers;

    private void Start()
    {
        InitVariables();
    }

    private void Update()
    {
        
    }

    public void HittheNumber()
    {
        print(input.text);
    }

    public void Next()
    {
        //curDecimalPoint = (curDecimalPoint + 1) % 10;
        //frontNumber.sprite = numbers[curDecimalPoint];
        //print(curDecimalPoint);

        if (curDecimalPoint + 1 >= pi.Length)
        {
            print("final decimal point");
            return;
        }

        frontNumber.sprite = numbers[pi[++curDecimalPoint]-'0'];

        print(pi[curDecimalPoint]-'0');
        print("DECIMAL : " + curDecimalPoint);
    }

    public void Prev()
    {
        if (curDecimalPoint - 1 < 0)
        {
            print("this is first decimal point");
            return;
        }



        frontNumber.sprite = numbers[pi[--curDecimalPoint] - '0'];
        //curDecimalPoint = (curDecimalPoint - 1) % 10;

        //if (curDecimalPoint < 0)
        //    curDecimalPoint = 10 + curDecimalPoint;

        //frontNumber.sprite = numbers[curDecimalPoint];
        //print(curDecimalPoint);
    }

    public void Teleport()
    {
        print(-1 % 10);
    }

    private void InitVariables()
    {
        pi = "14159265358979323846264338327950288419716939937510582097494459230781640628620899862803482534211706798214808651328230664709384460955058223172535940812848111745028410270193852110555964462294895493038196";
        curDecimalPoint = 195;
    }
}