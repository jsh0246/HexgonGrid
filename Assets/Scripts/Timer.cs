using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Image percentImage;
    [SerializeField] private TextMeshProUGUI secText;

    private int duration;
    private int remainingDuration;


    private void Start()
    {
        Being(60);
    }

    private  void Being(int second)
    {
        duration = second;
        remainingDuration = second;
        //StartCoroutine(Tiktok());
    }

    private IEnumerator Tiktok()
    {
        while(remainingDuration > 0)
        {
            secText.text = $"{remainingDuration / 60:00}:{remainingDuration % 60:00}";
            percentImage.fillAmount = Mathf.InverseLerp(0, duration, remainingDuration);
            remainingDuration--;
            yield return new WaitForSeconds(1f);
        }

        OnEnd();
    }

    private void OnEnd()
    {
        print("TImer ended");
    }
}