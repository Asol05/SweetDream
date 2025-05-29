using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3Fallen : MonoBehaviour
{
    //private int Damage = 450;

    void OnTriggerEnter2D(Collider2D Enemy)
    {
        if (Enemy.tag == "Enemy")
        {
            StartCoroutine(Delay(Enemy));
        }
        if (Enemy.tag == "Boss")
        {
            StartCoroutine(Boss(Enemy));
        }
    }

    IEnumerator Boss(Collider2D boss)
    {
        Boss DoDamage = boss.GetComponent<Boss>();
        yield return new WaitForSeconds(0.3f);
        //DoDamage.UpDateHp(Damage);
    }

    IEnumerator Delay(Collider2D Enemy)
    {
        EnemyAI DoDamage = Enemy.GetComponent<EnemyAI>();
        yield return new WaitForSeconds(0.3f);
        //DoDamage.UpDateHp(Damage);
        //Destroy(this.GameObject);
    }
}
