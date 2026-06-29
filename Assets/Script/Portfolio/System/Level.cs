using UnityEngine;
using UnityEngine.Events;

public enum LevelType
{
    Player,
    Monster
}

public class Level : MonoBehaviour
{
    public UnityEvent<int, int, int> OnExperienceChanged;

    [Header("Type")]
    [SerializeField] private LevelType levelType;

    [Header("Level")]
    [SerializeField] private int level = 1;

    [Header("Experience")]
    [SerializeField] private int currentExp;
    [SerializeField] private int requiredExp = 10;

    public int CurrentLevel => level;
    public int CurrentExp => currentExp;
    public int RequiredExp => requiredExp;

    public UnityEvent<int> OnLevelUp;

    private void Awake()
    {
        OnExperienceChanged?.Invoke(
            currentExp,
            requiredExp,
            level);
    }

    public void AddExperience(int amount)
    {
        if (levelType != LevelType.Player)
            return;

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

    public void SetLevel(int value)
    {
        level = Mathf.Max(1, value);
    }

    private void LevelUp()
    {
        level++;

        requiredExp += 10;

        Debug.Log($"{name} Level Up! Lv.{level}");

        OnLevelUp?.Invoke(level);

        OnExperienceChanged?.Invoke(
        currentExp,
        requiredExp,
        level);
    }
}