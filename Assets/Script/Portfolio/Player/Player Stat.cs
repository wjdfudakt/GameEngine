using UnityEngine;
using UnityEngine.Events;

public class PlayerStat : MonoBehaviour
{
    public UnityEvent<int, int, int> OnExperienceChanged;
    public UnityEvent<int> OnLevelUp;

    [Header("Level")]
    [SerializeField] private int level = 1;

    [Header("Experience")]
    [SerializeField] private int currentExp;
    [SerializeField] private int requiredExp = 5;
    [SerializeField] private float expMultiplier = 1.2f;

    [Header("Stats")]
    [SerializeField] private int attackPower = 10;
    [SerializeField] private int attackIncreasePerLevel = 2;
    [SerializeField] private int hpIncreasePerLevel = 10;

    public int CurrentLevel => level;
    public int CurrentExp => currentExp;
    public int RequiredExp => requiredExp;
    public int AttackPower => attackPower;

    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();

        OnExperienceChanged?.Invoke(
            currentExp,
            requiredExp,
            level);
    }

    public void AddExperience(int amount)
    {
        currentExp += amount;

        Debug.Log($"{name} EXP : {currentExp}/{requiredExp}");

        while (currentExp >= requiredExp)
        {
            currentExp -= requiredExp;
            LevelUp();
        }

        OnExperienceChanged?.Invoke(
            currentExp,
            requiredExp,
            level);
    }

    private void LevelUp()
    {
        level++;

        requiredExp = Mathf.RoundToInt(requiredExp * expMultiplier);

        attackPower += attackIncreasePerLevel;

        health?.IncreaseMaxHP(hpIncreasePerLevel);

        Debug.Log($"{name} Level Up! Lv.{level}");

        OnLevelUp?.Invoke(level);

        OnExperienceChanged?.Invoke(
            currentExp,
            requiredExp,
            level);
    }
}