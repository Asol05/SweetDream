using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PlayButton : MonoBehaviour
{
    public PlayerController player;
    private float horizontalInput = 0;
    private bool jump = false;
    private bool Dash = false;
    private bool Skill1 = false;
    private bool Skill2 = false;
    private bool Skill3 = false;
    private bool ATK = false;

    public TextMeshProUGUI Skill1CoolTime;
    public TextMeshProUGUI Skill2CoolTime;
    public TextMeshProUGUI Skill3CoolTime;

    public void OnPress()
    {
        jump = true;
        StartCoroutine(Jump());
    }
    public void OnPressDash()
    {
        Dash = true;
        StartCoroutine(dash());
    }

    public void OnRightButtonDown()
    {
        horizontalInput = 1f;
    }

    public void OnRightButtonUp()
    {
        horizontalInput = 0f;
    }

    public void OnLeftButtonDown()
    {
        horizontalInput = -1f;
    }

    public void OnLeftButtonUp()
    {
        horizontalInput = 0f;
    }

    public void OnPressSkill1()
    {
        if (!Skill1CoolTime.gameObject.activeSelf)
        {
            Skill1 = true;
            StartCoroutine(skill1());
        }
    }

    public void OnPressSkill2()
    {
        if (!Skill2CoolTime.gameObject.activeSelf)
        {
            Skill2 = true;
            StartCoroutine(skill2());
        }
    }

    public void OnPressSkill3()
    {
        if (!Skill3CoolTime.gameObject.activeSelf)
        {
            Skill3 = true;
            StartCoroutine(skill3());
        }
    }

    public void OnPressATK()
    {
        ATK = true;
        StartCoroutine(Attack());
    }

    public float GetInput()
    {
        return horizontalInput;
    }

    public bool GetJump()
    {
        return jump;
    }

    public bool GetDash()
    {
        return Dash;
    }

    public bool GetSkill1()
    {
        return Skill1;
    }

    public bool GetSkill2()
    {
        return Skill2;
    }

    public bool GetSkill3()
    {
        return Skill3;
    }

    public bool GetATK()
    {
        return ATK;
    }

    void Start()
    {
        Skill1CoolTime.gameObject.SetActive(false);
        Skill2CoolTime.gameObject.SetActive(false);
        Skill3CoolTime.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Skill1 == true || player.GetQ())
        {
            Skill1CoolTime.gameObject.SetActive(true);
            Skill1CoolTime.SetText(player.Gettimeskill1().ToString());
            StartCoroutine(skill1CoolTime());
        }
        if (Skill2 == true || player.GetW())
        {
            Skill2CoolTime.gameObject.SetActive(true);
            Skill2CoolTime.SetText(player.Gettimeskill2().ToString());
            StartCoroutine(skill2CoolTime());
        }
        if (Skill3 == true || player.GetE())
        {
            Skill3CoolTime.gameObject.SetActive(true);
            Skill3CoolTime.SetText(player.Gettimeskill3().ToString());
            StartCoroutine(skill3CoolTime());
        }
    }

    IEnumerator Jump()
    {
        yield return new WaitForSeconds(0.01f);
        jump = false ;
    }

    IEnumerator dash()
    {
        yield return new WaitForSeconds(0.01f);
        Dash = false;
    }

    IEnumerator skill1()
    {
        yield return new WaitForSeconds(0.01f);
        Skill1 = false;
    }

    IEnumerator skill2()
    {
        yield return new WaitForSeconds(0.01f);
        Skill2 = false;
    }

    IEnumerator skill3()
    {
        yield return new WaitForSeconds(0.01f);
        Skill3 = false;
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.01f);
        ATK = false;
    }

    IEnumerator skill1CoolTime()
    {
        float time = player.Gettimeskill1();
        for (int i = 0; i < player.Gettimeskill1(); i++)
        {
            yield return new WaitForSeconds(1f);
            time--;
            Skill1CoolTime.SetText(time.ToString());
        }
        Skill1CoolTime.gameObject.SetActive(false);
    }

    IEnumerator skill2CoolTime()
    {
        float time = player.Gettimeskill2();
        for (int i = 0; i < player.Gettimeskill2(); i++)
        {
            yield return new WaitForSeconds(1f) ;
            time--;
            Skill2CoolTime.SetText(time.ToString());
        }
        Skill2CoolTime.gameObject.SetActive(false);
    }

    IEnumerator skill3CoolTime()
    {
        float time = player.Gettimeskill3();
        for (int i = 0; i < player.Gettimeskill3(); i++)
        {
            yield return new WaitForSeconds(1f);
            time--;
            Skill3CoolTime.SetText(time.ToString());
        }
        Skill3CoolTime.gameObject.SetActive(false);
    }
}
