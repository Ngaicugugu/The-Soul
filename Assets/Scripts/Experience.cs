using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    public int value = 1; // Giá trị kinh nghiệm mặc định
    public float attractionSpeed = 5f; // Tốc độ di chuyển về phía người chơi
    public float collectionDistance = 1.5f; // Khoảng cách để thu thập kinh nghiệm

    private Transform player;
    private Rigidbody2D rb;
    private bool isAttracted = false;
    private bool hasLanded = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (hasLanded && isAttracted)
        {
            // Di chuyển kinh nghiệm về phía người chơi
            transform.position = Vector2.MoveTowards(transform.position, player.position, attractionSpeed * Time.deltaTime);

            // Kiểm tra khoảng cách để thu thập kinh nghiệm
            if (Vector2.Distance(transform.position, player.position) <= collectionDistance)
            {
                Collect();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra nếu kinh nghiệm đã chạm đất
        if (collision.collider.CompareTag("Ground"))
        {
            hasLanded = true;
            rb.isKinematic = true; // Ngừng ảnh hưởng của vật lý
            rb.velocity = Vector2.zero; // Dừng mọi chuyển động
        }
    }

    public void Attract()
    {
        isAttracted = true;
    }

    private void Collect()
    {
        PlayerController.Instance.GainExperience(value);
        Destroy(gameObject); // Hủy kinh nghiệm sau khi người chơi thu thập
    }
}
