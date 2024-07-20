using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossDamage : MonoBehaviour
{
    [SerializeField] private int attackDamage = 20;
    [SerializeField] private Vector3 attackOffset;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private LayerMask attackMask;
    [SerializeField] private BoxCollider2D hitBoxSkill;

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


    public void SkillOn()
    {
        hitBoxSkill.gameObject.SetActive(true);
    }

    public void SkillOff()
    {
        hitBoxSkill.gameObject.SetActive(false);
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

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
