using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSFX : MonoBehaviour
{
    public PlayerController player;

    public AudioSource skill1;
    public AudioClip skill1SFX;
    private bool skill1CoolDown = false;

    public AudioSource skill2;
    public AudioClip skill2SFX;
    private bool skill2CoolDown = false;

    public AudioSource skill3;
    public AudioClip skill3SFX1;
    public AudioClip skill3SFX2;
    private bool skill3CoolDown = false;

    public AudioSource audioSource;
    public AudioClip audioClip;
    private bool ATKCoolDown = false;

    public PlayButton Skill1Btn;
    public PlayButton Skill2Btn;
    public PlayButton Skill3Btn;
    public PlayButton ATKBtn;

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Q) || Skill1Btn.GetSkill1()) && !skill1CoolDown)
        {
            skill1.PlayOneShot(skill1SFX);
            StartCoroutine(Skill1CoolDown());
        }
        else if ((Input.GetKeyDown(KeyCode.W) || Skill2Btn.GetSkill2()) && !skill2CoolDown)
        {
            skill2.PlayOneShot(skill2SFX);
            StartCoroutine(Skill2CoolDown());
        }
        else if ((Input.GetKeyDown(KeyCode.E) || Skill3Btn.GetSkill3()) && !skill3CoolDown)
        {
            StartCoroutine(Skill3CoolDown());
        }
        else if ((Input.GetKeyDown(KeyCode.R) || ATKBtn.GetATK()) && !ATKCoolDown)
        {
            StartCoroutine(ATKSX());
        }
    }

    IEnumerator Skill1CoolDown()
    {
        skill1CoolDown = true;
        yield return new WaitForSeconds(player.Gettimeskill1());
        skill1CoolDown = false;
    }

    IEnumerator Skill2CoolDown()
    {
        skill2CoolDown = true;
        yield return new WaitForSeconds(player.Gettimeskill2());
        skill2CoolDown = false;
    }

    IEnumerator Skill3CoolDown()
    {
        skill3CoolDown = true;
        skill3.PlayOneShot(skill3SFX1);
        yield return new WaitForSeconds(player.Gettimeskill3() - 42f);
        skill3.PlayOneShot(skill3SFX2);
        yield return new WaitForSeconds(player.Gettimeskill3() - 3f);
        skill3CoolDown = false;
    }

    IEnumerator ATKSX()
    {
        ATKCoolDown = true;
        audioSource.PlayOneShot(audioClip);
        yield return new WaitForSeconds(0.38f);
        ATKCoolDown = false;
    }
}
