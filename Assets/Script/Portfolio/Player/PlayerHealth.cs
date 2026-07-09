using UnityEngine;

public class PlayerHealth : Health
{
    private PlayerBuff buff;

    protected override void Awake()
    {
        base.Awake();

        buff = GetComponent<PlayerBuff>();
    }

    public override void TakeDamage(int damage)
    {
        if (buff != null)
        {
            damage = Mathf.RoundToInt(
                damage *
                (1f - buff.DamageReducePercent / 100f));
        }

        base.TakeDamage(damage);
    }
}