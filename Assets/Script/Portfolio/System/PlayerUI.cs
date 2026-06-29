using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Level level;

    [Header("UI")]
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Slider expSlider;

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
}