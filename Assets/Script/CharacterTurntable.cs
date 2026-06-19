using UnityEngine;

public class CharacterTurntable : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 45f;

    void Start()
    {
        Debug.Log("부모 오브젝트가 회전하면 자식 오브젝트도 함께 따라갑니다.");
    }

    void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }
}