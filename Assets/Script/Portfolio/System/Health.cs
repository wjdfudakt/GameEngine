using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHP = 100;

    public UnityEvent<int, int> OnHealthChanged;

    public int CurrentHP { get; private set; }
    public int MaxHP => maxHP;

    public UnityEvent OnDeath;

    private void Awake()
    {
        CurrentHP = maxHP;

        OnHealthChanged?.Invoke(CurrentHP, MaxHP);
    }

    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;

        if (CurrentHP < 0)
            CurrentHP = 0;

        OnHealthChanged?.Invoke(CurrentHP, MaxHP);

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

        OnHealthChanged?.Invoke(CurrentHP, MaxHP);
    }

    public void IncreaseMaxHP(int amount)
    {
        maxHP += amount;

        CurrentHP = maxHP;

        OnHealthChanged?.Invoke(CurrentHP, MaxHP);

        Debug.Log($"{name} Max HP ¡ı∞° : {maxHP}");
    }

    private void Die()
    {
        OnDeath?.Invoke();
    }
}