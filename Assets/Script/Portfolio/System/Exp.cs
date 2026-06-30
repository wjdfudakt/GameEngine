using UnityEngine;

public class Experience : MonoBehaviour
{
    [SerializeField] private int experience = 1;

    private void OnTriggerEnter(Collider other)
    {
        Level level = other.GetComponent<Level>();

        if (level == null)
            return;

        if (level.Type != LevelType.Player)
            return;
        
        level.AddExperience(experience);
        Destroy(gameObject);        
    }
}
