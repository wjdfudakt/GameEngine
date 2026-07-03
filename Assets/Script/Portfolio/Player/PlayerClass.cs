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

    [Header("Warrior")]
    [SerializeField] private ClassStat warrior;

    [Header("Archer")]
    [SerializeField] private ClassStat archer;

    [Header("Mage")]
    [SerializeField] private ClassStat mage;

    public PlayerClassType Class => playerClass;

    private ClassStat CurrentStat
    {
        get
        {
            switch (playerClass)
            {
                case PlayerClassType.Warrior:
                    return warrior;

                case PlayerClassType.Archer:
                    return archer;

                case PlayerClassType.Mage:
                    return mage;

                default:
                    return warrior;
            }
        }
    }

    public int AttackPower => CurrentStat.attackPower;
    public int AttackIncreasePerLevel => CurrentStat.attackIncreasePerLevel;
    public float AttackRange => CurrentStat.attackRange;
    public float AttackCooldown => CurrentStat.attackCooldown;

    public void LevelUp()
    {
        CurrentStat.attackPower += CurrentStat.attackIncreasePerLevel;

        Debug.Log($"{playerClass} 공격력 증가 → {CurrentStat.attackPower}");
    }

    private void OnDrawGizmosSelected()
{
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, CurrentStat.attackRange);
}
}