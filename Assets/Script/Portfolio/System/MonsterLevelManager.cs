using UnityEngine;

public class MonsterLevelManager : MonoBehaviour
{
    public static MonsterLevelManager Instance;

    [SerializeField] private float levelUpInterval = 60f;

    public int CurrentMonsterLevel { get; private set; } = 1;

    private float timer;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= levelUpInterval)
        {
            timer = 0f;
            CurrentMonsterLevel++;

            Debug.Log($"몬스터 레벨 상승 : {CurrentMonsterLevel}");
        }
    }
}