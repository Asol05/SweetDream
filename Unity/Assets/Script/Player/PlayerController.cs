using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool canjump = true;
    private bool canDash = true;
    private bool canMove = true;
    private int Diraction = 0;
    public float jumpForce = 500f;
    public float MoveSpeed = 5f;
    private bool isGround = false;
    private bool isSecondJump = false;
    public float DashSpeed = 150f;
    public float DashTime = 0.2f;
    private bool Dashing = false;
    private bool DashCD = false;
    private Vector2 DashDiraction;
    Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;
    Animator animator;
    public AudioSource DashSFX;
    public AudioSource attacked;
    public GameObject PauseGame;
    public int Hp = 100;

    public GameObject skill1;
    private bool usingskill1 = false;
    private float skill1timeCoolDown = 3f;
    private bool skill1CoolDown = false;

    public GameObject skill2;
    private float skill2timeCoolDown = 7f;
    private bool skill2CoolDown = false;

    public GameObject skill3;
    private float skill3timeCoolDown = 45f;
    private bool skill3CoolDown = false;

    public GameObject NormalATK;
    private bool ATKCoolDown = false;

    public int GetHP()
    {
        return Hp;
    }

    public float GetVelocityX()
    {
        return rigidbody2D.velocity.x;
    }

    public float Gettimeskill1()
    {
        return skill1timeCoolDown;
    }

    public float Gettimeskill2()
    {
        return skill2timeCoolDown;
    }

    public float Gettimeskill3()
    {
        return skill3timeCoolDown;
    }

    public bool GetQ()
    {
        if (!skill1CoolDown)
        {
            return Input.GetKeyDown(KeyCode.Q);
        }
        else
        {
            return false;
        }
    }

    public bool GetW()
    {
        if (!skill2CoolDown)
        {
            return Input.GetKeyDown(KeyCode.W);
        }
        else
        {
            return false;
        }
    }

    public bool GetE()
    {
        if (!skill3CoolDown)
        {
            return Input.GetKeyDown(KeyCode.E);
        }
        else
        {
            return false;
        }
    }

    public void UpDateHp(int Damage)
    {
        attacked.Play();
        Hp -= Damage;
        StartCoroutine(Attacked());
        //Debug.Log("Attacked by Enemy. Hp : " + Hp);
    }

    public void Heal(int Healamount)
    {
        Hp = 0;
        Hp += Healamount;
        Debug.Log($"Get Heal : +{Healamount}" + $" Hp : {Hp}");
    }

    public void Die()
    {
        //Destroy(this.gameObject);
    }

    public bool IsDie()
    {
        if (Hp <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public PlayButton LeftButton;
    public PlayButton RightButton;
    public PlayButton JumpBtn;
    public PlayButton DashBtn;
    public PlayButton Skill1Btn;
    public PlayButton Skill2Btn;
    public PlayButton Skill3Btn;
    public PlayButton ATKBtn;

    private float x = 0;
    private float BtnL = 0;
    private float BtnR = 0;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        skill1.SetActive(false);
        skill2.SetActive(false);
        skill3.SetActive(false);
        NormalATK.SetActive(false);
        PauseGame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        skill1.transform.position = rigidbody2D.position;
        skill2.transform.position = rigidbody2D.position;
        skill3.transform.position = rigidbody2D.position;
        NormalATK.transform.position = rigidbody2D.position;
        if (((BtnL + BtnR) > 0) || x > 0)
        {
            Diraction = 1;
        }
        else if (((BtnL + BtnR) < 0) || x < 0)
        {
            Diraction = -1;
        }

        if (canMove)
        {
            MovePlayer();
        }
        Abilities();

        if (Hp <= 0)
        {
            Die();
        }
    }

    void MovePlayer()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || JumpBtn.GetJump()) && (isGround == true || isSecondJump == true) && canjump)
        {
            StartCoroutine(AFJump());
            if (isGround == false && isSecondJump == true)
            {
                isSecondJump = false;
            }
            isGround = false;
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        }
        else if ((Input.GetKeyDown(KeyCode.LeftShift) || DashBtn.GetDash()) && !DashCD)
        {
            StartCoroutine(AFDash());
            DashSFX.PlayOneShot(DashSFX.clip);
            StartCoroutine(Dash());
            StartCoroutine(DashCoolDown());
        }
        else if (!Dashing)
        {
            x = Input.GetAxisRaw("Horizontal");
            BtnL = LeftButton.GetInput();
            BtnR = RightButton.GetInput();
            if (x != 0)
            {
                rigidbody2D.velocity = new Vector2(x * MoveSpeed, rigidbody2D.velocity.y);
            }
            else 
            {
                rigidbody2D.velocity = new Vector2((BtnL + BtnR) * MoveSpeed, rigidbody2D.velocity.y);
            }

        }

        if (rigidbody2D.velocity.x != 0)
        {
            animator.SetBool("isMoving", true);
            if (Diraction == 1)
            {
                spriteRenderer.flipX = false;
            }
            else if (Diraction == -1)
            {
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (rigidbody2D.velocity.y != 0)
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
    }

    void Abilities()
    {
        if ((Input.GetKeyDown(KeyCode.Q) || Skill1Btn.GetSkill1()) && !skill1CoolDown)
        {
            StartCoroutine(UsingSkill1());
            StartCoroutine(Skill1CoolDown());
        }
        else if ((Input.GetKeyDown(KeyCode.W) || Skill2Btn.GetSkill2()) && !skill2CoolDown)
        {
            StartCoroutine(UsingSkill2());
            StartCoroutine(MoveAtSkill2());
            StartCoroutine(Skill2CoolDown());
        }
        else if ((Input.GetKeyDown(KeyCode.E) || Skill3Btn.GetSkill3()) && !skill3CoolDown)
        {
            StartCoroutine(UsingSkill3());
            StartCoroutine(Skill3CoolDown());
        }
        else if ((Input.GetKeyDown(KeyCode.R) || ATKBtn.GetATK()) && !ATKCoolDown)
        {
            StartCoroutine(Attacking());
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.tag == "Ground")
        {
            isGround = true;
            animator.SetBool("isJumping", false);
        }
        if (isGround == true)
        {
            isSecondJump = true;    
        }
    }

    void FixedUpdate()
    {
        if (Dashing)
        {
            if (Diraction == 1)
            {
                //rigidbody2D.AddForce(Vector2.right * DashSpeed, ForceMode2D.Force);
                transform.position += new Vector3(1.5f,0f,0f);
            }
            else if (Diraction == -1)
            {
                //rigidbody2D.AddForce(Vector2.left * DashSpeed, ForceMode2D.Force);
                transform.position += new Vector3(-1.5f, 0f, 0f);
            }
        }
        else if (usingskill1)
        {
            if (Diraction == 1)
            {
                //rigidbody2D.AddForce(Vector2.left * 1200, ForceMode2D.Force);
                transform.position += new Vector3(-1.25f, 0f, 0f);
            }
            else if (Diraction == -1)
            {
                //rigidbody2D.AddForce(Vector2.right * 1200, ForceMode2D.Force);
                transform.position += new Vector3(1.25f, 0f, 0f);
            }
        }
    }

    IEnumerator Dash()
    {
        Dashing = true;
        animator.SetBool("isDashing",true);
        DashDiraction = rigidbody2D.velocity.normalized;
        yield return new WaitForSeconds(DashTime);
        animator.SetBool("isDashing", false);
        Dashing = false;
    }

    IEnumerator DashCoolDown()
    {
        DashCD = true;
        yield return new WaitForSeconds(3f);
        DashCD = false;
    }

    IEnumerator UsingSkill1()
    {
        Animator SkillDiraction = skill1.GetComponent<Animator>();
        SpriteRenderer Skillflip = skill1.GetComponent<SpriteRenderer>();
        animator.Play("UsingSkill1");
        skill1.SetActive(true);

        if (Diraction == 1)
        {
            SkillDiraction.Play("Skill_1_Move_R");
            Skillflip.flipX = false;
        }
        else if (Diraction == -1)
        {
            SkillDiraction.Play("Skill_1_Move_L");
            Skillflip.flipX = true;
        }

        usingskill1 = true;
        yield return new WaitForSeconds(0.15f);
        usingskill1 = false;
        yield return new WaitForSeconds(0.15f);
        animator.Play("Idle");
        yield return new WaitForSeconds(0.30f);
        skill1.SetActive(false);
    }

    IEnumerator Skill1CoolDown()
    {
        skill1CoolDown = true;
        yield return new WaitForSeconds(skill1timeCoolDown);
        skill1CoolDown = false;
    }

    IEnumerator UsingSkill2()
    {
        Animator SkillDiraction = skill2.GetComponent<Animator>();
        SpriteRenderer Skillflip = skill2.GetComponent<SpriteRenderer>();

        skill2.SetActive(true);

        if (Diraction == 1)
        {
            SkillDiraction.Play("Skill_2_Move_R");
            Skillflip.flipX = false;
        }
        else if (Diraction == -1)
        {
            SkillDiraction.Play("Skill_2_Move_L");
            Skillflip.flipX = true;
        }

        yield return new WaitForSeconds(0.6f);
        skill2.SetActive(false);
    }

    IEnumerator MoveAtSkill2()
    {
        rigidbody2D.AddForce(new Vector2(0f, jumpForce * 2));
        yield return new WaitForSeconds(0.2f);
        rigidbody2D.velocity = Vector2.zero;
    }

    IEnumerator Skill2CoolDown()
    {
        skill2CoolDown = true;
        yield return new WaitForSeconds(skill2timeCoolDown);
        skill2CoolDown = false;
    }

    IEnumerator UsingSkill3()
    {
        animator.SetBool("isUseUtimate", true);
        skill3.SetActive(true);
        yield return new WaitForSeconds(3.25f);
        animator.SetBool("isUseUtimate", false);
        //animator.Play("Idle");
        yield return new WaitForSeconds(2.75f);
        skill3.SetActive(false);
    }

    IEnumerator Skill3CoolDown()
    {
        skill3CoolDown = true;
        yield return new WaitForSeconds(skill3timeCoolDown);
        skill3CoolDown = false;
    }

    IEnumerator Attacking()
    {
        ATKCoolDown = true;
        Animator SkillDiraction = NormalATK.GetComponent<Animator>();
        SpriteRenderer Skillflip = NormalATK.GetComponent<SpriteRenderer>();
        NormalATK.SetActive(true);
        if (Diraction == 1)
        {
            SkillDiraction.Play("NormalATK_R");
            Skillflip.flipX = false;
        }
        else if (Diraction == -1)
        {
            SkillDiraction.Play("NormalATK_L");
            Skillflip.flipX = true;
        }
        yield return new WaitForSeconds(0.38f);
        NormalATK.SetActive(false);
        ATKCoolDown = false;
    }


    IEnumerator Attacked()
    {
        animator.Play("Attacked");
        canMove = false;
        yield return new WaitForSeconds(0.25f);
        canMove = true;
        animator.Play("Idle");
    }

    IEnumerator AFJump()
    {
        canjump = false;
        yield return new WaitForSeconds(0.02f);
        canjump = true;
    }

    IEnumerator AFDash()
    {
        canDash = false;
        yield return new WaitForSeconds(0.02f);
        canDash = true;
    }
}
