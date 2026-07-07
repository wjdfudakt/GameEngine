using UnityEngine;

public class WarriorSkill : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private PlayerCombat combat;
    [SerializeField] private PlayerClass playerClass;

    [Header("Skill 1 - 연속베기")]
    [SerializeField] private float skill1DamagePercent = 150f;

    private void Awake()
    {
        if (combat == null)
            combat = GetComponent<PlayerCombat>();

        if (playerClass == null)
            playerClass = GetComponent<PlayerClass>();
    }

    public bool Skill1()
    {
        if (combat.CurrentTarget == null)
            return false;

        float distance =
        Vector3.Distance(
            transform.position,
            combat.CurrentTarget.position);

        if (distance > playerClass.AttackRange)
            return false;

        Health health =
            combat.CurrentTarget.GetComponent<Health>();

        if (health == null)
            return false;

        int damage = Mathf.RoundToInt(
            playerClass.AttackPower *
            (skill1DamagePercent / 100f));

        health.TakeDamage(damage);

        Debug.Log($"연속베기! {damage} 데미지");

        return true;
    }

    public void Skill2()
    {
        Debug.Log("전사 - 블레이드 스매시");
    }

    public void Skill3()
    {
        Debug.Log("전사 - 방패 치기");
    }

    public void Skill4()
    {
        Debug.Log("전사 - 찌르기");
    }

    public void Skill5()
    {
        Debug.Log("전사 - 전장의 함성");
    }

    public void Ultimate()
    {
        Debug.Log("전사 - 블레이드 임팩트");
    }
}