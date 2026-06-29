using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{

    [Header("Spawn Settings")]
    [SerializeField] private GameObject monsterPrefab;

    [SerializeField] private float spawnCooldown = 2f;

    [SerializeField] private Transform spawnCenter;
    [SerializeField] private float spawnRadius = 5f;

    private float timer;

    private void Awake()
    {
        if (spawnCenter == null)
            spawnCenter = transform;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnCooldown)
        {
            SpawnMonster();
            timer = 0f;
        }
    }

    private void SpawnMonster()
    {
        if (monsterPrefab == null)
            return;

        Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = transform.position + new Vector3(randomOffset.x, 2f, randomOffset.y);

        Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
