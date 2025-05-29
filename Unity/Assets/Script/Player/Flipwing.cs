using UnityEngine;
using System.Collections;
using Mono.CSharp.yyParser;

public class Flipwing : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public PlayButton Skill3Btn;
    public ParticleSystem particleSystem;
    private bool can = true;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        particleSystem.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space))
        {
            float x = Input.GetAxisRaw("Horizontal");
            if (x > 0f)
            {
                spriteRenderer.flipX = false;
            }
            else if (x < 0f)
            {
                spriteRenderer.flipX = true;
            }
        }

        if ((Input.GetKeyDown(KeyCode.E) || Skill3Btn.GetSkill3()) && can)
        {
            StartCoroutine(OpenPart());
            StartCoroutine(SkillCoolDown());
        }
        
    }

    IEnumerator OpenPart()
    {
        yield return new WaitForSeconds(1f);
        particleSystem.Play();
        yield return new WaitForSeconds(3f);
        particleSystem.Stop();
    }

    IEnumerator SkillCoolDown()
    {
        can = false;
        yield return new WaitForSeconds(45f);
        can = true;
    }
}
