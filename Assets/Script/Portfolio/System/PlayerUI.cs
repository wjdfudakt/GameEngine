using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private PlayerStat level;

    [Header("UI")]
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private Slider expSlider;

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
}