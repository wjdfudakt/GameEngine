using UnityEngine;
using UnityEngine.InputSystem;

public class ClientInputTester : MonoBehaviour
{
    private Vector2 moveInput;
    private string playerState = "Idle";

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        if (moveInput.sqrMagnitude > 0.01f)
        {
            playerState = "Move";
        }
        else
        {
            playerState = "Idle";
        }
    }

    void Update()
    {
        Debug.Log($"Input: {moveInput}, State: {playerState}");//입력이 있는지 없는지 확인. 있다면 MOVE, 없다면 IDLE로 출력하는, 이동 방향만 확인하는 코드
    }
}