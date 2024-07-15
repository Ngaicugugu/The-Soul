using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    private SpriteRenderer sR;
    private string id;
    private int attackDamage;
    private int unlockExp;
    private bool isUnlockExp;

    public SpriteRenderer SR { get => sR; set => sR = value; }
    public string Id { get => id; set => id = value; }
    public int AttackDamage { get => attackDamage; set => attackDamage = value; }
    public int UnlockExp { get => unlockExp; set => unlockExp = value; }
    public bool IsUnlockExp { get => isUnlockExp; set => isUnlockExp = value; }

    private void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SR.sprite = itemData.ItemImage;
        Id = itemData.ID;
        AttackDamage = itemData.AttackDamage;
        UnlockExp = itemData.UnlockExp;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerController != null)
        {
            if(playerController.experiencePoints >= UnlockExp)
            {
                isUnlockExp = true;
            }
            else
            {
                isUnlockExp = false;
            }
        }

    }
}
