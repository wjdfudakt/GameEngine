using UnityEngine;

public class Experience : MonoBehaviour
{
    [SerializeField] private int experience = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerStat playerStat = other.GetComponent<PlayerStat>();

        if (playerStat == null)
            return;
                        
        playerStat.AddExperience(experience);
        Destroy(gameObject);        
    }
}
