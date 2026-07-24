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

    [Header("Class Skill")]
    [SerializeField] private WarriorSkill warriorSkill;
    [SerializeField] private ArcherSkill archerSkill;
    [SerializeField] private MageSkill mageSkill;

    public PlayerClassType Class => playerClass;

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            playerClass = GameManager.Instance.SelectedClass;
        }
    }

    public ClassSkill ClassSkill
    {
        get
        {
            switch (playerClass)
            {
                case PlayerClassType.Warrior:
                    return warriorSkill;

                //case PlayerClassType.Archer:
                //    return archerSkill;

                //case PlayerClassType.Mage:
                //    return mageSkill;

                default:
                    return warriorSkill;
            }
        }
    }

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
    }

    private void OnDrawGizmosSelected()
{
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, CurrentStat.attackRange);
}
}