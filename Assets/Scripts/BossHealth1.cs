using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth1 : MonoBehaviour
{
    [SerializeField] private int health = 500;
    [SerializeField] private GameObject experiencePrefab; // Prefab của kinh nghiệm
    [SerializeField] private int experienceValue = 1; // Giá trị kinh nghiệm mỗi lần rơi
    [SerializeField] private GameObject doorPrefab;
    [SerializeField] private int bossNumber;

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
        SpawnExperience();

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
            Destroy(gameObject,1);
            ShowDoor();
            GameManager.Instance.DefeatBoss(bossNumber);
        }
    }

    private IEnumerator FlashRed()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        sr.color = color;
    }

    private void SpawnExperience()
    {
        GameObject exp = Instantiate(experiencePrefab, transform.position, Quaternion.identity);
        Experience experience = exp.GetComponent<Experience>();
        if (experience != null)
        {
            experience.value = experienceValue;
            StartCoroutine(AttractExperienceAfterDelay(experience, 1f)); // Delay 1 giây trước khi kinh nghiệm di chuyển về phía người chơi
        }
    }

    private IEnumerator AttractExperienceAfterDelay(Experience experience, float delay)
    {
        yield return new WaitForSeconds(delay);
        experience.Attract();
    }

    private void ShowDoor()
    {
        Instantiate(doorPrefab, transform.position, Quaternion.identity);
    }
}
