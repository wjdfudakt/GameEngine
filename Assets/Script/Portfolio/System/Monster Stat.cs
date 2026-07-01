using UnityEngine;
using UnityEngine.Events;
public enum MonsterGrade
{
    Normal,
    Elite,
    Boss
}

public class MonsterStat : MonoBehaviour
{
    public UnityEvent<int> OnLevelUp;

    [Header("Grade")]
    [SerializeField] private MonsterGrade grade = MonsterGrade.Normal;

    [Header("Grade Multiplier")]
    [SerializeField] private float normalAttackMultiplier = 1f;
    [SerializeField] private float normalHpMultiplier = 1f;

    [SerializeField] private float eliteAttackMultiplier = 1.5f;
    [SerializeField] private float eliteHpMultiplier = 2f;

    [SerializeField] private float bossAttackMultiplier = 3f;
    [SerializeField] private float bossHpMultiplier = 5f;

    [Header("Level")]
    [SerializeField] private int level = 1;

    [Header("Stats")]
    [SerializeField] private int attackPower = 5;
    [SerializeField] private int attackIncreasePerLevel = 1;
    [SerializeField] private int hpIncreasePerLevel = 5;

    [Header("Auto Level Up")]
    [SerializeField] private float levelUpInterval = 60f;

    public int CurrentLevel => level;
    public int AttackPower => attackPower;
    public MonsterGrade Grade => grade;

    private float timer;
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();

        ApplyGradeMultiplier();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= levelUpInterval)
        {
            timer = 0f;
            LevelUp();
        }
    }

    private void ApplyGradeMultiplier()
    {
        float attackMultiplier = 1f;
        float hpMultiplier = 1f;

        switch (grade)
        {
            case MonsterGrade.Normal:
                attackMultiplier = normalAttackMultiplier;
                hpMultiplier = normalHpMultiplier;
                break;

            case MonsterGrade.Elite:
                attackMultiplier = eliteAttackMultiplier;
                hpMultiplier = eliteHpMultiplier;
                break;

            case MonsterGrade.Boss:
                attackMultiplier = bossAttackMultiplier;
                hpMultiplier = bossHpMultiplier;
                break;
        }

        attackPower = Mathf.RoundToInt(attackPower * attackMultiplier);

        int increaseHP = Mathf.RoundToInt(
            health.MaxHP * (hpMultiplier - 1f));

        health.IncreaseMaxHP(increaseHP);
    }

    private void LevelUp()
    {
        level++;

        attackPower += attackIncreasePerLevel;

        health?.IncreaseMaxHP(hpIncreasePerLevel);

        Debug.Log($"{grade} {name} Level Up! Lv.{level}");

        OnLevelUp?.Invoke(level);
    }
}