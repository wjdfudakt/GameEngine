using UnityEngine;

public enum PlayerClassType
{
    Warrior,
    Archer,
    Mage
}

public class PlayerClass : MonoBehaviour
{
    [Header("Class")]
    [SerializeField] private PlayerClassType playerClass;

    [Header("Attack")]
    [SerializeField] private int attackPower = 10;
    [SerializeField] private int attackIncreasePerLevel = 2;
    [SerializeField] private float attackRange = 20f;
    [SerializeField] private float attackCooldown = 0.5f;

    public PlayerClassType Class => playerClass;

    public int AttackPower => attackPower;
    public float AttackRange => attackRange;
    public float AttackCooldown => attackCooldown;

    public void LevelUp()
    {
        attackPower += attackIncreasePerLevel;

        Debug.Log($"{playerClass} 공격력 증가 → {attackPower}");
    }
}