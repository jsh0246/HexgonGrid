using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitSkills : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] cooldownText;
    [SerializeField] private Image[] cooldownImage;
    [SerializeField] private float[] cooldown = { 3, 6, 9, 12 };

    [HideInInspector] public bool[] isCooldown = { false, false, false, false };
    private KeyCode[] keyCodes = { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R };
    private float[] cooldownTimer = { 0, 0, 0, 0 };
    private Unit unit;

    public void Awake()
    {
        InitVariables();
    }

    private void InitVariables()
    {
        unit = GetComponent<Unit>();
    }

    public void ChangeUnit()
    {
        for (int i = 0; i < cooldownText.Length; i++)
        {
            this.cooldownImage[i].gameObject.SetActive(isCooldown[i]);
            this.cooldownText[i].gameObject.SetActive(isCooldown[i]);
            this.cooldownText[i].text = cooldownTimer[i].ToString("F0");
            this.cooldownImage[i].fillAmount = cooldownTimer[i] / cooldown[i];
        }
    }

    public void SkillCooldown()
    {
        for (int i = 0; i < cooldownText.Length; i++)
        {
            SkillSetting(i);
            if (isCooldown[i])
            {
                //CD(i);
                StartCoroutine(Cooldown(i));
            }
        }
    }

    public void SkillSetting(int i)
    {
        if (unit.isSelected && Input.GetKeyDown(keyCodes[i]) && !isCooldown[i])
        {
            cooldownText[i].gameObject.SetActive(true);
            cooldownTimer[i] = cooldown[i];
            isCooldown[i] = true;
        }
    }

    public IEnumerator Cooldown(int i)
    {
        yield return null;

        if (cooldownTimer[i] > 0)
        {
            cooldownTimer[i] -= Time.deltaTime;

            if (cooldownTimer[i] < 0)
            {
                cooldownImage[i].fillAmount = cooldownTimer[i] = 0;
                isCooldown[i] = false;
                cooldownText[i].gameObject.SetActive(false);
            }

            cooldownText[i].text = cooldownTimer[i].ToString("F0");
            cooldownImage[i].fillAmount = cooldownTimer[i] / cooldown[i];
        }
    }

    public void CD(int i)
    {
        if (cooldownTimer[i] > 0)
        {
            cooldownTimer[i] -= Time.deltaTime;

            if (cooldownTimer[i] < 0)
            {
                cooldownImage[i].fillAmount = cooldownTimer[i] = 0;
                isCooldown[i] = false;
                cooldownText[i].gameObject.SetActive(false);
            }

            cooldownText[i].text = cooldownTimer[i].ToString("F0");
            cooldownImage[i].fillAmount = cooldownTimer[i] / cooldown[i];
        }
    }

    public void OnCancel(int i)
    {
        this.cooldownText[i].gameObject.SetActive(false);
        this.cooldownImage[i].gameObject.SetActive(false);
        //cooldownText[i].text = "00";
        //cooldownImage[i].fillAmount = 0f;
    }
}
