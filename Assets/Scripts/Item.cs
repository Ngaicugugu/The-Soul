using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    private SpriteRenderer sR;
    private string id;
    private int attackDamage;

    public SpriteRenderer SR { get => sR; set => sR = value; }
    public string Id { get => id; set => id = value; }
    public int AttackDamage { get => attackDamage; set => attackDamage = value; }

    private void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SR.sprite = itemData.ItemImage;
        Id = itemData.ID;
        AttackDamage = itemData.AttackDamage;   
    }
}
