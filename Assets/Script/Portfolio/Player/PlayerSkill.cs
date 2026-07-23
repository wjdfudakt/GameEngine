using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkill : MonoBehaviour
{
    private PlayerController controller;
    private PlayerCombat combat;
    private ClassSkill classSkill;
    private PlayerClass playerClass;
    public ClassSkill ClassSkill => classSkill;

    private bool waitingSkill1;
    private bool waitingSkill2;
    private bool waitingSkill3;
    private bool waitingSkill4;
    private bool waitingSkill5;

    private static readonly int Skill1TriggerHash = Animator.StringToHash("Skill1");
    private static readonly int Skill2TriggerHash = Animator.StringToHash("Skill2");
    private static readonly int Skill3TriggerHash = Animator.StringToHash("Skill3");
    private static readonly int Skill4TriggerHash = Animator.StringToHash("Skill4");
    private static readonly int Skill5TriggerHash = Animator.StringToHash("Skill5");

    private Animator animator;

    [Header("Ultimate Skill")]
    [SerializeField] private float ultimateGauge = 0f;
    [SerializeField] private float maxUltimateGauge = 100f;
    [SerializeField] private float skill1Gauge = 5f;
    [SerializeField] private float skill2Gauge = 10f;
    [SerializeField] private float skill3Gauge = 10f;
    [SerializeField] private float skill4Gauge = 10f;
    [SerializeField] private float skill5Gauge = 20f;

    public float UltimateGauge => ultimateGauge;
    public float UltimateGaugePercent => ultimateGauge / maxUltimateGauge;
    public float MaxUltimateGauge => maxUltimateGauge;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        combat = GetComponent<PlayerCombat>();
        playerClass = GetComponent<PlayerClass>();
        animator = GetComponentInChildren<Animator>();

        classSkill = playerClass.ClassSkill;

        Debug.Log(classSkill);

        controller.OnAutoMoveArrived += OnAutoMoveArrived;
    }


    public void OnSkill1(InputValue value)
    {
        if (!value.isPressed)
            return;

        if (!classSkill.CanUseSkill1())
            return;

        if (classSkill.Skill1())
        {
            classSkill.StartSkill1Cooldown();
            AddUltimateGauge(skill1Gauge);
            animator.SetTrigger(Skill1TriggerHash);
            return;
        }

        if (combat.CurrentTarget == null)
            return;

        waitingSkill1 = true;

        combat.BlockAutoAttack(true);

        controller.StartAutoMove();
    }

    public void OnSkill2(InputValue value)
    {
        if (!value.isPressed)
            return;

        if (!classSkill.CanUseSkill2())
            return;

        if (classSkill.Skill2())
        {
            classSkill.StartSkill2Cooldown();
            AddUltimateGauge(skill2Gauge);
            animator.SetTrigger(Skill2TriggerHash);
            return;
        }

        if (combat.CurrentTarget == null)
            return;

        waitingSkill2 = true;

        combat.BlockAutoAttack(true);

        controller.StartAutoMove();
    }

    public void OnSkill3(InputValue value)
    {
        if (!value.isPressed)
            return;

        if (!classSkill.CanUseSkill3())
            return;

        if (classSkill.Skill3())
        {
            classSkill.StartSkill3Cooldown();
            AddUltimateGauge(skill3Gauge);
            animator.SetTrigger(Skill3TriggerHash);
            return;
        }

        if (combat.CurrentTarget == null)
            return;

        waitingSkill3 = true;

        combat.BlockAutoAttack(true);

        controller.StartAutoMove();
    }

    public void OnSkill4(InputValue value)
    {
        if (!value.isPressed)
            return;

        if (!classSkill.CanUseSkill4())
            return;

        if (classSkill.Skill4())
        {
            classSkill.StartSkill4Cooldown();
            AddUltimateGauge(skill4Gauge);
            animator.SetTrigger(Skill4TriggerHash);
            return;
        }

        if (combat.CurrentTarget == null)
            return;

        waitingSkill4 = true;

        combat.BlockAutoAttack(true);

        controller.StartAutoMove();
    }

    public void OnSkill5(InputValue value)
    {
        if (!value.isPressed)
            return;

        if (!classSkill.CanUseSkill5())
            return;

        if (classSkill.Skill5())
        {
            classSkill.StartSkill5Cooldown();
            AddUltimateGauge(skill5Gauge);
            animator.SetTrigger(Skill5TriggerHash);
            return;
        }

        if (combat.CurrentTarget == null)
            return;

        waitingSkill5 = true;

        combat.BlockAutoAttack(true);

        controller.StartAutoMove();
    }

    public void OnSkill6(InputValue value)
    {
        if (!value.isPressed)
            return;

        if (ultimateGauge < maxUltimateGauge)
            return;

        ultimateGauge = 0f;
        Skill6();
    }

    private void AddUltimateGauge(float amount)
    {
        ultimateGauge += amount;

        if (ultimateGauge > maxUltimateGauge)
            ultimateGauge = maxUltimateGauge;
    }

    private void OnAutoMoveArrived()
    {
        if (waitingSkill1)
        {
            waitingSkill1 = false;

            if (classSkill.Skill1())
            {
                classSkill.StartSkill1Cooldown();
                AddUltimateGauge(skill1Gauge);
                animator.SetTrigger(Skill1TriggerHash);
            }
        }
        else if (waitingSkill2)
        {
            waitingSkill2 = false;

            if (classSkill.Skill2())
            {
                classSkill.StartSkill2Cooldown();
                AddUltimateGauge(skill2Gauge);
                animator.SetTrigger(Skill2TriggerHash);
            }
        }
        else if (waitingSkill3)
        {
            waitingSkill3 = false;

            if (classSkill.Skill3())
            {
                classSkill.StartSkill3Cooldown();
                AddUltimateGauge(skill3Gauge);
                animator.SetTrigger(Skill3TriggerHash);
            }
        }
        else if (waitingSkill4)
        {
            waitingSkill4 = false;

            if (classSkill.Skill4())
            {
                classSkill.StartSkill4Cooldown();
                AddUltimateGauge(skill4Gauge);
                animator.SetTrigger(Skill4TriggerHash);
            }
        }
        else if (waitingSkill5)
        {
            waitingSkill5 = false;

            if (classSkill.Skill5())
            {
                classSkill.StartSkill5Cooldown();
                AddUltimateGauge(skill5Gauge);
                animator.SetTrigger(Skill5TriggerHash);
            }
        }

        combat.BlockAutoAttack(false);
    }

    private void Skill6()
    {
        Debug.Log("Ultimate Skill");
    }

    private void OnDestroy()
    {
        if (controller != null)
            controller.OnAutoMoveArrived -= OnAutoMoveArrived;
    }
}