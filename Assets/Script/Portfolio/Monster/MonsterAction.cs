using UnityEngine;

public class MonsterAction : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private float attackTimer;

    private PlayerStat playerStat;

    private Health health;

    private MonsterStat monsterStat;

    private void Awake()
    {
        health = GetComponent<Health>();
        monsterStat = GetComponent<MonsterStat>();

        if (health != null)
        {
            health.OnDeath.AddListener(OnDeath);
        }
    }

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            target = player.transform;

            playerStat = player.GetComponent<PlayerStat>();
        }
    }

    private void Update()
    {
        if (target == null)
            return;

        float distance = Vector3.Distance(transform.position, target.position);


        attackTimer += Time.deltaTime;

        if (distance <= monsterStat.DetectRange)
        {
            if (distance > monsterStat.AttackRange)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                direction.y = 0f;

                transform.position += direction * monsterStat.MoveSpeed * Time.deltaTime;

                transform.forward = direction;
            }

            else
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        if (attackTimer < monsterStat.AttackCooldown)
            return;

        attackTimer = 0f;

        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            PlayerBuff buff = target.GetComponent<PlayerBuff>();

            if (buff != null && buff.IsGuarding)
            {
                if (Random.Range(0f, 100f) <= buff.GuardChance)
                {
                    Debug.Log("가드 성공!");
                    return;
                }
            }

            playerHealth.TakeDamage(monsterStat.AttackPower);
        }
    }

    private void OnDeath()
    {
        if (playerStat != null)
        {
            playerStat.AddExperience(monsterStat.ExperienceReward);
        }

        Destroy(gameObject);
    }    
}