using UnityEngine;

public class WarriorSkill : MonoBehaviour
{
    //
    [SerializeField] private PlayerBuff buff;

    [Header("Reference")]
    [SerializeField] private PlayerCombat combat;
    [SerializeField] private PlayerClass playerClass;

    [Header("Skill 1 - 연속베기")]
    [SerializeField] private float skill1DamagePercent = 150f;

    [Header("Skill 2 - 블레이드 스매시")]
    [SerializeField] private float skill2DamagePercent = 250f;
    [SerializeField] private Vector3 skill2BoxSize = new Vector3(3f, 5f, 2f);
    [SerializeField] private float skill2ForwardOffset = 2f;

    [Header("Skill 3 - 방패 치기")]
    [SerializeField] private float skill3DamagePercent = 180f;
    [SerializeField] private float knockbackDistance = 10f;
    [SerializeField] private float guardDuration = 10f;
    [SerializeField] private float guardChance = 50f;
    [SerializeField] private float guardDamageReduce = 25f;

    [Header("Skill 4 - 찌르기")]
    [SerializeField] private float skill4DamagePercent = 200f;
    [SerializeField] private float skill4DashDistance = 5f;
    [SerializeField] private float skill4DashRadius = 1f;

    [Header("Skill 5 - 전장의 함성")]
    [SerializeField] private float skill5Duration = 8f;
    [SerializeField] private float attackBuff = 30f;
    [SerializeField] private float damageReduction = 20f;

    private void Awake()
    {
        if (combat == null)
            combat = GetComponent<PlayerCombat>();

        if (playerClass == null)
            playerClass = GetComponent<PlayerClass>();

        if (buff == null)
            buff = GetComponent<PlayerBuff>();
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

    public bool Skill2()
    {
        Vector3 center =
            transform.position +
            transform.forward * skill2ForwardOffset;

        Collider[] hits = Physics.OverlapBox(
            center,
            skill2BoxSize * 0.5f,
            transform.rotation);

        bool hit = false;

        int damage = Mathf.RoundToInt(
            playerClass.AttackPower *
            (skill2DamagePercent / 100f));

        foreach (Collider collider in hits)
        {
            if (!collider.CompareTag("Monster"))
                continue;

            Health health = collider.GetComponent<Health>();

            if (health == null)
                continue;

            health.TakeDamage(damage);

            hit = true;
        }

        if (hit)
        {
            Debug.Log($"블레이드 스매시! {damage} 데미지");
        }

        return hit;
    }

    public bool Skill3()
    {
        if (combat.CurrentTarget == null)
            return false;

        float distance = Vector3.Distance(
            transform.position,
            combat.CurrentTarget.position);

        if (distance > playerClass.AttackRange)
            return false;

        Health health = combat.CurrentTarget.GetComponent<Health>();

        if (health == null)
            return false;

        int damage = Mathf.RoundToInt(
            playerClass.AttackPower *
            (skill3DamagePercent / 100f));

        health.TakeDamage(damage);

        Vector3 dir = (combat.CurrentTarget.position - transform.position).normalized;

        combat.CurrentTarget.position += dir * knockbackDistance;

        buff.StartGuard(
        guardDuration,
        guardChance,
        guardDamageReduce);

        Debug.Log($"방패 치기! {damage} 데미지");

        return true;
    }

    public bool Skill4()
    {
        int damage = Mathf.RoundToInt(
            playerClass.AttackPower *
            (skill4DamagePercent / 100f));

        Vector3 start = transform.position;
        Vector3 end = start + transform.forward * skill4DashDistance;

        Collider[] hits = Physics.OverlapCapsule(
            start,
            end,
            skill4DashRadius);

        bool hit = false;

        foreach (Collider collider in hits)
        {
            if (!collider.CompareTag("Monster"))
                continue;

            Health health = collider.GetComponent<Health>();

            if (health == null)
                continue;

            health.TakeDamage(damage);

            hit = true;
        }

        CharacterController controller =
            GetComponent<CharacterController>();

        if (controller != null)
        {
            controller.Move(
                transform.forward * skill4DashDistance);
        }
        else
        {
            transform.position +=
                transform.forward * skill4DashDistance;
        }

        if (hit)
        {
            Debug.Log($"찌르기! {damage} 데미지");
        }

        return hit;
    }

    public void Skill5()
    {
        buff.StartBattleCry(
        skill5Duration,
        attackBuff,
        damageReduction);

        Debug.Log("전장의 함성 발동");
    }

    public void Ultimate()
    {
        Debug.Log("전사 - 블레이드 임팩트");
    }
}