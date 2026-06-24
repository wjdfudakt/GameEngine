using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleInventory : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 50;

    private readonly List<ItemData> items = new List<ItemData>();

    void Update()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            AddItem(new ItemData("Small Potion", 20));
        }

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            UseFirstItem();
        }
    }

    private void AddItem(ItemData item)
    {
        items.Add(item);
        Debug.Log($"{item.Name}을(를) 인벤토리에 넣었습니다.");
    }

    private void UseFirstItem()
    {
        if (items.Count == 0)
        {
            Debug.Log("사용할 아이템이 없습니다.");
            return;
        }

        ItemData item = items[0];
        items.RemoveAt(0);

        currentHealth = Mathf.Min(currentHealth + item.HealAmount, maxHealth);
        Debug.Log($"{item.Name} 사용. 현재 체력: {currentHealth}/{maxHealth}");
    }
}

public class ItemData
{
    public string Name { get; }
    public int HealAmount { get; }

    public ItemData(string name, int healAmount)
    {
        Name = name;
        HealAmount = healAmount;
    }
}