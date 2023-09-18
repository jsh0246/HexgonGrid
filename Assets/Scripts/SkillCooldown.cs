using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.EditorUtilities;

public class SkillCooldown : MonoBehaviour
{
    [SerializeField] private Image imageCooldown;
    [SerializeField] private TMP_Text textCooldown;

    private bool isCooldown = false;
    private float cooldown = 6f;
    private float cooldownTimer = 0f;

    private void Start()
    {
        textCooldown.gameObject.SetActive(false);
        imageCooldown.fillAmount = 0f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            UseSkill();
        }

        if (isCooldown)
        {
            Cooldown();
        }
    }

    private void Cooldown()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer < 0f)
        {
            isCooldown = false;
            textCooldown.gameObject.SetActive(false);
            imageCooldown.fillAmount = 0f;
        } else
        {
            textCooldown.text = cooldownTimer.ToString("F1");
            imageCooldown.fillAmount = cooldownTimer / cooldown;
        }
    }

    public void UseSkill()
    {
        if(!isCooldown)
        {
            isCooldown = true;
            textCooldown.gameObject.SetActive(true);
            cooldownTimer = cooldown;
        }
    }
}