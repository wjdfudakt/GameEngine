using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    void Interact();
}

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField]private IInteractable currentTarget;
    [SerializeField]private string characterState = "Idle";

    public void OnInteract(InputValue value)
    {
        if (!value.isPressed || currentTarget == null)
        {
            return;
        }

        characterState = "Interact";
        currentTarget.Interact();
        Debug.Log($"State: {characterState}");
        characterState = "Idle";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            currentTarget = interactable;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable) && interactable == currentTarget)
        {
            currentTarget = null;
        }
    }
}