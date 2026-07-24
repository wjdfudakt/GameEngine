using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
        if (health != null)
        {
            health.OnDeath.AddListener(OnDeath);
        }
    }

    private void OnDeath()
    {
        Debug.Log("Player Dead");
        gameObject.SetActive(false);

        Time.timeScale = 0f;
    }        
}