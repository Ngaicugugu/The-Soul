using System.Collections;
using UnityEngine;
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
    public void AttackBoss()
    {
        Vector3 pos = CalculateAttackPosition();

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<BossHealth>().TakeDamage(attackDamage);
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
