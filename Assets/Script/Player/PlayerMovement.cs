using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 120f;

    void Update()
    {
        Vector3 move = Vector3.zero;

        if (Keyboard.current.wKey.isPressed)
            move += transform.forward;

        if (Keyboard.current.sKey.isPressed)
            move -= transform.forward;

        if (Keyboard.current.aKey.isPressed)
            move -= transform.right;

        if (Keyboard.current.dKey.isPressed)
            move += transform.right;

        transform.position += move.normalized * moveSpeed * Time.deltaTime;

        float rotation = 0f;

        if (Keyboard.current.leftArrowKey.isPressed)
            rotation = -1f;

        if (Keyboard.current.rightArrowKey.isPressed)
            rotation = 1f;

        transform.Rotate(
            Vector3.up,
            rotation * rotateSpeed * Time.deltaTime
        );
    }
}