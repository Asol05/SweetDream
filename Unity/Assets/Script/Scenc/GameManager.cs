using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject WinPanal;
    public GameObject Spawner;
    public AudioClip audioClip;
    public AudioSource audio;
    public AudioSource BellSFX;
    public Pause pause;
    public static bool WinStatus = false;

    private static float _startTime;
    public static float FakeTime => UnityEngine.Time.time - _startTime;

    // เรียกใช้ตอนเริ่มเกมหรือโหลดซีนใหม่
    public static void ResetTime()
    {
        WinStatus = false;
        _startTime = UnityEngine.Time.time;
    }

    // Start is called before the first frame update
    void Start()
    {
        WinPanal.SetActive(false);
        StartCoroutine(RingTheBall());
    }

    // Update is called once per frame
    void Update()
    {
        if (WinStatus)
        {
            StartCoroutine(Victory());
            Spawner.SetActive(false);
        }
    }

    IEnumerator Victory()
    {
        EnemyAI[] enemies = FindObjectsOfType<EnemyAI>();
        Boss[] bosses = FindObjectsOfType<Boss>();
        foreach (EnemyAI enemy in enemies)
        {
            enemy.ApplyCC(10f);
        }
        foreach (Boss boss in bosses)
        {
            boss.ApplyCC(10f);
        }
        yield return new WaitForSeconds(0.35f);
        foreach (EnemyAI enemy in enemies)
        {
            enemy.UpDateHp(999999);
        }
        foreach (Boss boss in bosses)
        {
            boss.UpDateHp(999999);
        }
        yield return new WaitForSeconds(1.5f);
        pause.SetPauing(true);
        WinPanal.SetActive(true);
        audio.PlayOneShot(audioClip);
        yield return new WaitForSeconds(5f);
    }

    IEnumerator RingTheBall()
    {
        yield return new WaitForSeconds(360f);
        for (int i = 0; i < 3; i++)
        {
            BellSFX.Play();
            yield return new WaitForSeconds(5f);
        }
    }
}
