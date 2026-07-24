using UnityEngine;

public class MageSkill : ClassSkill
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

    [Header("Skill 1 - ")]
    [SerializeField] private float skill1Cooldown;

    [Header("Skill 2 - ")]
    [SerializeField] private float skill2Cooldown;

    [Header("Skill 3 - ")]
    [SerializeField] private float skill3Cooldown;

    [Header("Skill 4 - ")]
    [SerializeField] private float skill4Cooldown;

    [Header("Skill 5 - ")]
    [SerializeField] private float skill5Cooldown;

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
        return true;
    }

    public override bool Skill2()
    {
        return true;
    }

    public override bool Skill3()
    {
        return true;
    }

    public override bool Skill4()
    {
        return true;
    }

    public override bool Skill5()
    {
        return true;
    }

    public void Ultimate()
    {
        Debug.Log("");
    }
}
