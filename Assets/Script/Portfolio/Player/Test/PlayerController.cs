using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 120f;

    private CharacterController controller;
    private Vector2 moveInput;
    private float lookInput;

    public bool IsMoving => moveInput.sqrMagnitude > 0.01f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<float>();
    }

    private void Update()
    {
        transform.Rotate(
        Vector3.up,
        lookInput * rotateSpeed * Time.deltaTime);

        Vector3 move =
            transform.forward * moveInput.y +
            transform.right * moveInput.x;

        controller.Move(
            move * moveSpeed * Time.deltaTime);
    }
}