using UnityEngine;

public class CameraFocusZone : MonoBehaviour
{
    [SerializeField] private CameraPrioritySwitcher cameraSwitcher;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraSwitcher.ShowFocusCamera();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraSwitcher.ShowNormalCamera();
        }
    }
}