using Unity.Cinemachine;
using UnityEngine;

public class CameraPrioritySwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineCamera normalCamera;
    [SerializeField] private CinemachineCamera focusCamera;

    public void ShowFocusCamera()
    {
        normalCamera.Priority = 10;
        focusCamera.Priority = 20;
    }

    public void ShowNormalCamera()
    {
        normalCamera.Priority = 20;
        focusCamera.Priority = 10;
    }
}