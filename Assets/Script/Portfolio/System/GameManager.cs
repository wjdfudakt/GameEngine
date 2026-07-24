using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerClassType SelectedClass { get; private set; } = PlayerClassType.Warrior;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetSelectedClass(PlayerClassType classType)
    {
        SelectedClass = classType;
    }
}