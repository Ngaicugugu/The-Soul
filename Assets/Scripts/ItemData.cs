using UnityEngine;

[CreateAssetMenu(fileName ="item_",menuName ="Data/Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string id;
    [SerializeField] private int attackDamage;
    [SerializeField] private Sprite itemImage;
    [SerializeField] private int unlockExp;

    public string ID => id;
    public int AttackDamage => attackDamage;  
    public Sprite ItemImage => itemImage;
    public int UnlockExp => unlockExp;
}

