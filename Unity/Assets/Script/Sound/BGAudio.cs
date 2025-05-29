using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGAudio : MonoBehaviour
{
    public Transform player;
    public AudioSource audioSource1;
    public AudioClip Battle;
    public AudioClip Normal;

    private int enemyCount = 0;
    private Coroutine currentFade = null;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + new Vector3(0,4.2f,0);
    }

    void OnTriggerEnter2D(Collider2D Enemy)
    {
        if (Enemy.gameObject.tag == "Enemy")
        {
            enemyCount++;
            if (enemyCount == 1)
            {
                if (currentFade != null) StopCoroutine(currentFade);
                currentFade = StartCoroutine(FadeInBattle());
            }
        }
    }

    void OnTriggerExit2D(Collider2D Enemy)
    {
        if (Enemy.gameObject.tag == "Enemy")
        {
            enemyCount--;
            if (enemyCount <= 0)
            {
                if (currentFade != null) StopCoroutine(currentFade);
                currentFade = StartCoroutine(FadeOutBattle());
            }
        }
    }

    IEnumerator FadeInBattle()
    {
        float Volume = 1f;
        audioSource1.volume = Volume;

        while (audioSource1.volume > 0f)
        {
            audioSource1.volume -= Time.deltaTime / 1f;
            yield return null;
        }
        audioSource1.volume = 0f;
        Volume = 0f;
        audioSource1.clip = Battle;
        audioSource1.Play();
        audioSource1.volume = Volume;

        while (audioSource1.volume < 1f)
        {
            audioSource1.volume += Time.deltaTime / 1f;
            yield return null;
        }
        audioSource1.volume = 1f;
    }

    IEnumerator FadeOutBattle()
    {
        float Volume = 1f;
        audioSource1.volume = Volume;

        while (audioSource1.volume > 0f)
        {
            audioSource1.volume -= Time.deltaTime / 1f;
            yield return null;
        }
        audioSource1.volume = 0f;
        audioSource1.clip = Normal;
        audioSource1.Play();
        Volume = 0f;
        audioSource1.volume = Volume;

        while (audioSource1.volume < 1f)
        {
            audioSource1.volume += Time.deltaTime / 1f;
            yield return null;
        }
        audioSource1.volume = 1f;
    }



}
