using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "itemDatabase", menuName = "Data/item database")]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] List<ItemData> items;

    public ItemData GetItemByID(string id)
    {
        return items.FirstOrDefault(x=>x.ID.Equals(id));
    }
}
