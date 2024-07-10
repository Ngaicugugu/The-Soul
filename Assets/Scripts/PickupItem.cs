using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private Item nearbyItem;

    private void Update()
    {
        if (nearbyItem != null && Input.GetKeyDown(KeyCode.F))
        {
            CollectItem(nearbyItem);
        }
    }

    private void CollectItem(Item item)
    {
        Debug.Log("Collected item: " + item.Id + " with attack damage: " + item.AttackDamage);

        PlayerDamage.Instance.attackDamage = item.AttackDamage;

        // Thêm các hành động khác khi lấy item, ví dụ: tăng chỉ số cho người chơi
        Destroy(item.gameObject);
        nearbyItem = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            nearbyItem = item;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null && item == nearbyItem)
        {
            nearbyItem = null;
        }
    }
}
