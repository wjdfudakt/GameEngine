using UnityEngine;

public class GoalRewardEvent : MonoBehaviour
{
    [SerializeField] private int rewardGold = 100;

    private bool isCompleted;

    void OnTriggerEnter(Collider other)
    {
        if (isCompleted || !other.CompareTag("Player"))
        {
            return;
        }

        isCompleted = true;
        GiveReward();
        Debug.Log("목표 지점에 도착했습니다. 클라이언트 미션 완료!");
    }

    private void GiveReward()
    {
        Debug.Log($"보상 지급: Gold +{rewardGold}");
    }
}