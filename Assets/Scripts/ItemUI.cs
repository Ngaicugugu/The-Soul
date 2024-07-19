using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemInfoText;



    public void SetItemInfo(Sprite image, int attackDamage, int unlockExp)
    {
        itemImage.sprite = image;
        itemInfoText.text = $"Damage: {attackDamage}\n\nExp: {PlayerController.Instance.experiencePoints}\\{unlockExp}";
    }

}
