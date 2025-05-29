using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalATK : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Boss boss = other.GetComponent<Boss>();
        EnemyAI Enemy = other.GetComponent<EnemyAI>();
        if (other.gameObject.tag == "Enemy")
        {
            Enemy.UpDateHp(7);
            this.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "Boss")
        {
            boss.UpDateHp(7);
            this.gameObject.SetActive(false);
        }
    }
}
