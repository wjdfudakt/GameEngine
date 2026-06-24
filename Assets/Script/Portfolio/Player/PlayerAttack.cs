using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float detectDistance = 20f;
    [SerializeField] private float attackDistance = 5f;

    [SerializeField] private Transform currentTarget;

    private readonly List<Transform> detectedTargets = new();

    private int currentTargetIndex = -1;

    private void Update()
    {
        FindTargets();

        if (currentTarget == null)
            return;

        float distance = Vector3.Distance(
            transform.position,
            currentTarget.position);

        if (distance <= attackDistance)
        {
            Debug.Log($"공격! : {currentTarget.name}");
        }
    }

    private void FindTargets()
    {
        GameObject[] monsters =
            GameObject.FindGameObjectsWithTag("Monster");

        foreach (GameObject monster in monsters)
        {
            float distance = Vector3.Distance(
                transform.position,
                monster.transform.position);

            bool isDetected =
                detectedTargets.Contains(monster.transform);

            if (distance <= detectDistance && !isDetected)
            {
                detectedTargets.Add(monster.transform);

                Debug.Log($"{monster.name} 발견!");

                if (currentTarget == null)
                {
                    currentTarget = monster.transform;
                    currentTargetIndex =
                        detectedTargets.IndexOf(monster.transform);

                    Debug.Log($"타겟 선택 : {currentTarget.name}");
                }
            }

            if (distance > detectDistance && isDetected)
            {
                int removedIndex =
                    detectedTargets.IndexOf(monster.transform);

                detectedTargets.Remove(monster.transform);

                Debug.Log($"{monster.name} 놓침!");

                if (monster.transform == currentTarget)
                {
                    currentTarget = null;
                    currentTargetIndex = -1;

                    if (detectedTargets.Count > 0)
                    {
                        currentTargetIndex = 0;
                        currentTarget = detectedTargets[0];

                        Debug.Log(
                            $"타겟 변경 : {currentTarget.name}");
                    }
                }
                else if (removedIndex < currentTargetIndex)
                {
                    currentTargetIndex--;
                }
            }
        }

        detectedTargets.RemoveAll(t => t == null);
    }

    // E
    public void OnNextTarget(InputValue value)
    {
        if (!value.isPressed)
            return;

        if (detectedTargets.Count <= 1)
            return;

        currentTargetIndex++;

        if (currentTargetIndex >= detectedTargets.Count)
            currentTargetIndex = 0;

        currentTarget = detectedTargets[currentTargetIndex];

        Debug.Log($"다음 타겟 : {currentTarget.name}");
    }

    // Q
    public void OnPreviousTarget(InputValue value)
    {
        if (!value.isPressed)
            return;

        if (detectedTargets.Count <= 1)
            return;

        currentTargetIndex--;

        if (currentTargetIndex < 0)
            currentTargetIndex = detectedTargets.Count - 1;

        currentTarget = detectedTargets[currentTargetIndex];

        Debug.Log($"이전 타겟 : {currentTarget.name}");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(
            transform.position,
            detectDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(
            transform.position,
            attackDistance);
    }
}