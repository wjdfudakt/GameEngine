using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHP = 100;

    public int CurrentHP { get; private set; }
    public int MaxHP => maxHP;

    public UnityEvent OnDeath;

    private void Awake()
    {
        CurrentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;

        if (CurrentHP < 0)
            CurrentHP = 0;

        Debug.Log($"{name} HP : {CurrentHP}");

        if (CurrentHP <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        CurrentHP += amount;

        if (CurrentHP > maxHP)
            CurrentHP = maxHP;
    }

    private void Die()
    {
        OnDeath?.Invoke();
    }
}