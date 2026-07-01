using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterUI : MonoBehaviour
{
    [SerializeField] private MonsterStat MonsterStat;
    [SerializeField] private Health health;

    [Header("UI")]
    [SerializeField] private TMP_Text gradeText;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private Slider hpSlider;

    private void Awake()
    {
        if (MonsterStat == null)
            MonsterStat = GetComponentInParent<MonsterStat>();

        if (health == null)
            health = GetComponentInParent<Health>();

        health.OnHealthChanged.AddListener(UpdateHealth);

        UpdateGrade();
        UpdateHealth(
            health.CurrentHP,
            health.MaxHP);
    }

    private void OnDestroy()
    {
        health.OnHealthChanged.RemoveListener(UpdateHealth);
    }

    private void UpdateGrade()
    {
        gradeText.text = MonsterStat.Grade.ToString();
    }

    private void UpdateHealth(int current, int max)
    {
        hpText.text = $"{current}/{max}";
        hpSlider.maxValue = max;
        hpSlider.value = current;
    }
}