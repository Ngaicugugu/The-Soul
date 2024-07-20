using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    public static PlayerDamage Instance;

    [SerializeField] public int attackDamage;

    [SerializeField] private Vector3 attackOffset;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private LayerMask attackMask;

    private bool facingRight = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);  // Phá hủy instance trùng lặp
        }
    }
    public void OnAttackBoss()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        switch (sceneName)
        {
            case "Boss1Scene":
                AttackBoss1();
                break;
            case "Boss2Scene":
                Debug.Log("Đã thấy scene: " + sceneName);
                AttackBoss2();
                break;
            default:
                Debug.LogWarning("Scene không được nhận diện: " + sceneName);
                break;
        }
        
    }

    private void AttackBoss3()
    {
        Vector3 pos = CalculateAttackPosition();

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<BossHealth>().TakeDamage(attackDamage);
        }
    }

    private void AttackBoss2()
    {
        Vector3 pos = CalculateAttackPosition();

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<BossHealth1>().TakeDamage(attackDamage);
        }
    }

    private void AttackBoss1()
    {
        Vector3 pos = CalculateAttackPosition();

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<BossHealth2>().TakeDamage(attackDamage);
        }
    }

    private Vector3 CalculateAttackPosition()
    {
        Vector3 pos = transform.position;
        if (facingRight)
        {
            pos += transform.right * attackOffset.x;
        }
        else
        {
            pos -= transform.right * attackOffset.x; // Đảo ngược x nếu quay mặt sang trái
        }
        pos += transform.up * attackOffset.y;
        return pos;
    }

    public void SetFacingDirection(bool isFacingRight)
    {
        facingRight = isFacingRight;
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = CalculateAttackPosition();
        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
