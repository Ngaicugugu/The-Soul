using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] private GameObject notificationPrefab;
    [SerializeField] private Transform notificationParent;

    private Item nearbyItem;
    private bool isUnlock;
    private GameObject notificationInstance;
    private TMP_Text notificationText;

    private void Start()
    {
        if (notificationPrefab != null && notificationParent != null)
        {
            notificationInstance = Instantiate(notificationPrefab, notificationParent);
            notificationText = notificationInstance.GetComponent<TextMeshProUGUI>();
            if (notificationText != null)
            {
                notificationText.enabled = false;
            }
        }
    }

    private void Update()
    {
        if (nearbyItem != null && Input.GetKeyDown(KeyCode.F) && isUnlock)
        {
            CollectItem(nearbyItem);
            ShowNotification("Đã trang Bị");
            AudioManager.Instance.PlaySFX(AudioManager.Instance.PickUp);
        }
        else if(nearbyItem != null && Input.GetKeyDown(KeyCode.F) && !isUnlock)
        {
            ShowNotification("Chưa đủ kinh nghiệm");
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

    private void ShowNotification(string message)
    {
        if (notificationText != null)
        {
            notificationText.text = message;
            StartCoroutine(DisplayNotification());
        }
    }

    private IEnumerator DisplayNotification()
    {
        notificationText.enabled = true;
        yield return new WaitForSeconds(1f);  
        notificationText.enabled = false;
    }
}
