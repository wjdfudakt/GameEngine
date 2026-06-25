using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private CharacterController controller;
    private Vector2 moveInput;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void Update()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

        controller.Move(move * moveSpeed * Time.deltaTime);

        if (move.sqrMagnitude > 0.01f)
        {
            transform.forward = move.normalized;
        }
    }
}