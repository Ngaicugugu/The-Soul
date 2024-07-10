using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int health = 500;

    private float flashDuration = 0.1f;

    private SpriteRenderer sr;
    private Color color;
    private Animator myAnim;
    private bool isDead;

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        color = sr.color;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        StartCoroutine(FlashRed());

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!isDead)
        {
            isDead = true;
            myAnim.SetTrigger("death");
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    private IEnumerator FlashRed()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        sr.color = color;
    }

    private IEnumerator DestroyAfterAnimation()
    {
        // Đợi cho đến khi trạng thái hoạt ảnh chuyển sang "death"
        while (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("death"))
        {
            yield return null;
        }

        // Đợi cho đến khi hoạt ảnh "death" kết thúc
        while (myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        // Hủy GameObject sau khi hoạt ảnh "death" đã hoàn thành
        Destroy(gameObject);
    }
}
