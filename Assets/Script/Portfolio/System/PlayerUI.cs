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
        hpText.text = $"HP : {maxHp} / {hp}";
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
        UpdateSkillText(skill1Text, playerSkill.Skill1Remain);
        UpdateSkillText(skill2Text, playerSkill.Skill2Remain);
        UpdateSkillText(skill3Text, playerSkill.Skill3Remain);
        UpdateSkillText(skill4Text, playerSkill.Skill4Remain);
        UpdateSkillText(skill5Text, playerSkill.Skill5Remain);
        UpdateSkillText(skill6Text, playerSkill.Skill6Remain);
    }

    private void UpdateSkillText(TMP_Text text, float remain)
    {
        if (remain <= 0)
            text.text = "Ready";
        else
            text.text = remain.ToString("0.0");
    }
}