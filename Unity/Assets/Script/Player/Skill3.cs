using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3 : MonoBehaviour
{
    public GameObject effectHit;
    public GameObject effectHit2;
    public int Damage = 150;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            StartCoroutine(AttackEnemy());
        }
        if (other.tag == "Boss")
        {
            StartCoroutine(AttackBoss());
        }
    }

    IEnumerator AttackBoss()
    {
        Boss[] bosses = FindObjectsOfType<Boss>();
        foreach (Boss BossCC in bosses)
        {
            BossCC.ApplyCC(4f);
        }
        yield return new WaitForSeconds(2f);
        foreach (Boss boss in bosses)
        {
            GameObject effect = Instantiate(effectHit, boss.transform.position + new Vector3(-0.5f, 100.5f, 0f), Quaternion.identity);
            effect.transform.localScale = new Vector3(2f, 2f, 2f);
            Destroy(effect, 1f);
        }
        yield return new WaitForSeconds(1f);
        foreach (Boss boss2 in bosses)
        {
            GameObject effect2 = Instantiate(effectHit2, boss2.transform.position + new Vector3(-4.175f, 4f, 0f), Quaternion.identity);
            effect2.transform.localScale = new Vector3(2f, 2f, 2f);
            Destroy(effect2, 0.95f);
            boss2.UpDateHp(Damage);
        }
        yield return new WaitForSeconds(10f);
    }

    IEnumerator AttackEnemy()
    {
        EnemyAI[] enemies = FindObjectsOfType<EnemyAI>();
        foreach (EnemyAI EnemyCC in enemies)
        {
            EnemyCC.ApplyCC(4f);
        }
        yield return new WaitForSeconds(2f);
        foreach (EnemyAI Enemy in enemies)
        {
            GameObject effect = Instantiate(effectHit, Enemy.transform.position + new Vector3(-0.5f, 102f, 0f), Quaternion.identity);
            Destroy(effect, 1f);
        }
        yield return new WaitForSeconds(1f);
        foreach (EnemyAI Enemy2 in enemies)
        {
            GameObject effect2 = Instantiate(effectHit2, Enemy2.transform.position + new Vector3(-2.175f, 2f, 0f), Quaternion.identity);
            Destroy(effect2, 0.95f);
            Enemy2.UpDateHp(Damage);
        }
        yield return new WaitForSeconds(10f);
    }
}
