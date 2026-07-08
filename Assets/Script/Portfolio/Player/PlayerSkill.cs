using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkill : MonoBehaviour
{
    private PlayerController controller;

    private bool waitingSkill1;

    [Header("Cooldown")]
    [SerializeField] private float skill1Cooldown = 5f;
    [SerializeField] private float skill2Cooldown = 10f;
    [SerializeField] private float skill3Cooldown = 12f;
    [SerializeField] private float skill4Cooldown = 15f;
    [SerializeField] private float skill5Cooldown = 30f;

    [Header("Ultimate Skill")]
    [SerializeField] private float ultimateGauge = 0f;
    [SerializeField] private float maxUltimateGauge = 100f;
    [SerializeField] private float skill1Gauge = 5f;
    [SerializeField] private float skill2Gauge = 10f;
    [SerializeField] private float skill3Gauge = 10f;
    [SerializeField] private float skill4Gauge = 10f;
    [SerializeField] private float skill5Gauge = 20f;

    [Header("Class Skill")]
    [SerializeField] private WarriorSkill warriorSkill;

    public float UltimateGauge => ultimateGauge;
    public float UltimateGaugePercent => ultimateGauge / maxUltimateGauge;
    public float MaxUltimateGauge => maxUltimateGauge;

    private float skill1Timer;
    private float skill2Timer;
    private float skill3Timer;
    private float skill4Timer;
    private float skill5Timer;

    public float Skill1Remain => Mathf.Max(0, skill1Cooldown - skill1Timer);
    public float Skill2Remain => Mathf.Max(0, skill2Cooldown - skill2Timer);
    public float Skill3Remain => Mathf.Max(0, skill3Cooldown - skill3Timer);
    public float Skill4Remain => Mathf.Max(0, skill4Cooldown - skill4Timer);
    public float Skill5Remain => Mathf.Max(0, skill5Cooldown - skill5Timer);

    public float Skill1Cooldown => skill1Cooldown;
    public float Skill2Cooldown => skill2Cooldown;
    public float Skill3Cooldown => skill3Cooldown;
    public float Skill4Cooldown => skill4Cooldown;
    public float Skill5Cooldown => skill5Cooldown;

    private PlayerCombat combat;

    private void Awake()
    {
        if (warriorSkill == null)
            warriorSkill = GetComponent<WarriorSkill>();

        controller = GetComponent<PlayerController>();
        combat = GetComponent<PlayerCombat>();

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


        CheckWaitingSkill1();
    }   

    public void OnSkill1(InputValue value)
    {
        if (!value.isPressed)
            return;

        if (skill1Timer < skill1Cooldown)
            return;

        if (warriorSkill.Skill1())
        {
            skill1Timer = 0f;
            AddUltimateGauge(skill1Gauge);
            return;
        }

        if (combat.CurrentTarget == null)
            return;

        controller.StartAutoMove();

        waitingSkill1 = true;
    }

    public void OnSkill2(InputValue value)
    {
        if (!value.isPressed)
            return;

        if (skill2Timer < skill2Cooldown)
            return;

        skill2Timer = 0f;
        Skill2();
    }

    public void OnSkill3(InputValue value)
    {
        if (!value.isPressed)
            return;

        if (skill3Timer < skill3Cooldown)
            return;

        skill3Timer = 0f;
        Skill3();
    }

    public void OnSkill4(InputValue value)
    {
        if (!value.isPressed)
            return;

        if (skill4Timer < skill4Cooldown)
            return;

        skill4Timer = 0f;
        Skill4();
    }

    public void OnSkill5(InputValue value)
    {
        if (!value.isPressed)
            return;

        if (skill5Timer < skill5Cooldown)
            return;

        skill5Timer = 0f;
        Skill5();
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

    private void CheckWaitingSkill1()
    {
        if (!waitingSkill1)
            return;

        if (controller.IsAutoMoving)
            return;

        if (warriorSkill.Skill1())
        {
            skill1Timer = 0f;
            AddUltimateGauge(skill1Gauge);
        }

        waitingSkill1 = false;
    }

    private void Skill2()
    {
        warriorSkill.Skill2();

        AddUltimateGauge(skill2Gauge);
    }

    private void Skill3()
    {
        warriorSkill.Skill3();

        AddUltimateGauge(skill3Gauge);
    }

    private void Skill4()
    {
        warriorSkill.Skill4();

        AddUltimateGauge(skill4Gauge);
    }

    private void Skill5()
    {
        warriorSkill.Skill5();

        AddUltimateGauge(skill5Gauge);
    }

    private void Skill6()
    {
        Debug.Log("Ultimate Skill");
    }
}