using UnityEngine;

public class PlayerBuff : MonoBehaviour
{
    private PlayerClass playerClass;


    [Header("Skill 3 - 방패 치기")]
    private bool isGuarding;
    private float guardTimer;
    private float guardChance;
    private float guardDamageReduce;

    public bool IsGuarding => isGuarding;
    public float GuardChance => guardChance;
    public float GuardDamageReduce => guardDamageReduce;


    [Header("Skill 5 - 전장의 함성")]
    private bool battleCry;
    private float battleCryTimer;
    private float damageMultiplier = 1f;
    private float damageReducePercent = 0f;

    public float DamageMultiplier => damageMultiplier;
    public float DamageReducePercent => damageReducePercent;

    private void Awake()
    {
        playerClass = GetComponent<PlayerClass>();
    }

    private void Update()
    {
        UpdateGuard();
        UpdateBattleCry();
    }

    
    private void UpdateGuard()
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

    private void UpdateBattleCry()
    {
        if (!battleCry)
            return;

        battleCryTimer -= Time.deltaTime;

        if (battleCryTimer <= 0f)
        {
            battleCry = false;

            damageMultiplier = 1f;
            damageReducePercent = 0f;

            Debug.Log("전장의 함성 종료");
        }
    }

    public void StartGuard(
        float duration,
        float chance,
        float reducePercent)
    {
        isGuarding = true;

        guardTimer = duration;

        guardChance = chance;

        guardDamageReduce = reducePercent;

        Debug.Log("가드 시작");
    }

    public void StartBattleCry(
        float duration,
        float attackIncrease,
        float damageReduce)
    {
        battleCry = true;

        battleCryTimer = duration;

        damageMultiplier =
            1f + attackIncrease / 100f;

        damageReducePercent =
            damageReduce;

        Debug.Log("전장의 함성 시작");
    }
}