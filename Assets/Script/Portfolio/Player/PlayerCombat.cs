using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private float detectRange = 10f;

    [SerializeField] private Transform currentTarget;

    [SerializeField] private PlayerStat level;

    private PlayerController controller;

    private readonly List<Transform> targets = new();

    private PlayerClass playerClass;

    private int damage => playerClass.AttackPower;

    private float attackRange => playerClass.AttackRange;

    private float attackCooldown => playerClass.AttackCooldown;

    private float attackTimer;

    private bool blockAutoAttack;

    private PlayerBuff buff;

    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private Animator animator;

    public Transform CurrentTarget
    {
        get => currentTarget;
        private set
        {
            if (currentTarget == value)
                return;

            currentTarget = value;

            if (currentTarget != null)
                Debug.Log($"현재 타겟 : {currentTarget.name}");
            else
                Debug.Log("현재 타겟 : 없음");
        }
    }

    public void BlockAutoAttack(bool value)
    {
        blockAutoAttack = value;
    }

    private void Awake()
    {
        level = GetComponent<PlayerStat>();
        controller = GetComponent<PlayerController>();
        playerClass = GetComponent<PlayerClass>();
        buff = GetComponent<PlayerBuff>();
        animator = GetComponentInChildren<Animator>();

        if (level != null)
        {
            level.OnLevelUp.AddListener(OnLevelUp);
        }
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;

        FindTargets();
        ValidateTarget();

        if (CurrentTarget == null)
        {
            SelectNearestTarget();
        }

        AutoAttack();
    }

    private void FindTargets()
    {
        targets.Clear();

        Collider[] hits = Physics.OverlapSphere(transform.position, detectRange);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Monster"))
            {
                targets.Add(hit.transform);
            }
        }
    }

    private void ValidateTarget()
    {
        if (CurrentTarget == null)
            return;

        if (!CurrentTarget.gameObject.activeInHierarchy)
        {
            CurrentTarget = null;
            return;
        }

        if (Vector3.Distance(transform.position, CurrentTarget.position) > detectRange)
        {
            CurrentTarget = null;
        }
    }

    private void SelectNearestTarget()
    {
        float nearestDistance = float.MaxValue;
        Transform nearestTarget = null;

        foreach (Transform target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestTarget = target;
            }
        }

        CurrentTarget = nearestTarget;
    }

    public void OnNextTarget()
    {
        ChangeTarget(1);
    }

    public void OnPreviousTarget()
    {
        ChangeTarget(-1);
    }

    private void ChangeTarget(int direction)
    {
        if (targets.Count == 0)
            return;

        int index = targets.IndexOf(CurrentTarget);

        if (index < 0)
            index = 0;

        index = (index + direction + targets.Count) % targets.Count;

        CurrentTarget = targets[index];
    }

    private void AutoAttack()
    {
        if (blockAutoAttack)
            return;

        if (controller != null && (controller.IsMoving || controller.IsAutoMoving))
            return;

        if (CurrentTarget == null)
            return;

        float distance = Vector3.Distance(transform.position, CurrentTarget.position);

        if (distance > attackRange)
            return;

        if (attackTimer < attackCooldown)
            return;

        attackTimer = 0f;

        Health health = CurrentTarget.GetComponent<Health>();

        if (health != null)
        {
            int damage = Mathf.RoundToInt(
                playerClass.AttackPower *
                buff.DamageMultiplier);

            health.TakeDamage(damage);

            if (animator != null)
            {
                animator.SetTrigger(AttackHash);
            }
        }        
    }

    private void OnDestroy()
    {
        if (level != null)
        {
            level.OnLevelUp.RemoveListener(OnLevelUp);
        }
    }

    private void OnLevelUp(int currentLevel)
    {
        playerClass.LevelUp();

        Debug.Log($"레벨 {currentLevel} 달성! 공격력 : {damage}");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}