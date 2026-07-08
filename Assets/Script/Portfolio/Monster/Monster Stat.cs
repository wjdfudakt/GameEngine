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

    private float attackMultiplier = 1f;
    private float hpMultiplier = 1f;
    private float expMultiplier = 1f;

    [Header("Grade")]
    [SerializeField] private MonsterGrade grade = MonsterGrade.Normal;

    [Header("Grade Multiplier")]
    [Header("Normal")]
    [SerializeField] private float normalAttackMultiplier = 1f;
    [SerializeField] private float normalHpMultiplier = 1f;
    [Header("Elite")]
    [SerializeField] private float eliteAttackMultiplier = 1.5f;
    [SerializeField] private float eliteHpMultiplier = 2f;
    [Header("Boss")]
    [SerializeField] private float bossAttackMultiplier = 3f;
    [SerializeField] private float bossHpMultiplier = 5f;

    [Header("Level")]
    [SerializeField] private int level = 1;
    [SerializeField] private int attackIncreasePerLevel = 1;
    [SerializeField] private int hpIncreasePerLevel = 5;

    [Header("Stats")]
    [SerializeField] private float detectRange = 10f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private int attackPower = 5;
    [SerializeField] private float attackCooldown = 1f;

    [Header("Reward")]
    [SerializeField] private int experienceReward = 1;

    public int ExperienceReward => experienceReward;

    public int CurrentLevel => level;

    public int AttackPower => attackPower;
    public float AttackCooldown => attackCooldown;
    public float MoveSpeed => moveSpeed;
    public float DetectRange => detectRange;
    public float AttackRange => attackRange;

    public MonsterGrade Grade => grade;

    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();

        ApplyGradeMultiplier();
    }

    private void Start()
    {
        int targetLevel = MonsterLevelManager.Instance.CurrentMonsterLevel;

        while (level < targetLevel)
        {
            LevelUp();
        }
    }

    private void ApplyGradeMultiplier()
    {
        switch (grade)
        {
            case MonsterGrade.Normal:
                attackMultiplier = normalAttackMultiplier;
                hpMultiplier = normalHpMultiplier;
                expMultiplier = 1f;
                break;

            case MonsterGrade.Elite:
                attackMultiplier = eliteAttackMultiplier;
                hpMultiplier = eliteHpMultiplier;
                expMultiplier = 3f;
                break;

            case MonsterGrade.Boss:
                attackMultiplier = bossAttackMultiplier;
                hpMultiplier = bossHpMultiplier;
                expMultiplier = 10f;
                break;
        }

        attackPower = Mathf.RoundToInt(attackPower * attackMultiplier);

        int increaseHP =
            Mathf.RoundToInt(health.MaxHP * (hpMultiplier - 1f));

        health.IncreaseMaxHP(increaseHP);

        experienceReward =
            Mathf.RoundToInt(experienceReward * expMultiplier);
    }

    private void LevelUp()
    {
        level++;

        attackPower += Mathf.RoundToInt(
            attackIncreasePerLevel * attackMultiplier);

        health?.IncreaseMaxHP(hpIncreasePerLevel);

        Debug.Log($"{grade} {name} Level Up! Lv.{level}");

        OnLevelUp?.Invoke(level);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}