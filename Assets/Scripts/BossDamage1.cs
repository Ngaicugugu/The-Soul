using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage1 : MonoBehaviour
{
    [SerializeField] private int attackDamage = 20;

    [SerializeField] private Vector3 attackOffset;
    [SerializeField] private Vector3 attackOffset2;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackRange2 = 1f;
    [SerializeField] private LayerMask attackMask;

    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }

    public void Attack2()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset2.x;
        pos += transform.up * attackOffset2.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange2, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Vector3 pos2 = transform.position;
        pos2 += transform.right * attackOffset2.x;
        pos2 += transform.up * attackOffset2.y;
        Gizmos.DrawWireSphere(pos, attackRange);
        Gizmos.DrawWireSphere(pos2, attackRange2);
    }
}
