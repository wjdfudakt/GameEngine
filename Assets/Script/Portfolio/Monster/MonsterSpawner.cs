using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [Header("Monster Prefab")]
    [SerializeField] private GameObject normalMonsterPrefab;
    [SerializeField] private GameObject eliteMonsterPrefab;
    [SerializeField] private GameObject bossMonsterPrefab;

    [Header("Spawn Cooldown")]
    [SerializeField] private float normalSpawnCooldown = 2f;
    [SerializeField] private float eliteSpawnCooldown = 20f;
    [SerializeField] private float bossSpawnCooldown = 60f;

    [Header("Spawn Settings")]
    [SerializeField] private Transform spawnCenter;
    [SerializeField] private float spawnRadius = 5f;

    private float normalTimer;
    private float eliteTimer;
    private float bossTimer;

    private void Awake()
    {
        if (spawnCenter == null)
            spawnCenter = transform;
    }

    private void Update()
    {
        normalTimer += Time.deltaTime;
        eliteTimer += Time.deltaTime;
        bossTimer += Time.deltaTime;

        if (normalMonsterPrefab != null &&
            normalTimer >= normalSpawnCooldown)
        {
            SpawnMonster(normalMonsterPrefab);
            normalTimer = 0f;
        }

        if (eliteMonsterPrefab != null &&
            eliteTimer >= eliteSpawnCooldown)
        {
            SpawnMonster(eliteMonsterPrefab);
            eliteTimer = 0f;
        }

        if (bossMonsterPrefab != null &&
            bossTimer >= bossSpawnCooldown)
        {
            SpawnMonster(bossMonsterPrefab);
            bossTimer = 0f;
        }
    }

    private void SpawnMonster(GameObject monsterPrefab)
    {
        Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;

        Vector3 spawnPosition =
            spawnCenter.position +
            new Vector3(randomOffset.x, 2f, randomOffset.y);

        Instantiate(
            monsterPrefab,
            spawnPosition,
            Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(spawnCenter != null ? spawnCenter.position : transform.position, spawnRadius);
    }
}