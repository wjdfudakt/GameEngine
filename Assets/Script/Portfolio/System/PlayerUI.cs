using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private PlayerStat level;
    [SerializeField] private PlayerSkill playerSkill;

    [Header("UI")]
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private Slider expSlider;

    [Header("Skill UI")]
    [SerializeField] private TMP_Text skill1Text;
    [SerializeField] private TMP_Text skill2Text;
    [SerializeField] private TMP_Text skill3Text;
    [SerializeField] private TMP_Text skill4Text;
    [SerializeField] private TMP_Text skill5Text;
    [SerializeField] private TMP_Text skill6Text;

    [Header("Skill Mask")]
    [SerializeField] private Image skill1Mask;
    [SerializeField] private Image skill2Mask;
    [SerializeField] private Image skill3Mask;
    [SerializeField] private Image skill4Mask;
    [SerializeField] private Image skill5Mask;
    [SerializeField] private Image skill6Mask;

    private float surviveTime;


    private void Awake()
    {
        health.OnHealthChanged.AddListener(UpdateHealthUI);
        level.OnExperienceChanged.AddListener(UpdateLevelUI);

        UpdateHealthUI(health.CurrentHP, health.MaxHP);
        UpdateLevelUI(
            level.CurrentExp,
            level.RequiredExp,
            level.CurrentLevel);
    }

    private void Update()
    {
        surviveTime += Time.deltaTime;
        UpdateTimeUI();
        UpdateSkillUI();
    }

    private void UpdateHealthUI(int hp, int maxHp)
    {
        hpText.text = $"HP : {hp} / {maxHp}";
    }

    private void UpdateLevelUI(int exp, int maxExp, int lv)
    {
        levelText.text = $"Lv. {lv}";

        expSlider.maxValue = maxExp;
        expSlider.value = exp;
    }

    private void UpdateTimeUI()
    {
        int minutes = Mathf.FloorToInt(surviveTime / 60f);
        int seconds = Mathf.FloorToInt(surviveTime % 60f);

        timeText.text = $"{minutes:00}:{seconds:00}";
    }

    private void UpdateSkillUI()
    {
        UpdateSkill(
            skill1Text,
            skill1Mask,
            playerSkill.ClassSkill.Skill1Remain,
            playerSkill.ClassSkill.Skill1Cooldown);

        UpdateSkill(
            skill2Text,
            skill2Mask,
            playerSkill.ClassSkill.Skill2Remain,
            playerSkill.ClassSkill.Skill2Cooldown);

        UpdateSkill(
            skill3Text,
            skill3Mask,
            playerSkill.ClassSkill.Skill3Remain,
            playerSkill.ClassSkill.Skill3Cooldown);

        UpdateSkill(
            skill4Text,
            skill4Mask,
            playerSkill.ClassSkill.Skill4Remain,
            playerSkill.ClassSkill.Skill4Cooldown);

        UpdateSkill(
            skill5Text,
            skill5Mask,
            playerSkill.ClassSkill.Skill5Remain,
            playerSkill.ClassSkill.Skill5Cooldown);

        UpdateUltimate();
    }

    private void UpdateSkill(
    TMP_Text text,
    Image mask,
    float remain,
    float cooldown)
    {
        if (remain <= 0f)
        {
            text.text = "";
            mask.fillAmount = 0f;
        }
        else
        {
            text.text = Mathf.Ceil(remain).ToString();

            mask.fillAmount = remain / cooldown;
        }
    }

    private void UpdateUltimate()
    {
        skill6Mask.fillAmount =
            1f - playerSkill.UltimateGaugePercent;

        if (playerSkill.UltimateGauge >= 100)
        {
            skill6Text.text = "READY";
        }
        else
        {
            skill6Text.text =
                $"{Mathf.RoundToInt(playerSkill.UltimateGauge)}%";
        }
    }
}