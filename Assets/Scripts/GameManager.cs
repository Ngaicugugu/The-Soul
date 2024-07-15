using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
   
    public int playerExp;
    public int playerAttackDamage;

    private void Awake()
    {
       
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Ngăn chặn đối tượng này bị phá hủy
        }
        else
        {
            Destroy(gameObject);  
        }
    }

   
    public void SavePlayerData(int exp, int attackDamage)
    {
        playerExp = exp;
        playerAttackDamage = attackDamage;
    }


    public void LoadPlayerData(out int exp, out int attackDamage, int defaultAttackDamage = 5)
    {
        exp = playerExp;
        attackDamage = playerAttackDamage > 0 ? playerAttackDamage : defaultAttackDamage;  // Sử dụng giá trị mặc định nếu attackDamage <= 0
    }
}
