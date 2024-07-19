using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private GameObject uiPrefab;

    private SpriteRenderer sR;
    private string id;
    private int attackDamage;
    private int unlockExp;
    private bool isUnlockExp;
    private GameObject itemUIInstance;

    public SpriteRenderer SR { get => sR; set => sR = value; }
    public string Id { get => id; set => id = value; }
    public int AttackDamage { get => attackDamage; set => attackDamage = value; }
    public int UnlockExp { get => unlockExp; set => unlockExp = value; }
    public bool IsUnlockExp { get => isUnlockExp; set => isUnlockExp = value; }

    private void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
        GetComponent<Collider2D>().isTrigger = true;
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

        if (itemUIInstance == null)
        {
            itemUIInstance = Instantiate(uiPrefab, transform.position + new Vector3(0,1,0), Quaternion.identity);
            itemUIInstance.GetComponent<ItemUI>().SetItemInfo(itemData.ItemImage, itemData.AttackDamage, itemData.UnlockExp);
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (itemUIInstance != null)
            {
                Destroy(itemUIInstance);
            }
        }
    }
}
