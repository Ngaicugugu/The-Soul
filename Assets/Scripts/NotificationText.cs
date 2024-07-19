using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationText : MonoBehaviour
{
    public float displayDuration = 2f;
    private TMP_Text notificationText;

    private void Awake()
    {
        notificationText = GetComponent<TMP_Text>();
        notificationText.enabled = false;
    }

    public void ShowNotification(string message)
    {
        notificationText.text = message;
        StartCoroutine(DisplayNotification());
    }

    private IEnumerator DisplayNotification()
    {
        notificationText.enabled = true;
        yield return new WaitForSeconds(displayDuration);
        notificationText.enabled = false;
    }
}
