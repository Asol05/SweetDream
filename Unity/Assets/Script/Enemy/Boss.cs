using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class Boss : MonoBehaviour
{

    public float speed = 4f;
    public float nextWayPoaintDistance = 3f;
    Path path;
    int currentWayPoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    Rigidbody2D rigidbody2D;
    public PlayerController target;
    private bool canMove = true;
    private bool isStuning = false;
    public float Hp = 100;
    private float MaxHp;
    public int Damage = 10;

    Animator animator;

    public Transform hpbarFill;
    private Vector3 originalScale;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        //seeker.StartPath(rigidbody2D.position, target.position, OnPathComplete);
        InvokeRepeating("UpdatePath", 1f, 1f);
        animator = GetComponent<Animator>();
        target = FindObjectOfType<PlayerController>();
        UpGrade();
        MaxHp = Hp;
        originalScale = hpbarFill.localScale;
    }

    void UpdatePath()
    {
        if (seeker.IsDone() && canMove)
        {
            seeker.StartPath(rigidbody2D.position, target.transform.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null) return;
        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 diraction = ((Vector2)path.vectorPath[currentWayPoint] - rigidbody2D.position).normalized;
        //Vector2 force = diraction * speed;

        if (canMove)
        {
            Vector2 force = new Vector2(diraction.x * speed, rigidbody2D.velocity.y);
            rigidbody2D.velocity = force;
        }
        else
        {
            rigidbody2D.AddForce(Vector2.zero);
        }

        float distance = Vector2.Distance(rigidbody2D.position, path.vectorPath[currentWayPoint]);
        if (distance < nextWayPoaintDistance)
        {
            currentWayPoint++;
        }

        if (rigidbody2D.velocity.y != 0)
        {
            animator.SetBool("isWalking", false);
            animator.Play("Blue Idle - Animation");
        }

        if (rigidbody2D.velocity.x != 0)
        {
            animator.SetBool("isWalking", true);
            if (rigidbody2D.velocity.x >= 0.01f)
            {
                transform.localScale = new Vector3(6f, 6f, 6f);
            }
            else if (rigidbody2D.velocity.x <= -0.01f)
            {
                transform.localScale = new Vector3(-6f, 6f, 6f);
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        float Hppercent = Hp / MaxHp;
        Debug.Log(Hppercent);
        if (Hppercent <= 0.6f)
        {

        }
        if (Hppercent <= 0)
        {
            Hppercent = 0;
        }
        float Fillposition = (1 - Hppercent) * -0.43f;
        hpbarFill.localScale = new Vector3(originalScale.x * Hppercent, originalScale.y, originalScale.z);
        hpbarFill.localPosition = new Vector3(Fillposition * -1f, 0f, 0f); 

        if (Hp <= 0)
        {
            Die();
        }

    }


    public void Die()
    {
        //Hp = 100;
        animator.Play("Die");
        Destroy(this.gameObject, 0.35f);
        //StartCoroutine(PlayDie());
    }

    IEnumerator PlayDie()
    {
        yield return new WaitForSeconds(0.35f);
        this.gameObject.SetActive(false);
    }

    public void UpDateHp(int Damage)
    {
        Hp = Hp - Damage;
    }

    public void ApplyCC(float time)
    {
        StartCoroutine(CannotMove(time));
    }

    IEnumerator CannotMove(float Time)
    {
        animator.Play("Blue Idle - Animation");
        isStuning = true;
        canMove = false;
        yield return new WaitForSeconds(Time);
        canMove = true;
        isStuning = false;
        animator.Play("Walk");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" && !isStuning)
        {
            canMove = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (canMove && !isStuning)
        {
            Rigidbody2D playerrb = other.GetComponent<Rigidbody2D>();
            PlayerController player = other.GetComponent<PlayerController>();
            if (other.gameObject.tag == "Player")
            {
                float khockbackDiraction = (other.transform.position.x - transform.position.x) * -1f;
                //rigidbody2D.AddForce((khockbackDiraction * -1f) * 7f,ForceMode2D.Impulse);
                transform.position += new Vector3(khockbackDiraction, 0f, 0f);
                player.UpDateHp(Damage);
            }
        }
    }

    private void UpGrade()
    {
        if (GameManager.FakeTime >= 360f)
        {
            Damage += 30;
            Hp += 1200;
        }
        else if (GameManager.FakeTime >= 300f)
        {
            Damage += 25;
            Hp += 1000;
        }
        else if (GameManager.FakeTime >= 240f)
        {
            Damage += 20;
            Hp += 800;
        }
        else if (GameManager.FakeTime >= 180f)
        {
            Damage += 15;
            Hp += 600;
        }
        else if (GameManager.FakeTime >= 120f)
        {
            Damage += 10;
            Hp += 400;
        }
        else if (GameManager.FakeTime >= 60f)
        {
            Damage += 5;
            Hp += 200;
        }
    }
}
