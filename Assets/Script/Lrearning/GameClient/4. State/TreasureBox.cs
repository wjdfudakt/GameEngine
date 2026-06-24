using UnityEngine;

public class TreasureBox : MonoBehaviour, IInteractable
{
    [SerializeField]private bool isOpen;

    public void Interact()
    {
        if (isOpen)
        {
            Debug.Log("상자는 이미 열려 있습니다.");
            return;
        }

        isOpen = true;
        Debug.Log("상자를 열고 보상을 확인했습니다.");
    }
}