using UnityEngine;

public class WarriorSkill : ClassSkill
{
    [SerializeField] private PlayerBuff buff;

    private float skill1Timer;
    private float skill2Timer;
    private float skill3Timer;
    private float skill4Timer;
    private float skill5Timer;

    public override float Skill1Cooldown => skill1Cooldown;
    public override float Skill2Cooldown => skill2Cooldown;
    public override float Skill3Cooldown => skill3Cooldown;
    public override float Skill4Cooldown => skill4Cooldown;
    public override float Skill5Cooldown => skill5Cooldown;

    public override float Skill1Remain => Mathf.Max(0f, skill1Cooldown - skill1Timer);
    public override float Skill2Remain => Mathf.Max(0f, skill2Cooldown - skill2Timer);
    public override float Skill3Remain => Mathf.Max(0f, skill3Cooldown - skill3Timer);
    public override float Skill4Remain => Mathf.Max(0f, skill4Cooldown - skill4Timer);
    public override float Skill5Remain => Mathf.Max(0f, skill5Cooldown - skill5Timer);

    public override bool CanUseSkill1() => skill1Timer >= skill1Cooldown;
    public override bool CanUseSkill2() => skill2Timer >= skill2Cooldown;
    public override bool CanUseSkill3() => skill3Timer >= skill3Cooldown;
    public override bool CanUseSkill4() => skill4Timer >= skill4Cooldown;
    public override bool CanUseSkill5() => skill5Timer >= skill5Cooldown;

    public override void StartSkill1Cooldown() => skill1Timer = 0f;
    public override void StartSkill2Cooldown() => skill2Timer = 0f;
    public override void StartSkill3Cooldown() => skill3Timer = 0f;
    public override void StartSkill4Cooldown() => skill4Timer = 0f;
    public override void StartSkill5Cooldown() => skill5Timer = 0f;

    [Header("Reference")]
    [SerializeField] private PlayerCombat combat;
    [SerializeField] private PlayerClass playerClass;

    [Header("Skill 1 - 연속베기")]
    [SerializeField] private float skill1DamagePercent = 150f;
    [SerializeField] private float skill1Cooldown = 5f;

    [Header("Skill 2 - 블레이드 스매시")]
    [SerializeField] private float skill2DamagePercent = 250f;
    [SerializeField] private Vector3 skill2BoxSize = new Vector3(3f, 5f, 2f);
    [SerializeField] private float skill2ForwardOffset = 2f;
    [SerializeField] private float skill2Cooldown = 12f;

    [Header("Skill 3 - 방패 치기")]
    [SerializeField] private float skill3DamagePercent = 180f;
    [SerializeField] private float knockbackDistance = 10f;
    [SerializeField] private float guardDuration = 10f;
    [SerializeField] private float guardChance = 50f;
    [SerializeField] private float guardDamageReduce = 25f;
    [SerializeField] private float skill3Cooldown = 10f;

    [Header("Skill 4 - 찌르기")]
    [SerializeField] private float skill4DamagePercent = 200f;
    [SerializeField] private float skill4DashDistance = 5f;
    [SerializeField] private float skill4DashRadius = 1f;
    [SerializeField] private float skill4Cooldown = 9f;

    [Header("Skill 5 - 전장의 함성")]
    [SerializeField] private float skill5Duration = 8f;
    [SerializeField] private float attackBuff = 30f;
    [SerializeField] private float damageReduction = 20f;
    [SerializeField] private float skill5Cooldown = 15f;

    private void Awake()
    {
        if (combat == null)
            combat = GetComponent<PlayerCombat>();

        if (playerClass == null)
            playerClass = GetComponent<PlayerClass>();

        if (buff == null)
            buff = GetComponent<PlayerBuff>();
    }

    private void Start()
    {
        skill1Timer = skill1Cooldown;
        skill2Timer = skill2Cooldown;
        skill3Timer = skill3Cooldown;
        skill4Timer = skill4Cooldown;
        skill5Timer = skill5Cooldown;
    }
    private void Update()
    {
        skill1Timer += Time.deltaTime;
        skill2Timer += Time.deltaTime;
        skill3Timer += Time.deltaTime;
        skill4Timer += Time.deltaTime;
        skill5Timer += Time.deltaTime;
    }

    public override bool Skill1()
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

    public override bool Skill2()
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

    public override bool Skill3()
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

    public override bool Skill4()
    {
        if (combat.CurrentTarget == null)
            return false;

        float distance = Vector3.Distance(
            transform.position,
            combat.CurrentTarget.position);

        if (distance > playerClass.AttackRange)
            return false;

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
            controller.Move(transform.forward * skill4DashDistance);
        }
        else
        {
            transform.position += transform.forward * skill4DashDistance;
        }

        Debug.Log($"찌르기! {damage} 데미지");

        return true;
    }

    public override bool Skill5()
    {
        if (combat.CurrentTarget == null)
            return false;

        buff.StartBattleCry(
            skill5Duration,
            attackBuff,
            damageReduction);

        Debug.Log("전장의 함성 발동");

        return true;
    }

    public void Ultimate()
    {
        Debug.Log("전사 - 블레이드 임팩트");
    }
}