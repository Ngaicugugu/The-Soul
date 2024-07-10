using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] int baseAttackDamage;

    private List<ItemData> equippedItems= new List<ItemData>();

    public void EquipItem(ItemData item)
    {
        equippedItems.Add(item);
        RecalculateStat();
    }

    public void RemoveItem(ItemData item)
    {
        equippedItems.Remove(item);
        RecalculateStat();
    }

    public void RecalculateStat()
    {

    }

    public int GetFinalAttackDamage()
    {
        var finalAttackDamage = baseAttackDamage;
        finalAttackDamage += equippedItems.Sum(i => i.AttackDamage);
        return finalAttackDamage;
    }
}
