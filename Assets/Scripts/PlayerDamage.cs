using System.Collections;
using UnityEngine;
public class PlayerDamage : MonoBehaviour
{
    public static PlayerDamage Instance;

    [SerializeField] public int attackDamage = 100;

    [SerializeField] private Vector3 attackOffset;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private LayerMask attackMask;

    private void Awake()
    {
        Instance = this;
    }
    public void AttackBoss()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<BossHealth>().TakeDamage(attackDamage);
        }
    }


    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
