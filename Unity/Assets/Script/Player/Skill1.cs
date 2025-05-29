using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator animator;
    [SerializeField] private PlayerController player;
    private float PlayerMove;
    private float knockback = 10f;
    

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
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            if (enemy != null)
            {
                Enemy.ApplyCC(1f);
                Enemy.UpDateHp(40);
                Vector2 khockbackDiraction = (other.transform.position - transform.position).normalized;
                enemy.AddForce(khockbackDiraction * knockback , ForceMode2D.Impulse);

            }
        }
        Boss boss = other.GetComponent<Boss>();
        if (other.tag == "Boss")
        {
            Rigidbody2D bossrb = other.GetComponent<Rigidbody2D>();
            if (bossrb != null)
            {
                boss.ApplyCC(1f);
                boss.UpDateHp(40);
                Vector2 khockbackDiraction = (other.transform.position - transform.position).normalized;
                bossrb.AddForce(khockbackDiraction * knockback, ForceMode2D.Impulse);

            }
        }
    }
}
