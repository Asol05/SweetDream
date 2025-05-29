using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    public AudioSource audioSource;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (rigidbody2D.velocity.x != 0)
        {
            animator.SetBool("isWalking", true);
            if (rigidbody2D.velocity.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (rigidbody2D.velocity.x < 0)
            {
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        
        if (aIPath.desiredVelocity.x != 0)
        {
            animator.SetBool("isWalking", true);
            if (aIPath.desiredVelocity.x >= 0.01f)
            {
                transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
            }
            else if (aIPath.desiredVelocity.x <= -0.01f)
            {
                transform.localScale = new Vector3(-3.5f, 3.5f, 3.5f);
            }
        }
        

        if (rigidbody2D.velocity.y != 0)
        {
            canMove = false;
        }

        if (canMove)
        {
            //rigidbody2D.velocity = new Vector2(-4f,rigidbody2D.velocity.y);
        }
        else
        {
            animator.SetBool("isWalking" , false);
        }
        */
 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Skill")
        {
            audioSource.PlayOneShot(audioClip);
            //animator.SetTrigger("isHurting");
            StartCoroutine(Hurt());
        }
        if (other.gameObject.tag == "NormalATK")
        {
            audioSource.PlayOneShot(audioClip);
            StartCoroutine(Hurt());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Skill")
        {
            //animator.SetTrigger("isHurting");
            StartCoroutine(Hurt());
        }
    }

    IEnumerator Hurt()
    {
        animator.Play("Hurt");
        yield return new WaitForSeconds(0.3f);
        animator.Play("Blue Idle - Animation");
    }

}
