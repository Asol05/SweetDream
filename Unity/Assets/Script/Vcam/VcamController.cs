using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VcamController : MonoBehaviour
{
    public PlayerController player;
    public GameObject VcamPlayer;
    public GameObject VcamShake;
    private bool skill3CoolDown = false;
    public PlayButton Skill3;

    // Start is called before the first frame update
    void Start()
    {
        VcamPlayer.SetActive(true);
        VcamShake.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) || Skill3.GetSkill3()) && !skill3CoolDown)
        {
            StartCoroutine(CamShake());
            StartCoroutine(Skill3CoolDown());
        }
    }

    IEnumerator CamShake()
    {
        yield return new WaitForSeconds(3f);
        VcamPlayer.SetActive(false);
        VcamShake.SetActive(true);
        yield return new WaitForSeconds(0.35f);
        VcamShake.SetActive(false);
        VcamPlayer.SetActive(true);
    }

    IEnumerator Skill3CoolDown()
    {
        skill3CoolDown = true;
        yield return new WaitForSeconds(player.Gettimeskill3());
        skill3CoolDown = false;
    }
}
