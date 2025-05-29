using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill2 : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator animator;
    [SerializeField] private PlayerController player;
    private float PlayerMove;
    private float knockup = 5f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyAI Enemy = other.GetComponent<EnemyAI>();
        if (other.tag == "Enemy")
        {
            Enemy.ApplyCC(1f);
            Enemy.UpDateHp(25);
            Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();
            enemyRb.AddForce(Vector2.up * knockup, ForceMode2D.Impulse);
        }
        Boss boss = other.GetComponent<Boss>();
        if (other.tag == "Boss")
        {
            boss.ApplyCC(1f);
            boss.UpDateHp(25);
            Rigidbody2D bossRb = other.GetComponent<Rigidbody2D>();
            bossRb.AddForce(Vector2.up * knockup, ForceMode2D.Impulse);
        }
    }
}
