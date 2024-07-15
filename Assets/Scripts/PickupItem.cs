using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private Item nearbyItem;
    private bool isUnlock;

    private void Update()
    {
        if (nearbyItem != null && Input.GetKeyDown(KeyCode.F) && isUnlock)
        {
            CollectItem(nearbyItem);
        }
        else if(nearbyItem != null && Input.GetKeyDown(KeyCode.F) && !isUnlock)
        {
            Debug.Log("Chua du kinh nghiem");
        }
    }

    private void CollectItem(Item item)
    {
        PlayerDamage.Instance.attackDamage = item.AttackDamage;
        nearbyItem = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            nearbyItem = item;
            isUnlock = item.IsUnlockExp;
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
