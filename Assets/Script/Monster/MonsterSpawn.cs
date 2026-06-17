using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs;
    public Transform player;

    public float spawnRadius = 10f;//생성 범위(반지름)
    public float spawnInterval = 2f;//생성 주기

    void Start()
    {
        InvokeRepeating(nameof(Spawn), 1f, spawnInterval);
    }

    void Spawn()
    {
        if (player == null || monsterPrefabs.Length == 0) return;

        // 랜덤 몬스터 선택
        GameObject prefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];

        Vector2 random = Random.insideUnitCircle * spawnRadius;

        Vector3 spawnPos = player.position + new Vector3(random.x, 0.5f, random.y);

        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}