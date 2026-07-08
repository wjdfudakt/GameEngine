using UnityEngine;

public class WarriorSkill : MonoBehaviour
{
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
    [SerializeField] private float knockbackDistance = 2f;
    [SerializeField] private float guardDuration = 5f;
    [SerializeField] private float guardChance = 30f;


    private bool isGuarding;
    private float guardTimer;

    public bool IsGuarding => isGuarding;
    public float GuardChance => guardChance;

    private void Awake()
    {
        if (combat == null)
            combat = GetComponent<PlayerCombat>();

        if (playerClass == null)
            playerClass = GetComponent<PlayerClass>();
    }

    private void Update()
    {
        if (!isGuarding)
            return;

        guardTimer -= Time.deltaTime;

        if (guardTimer <= 0f)
        {
            isGuarding = false;
            Debug.Log("가드 종료");
        }
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

        Health health =
            combat.CurrentTarget.GetComponent<Health>();

        if (health == null)
            return false;

        int damage = Mathf.RoundToInt(
            playerClass.AttackPower *
            (skill3DamagePercent / 100f));

        health.TakeDamage(damage);

        Vector3 dir = (combat.CurrentTarget.position - transform.position).normalized;

        combat.CurrentTarget.position += dir * knockbackDistance;

        isGuarding = true;
        guardTimer = guardDuration;

        Debug.Log($"방패 치기! {damage} 데미지");

        return true;
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

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;

    //    Matrix4x4 oldMatrix = Gizmos.matrix;

    //    Gizmos.matrix = Matrix4x4.TRS(
    //        transform.position,
    //        transform.rotation,
    //        Vector3.one);

    //    Gizmos.DrawWireCube(
    //        Vector3.forward * skill2ForwardOffset,
    //        skill2BoxSize);

    //    Gizmos.matrix = oldMatrix;
    //}
}