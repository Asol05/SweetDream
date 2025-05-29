using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject enenyPrefab;
    public GameObject BossPrefab;
    public GameObject PortalGate;
    public GameObject Player;
    public float spawn = 1f;
    public float nextspawn = 7f;

    private void Start()
    {
        GameManager.ResetTime();
        StartCoroutine(GainSpawn());
        StartCoroutine(BigWave());
        StartCoroutine(BuiltPortal());
        StartCoroutine(FisrtWave());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.FakeTime >= spawn)
        {
            GameObject enemy = Instantiate(enenyPrefab, transform.position + new Vector3(Random.Range(-180f,180f),1f,0f), Quaternion.identity);
            spawn = GameManager.FakeTime + nextspawn;
        }
        ///Debug.Log(GameManager.FakeTime);
    }

    IEnumerator GainSpawn()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(120f);
            nextspawn--;
        }
    }

    IEnumerator BuiltPortal()
    {
        yield return new WaitForSeconds(361f);
        GameObject Portal = Instantiate(PortalGate, transform.position + new Vector3(Random.Range(-200f, 200f), 3f, 0f), Quaternion.identity);
    }

    IEnumerator FisrtWave()
    {
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < 8; i++)
        {
            GameObject enemy = Instantiate(enenyPrefab, Player.transform.position + new Vector3(Random.Range(-80f, 80f), 1f, 0f), Quaternion.identity);
        }
    }

    IEnumerator BigWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);
            for (int i = 0; i < 6; i++)
            {
                GameObject enemy = Instantiate(enenyPrefab, Player.transform.position + new Vector3(Random.Range(-60f, 60f), 1f, 0f), Quaternion.identity);
            }
            for (int i = 0; i < 2; i++)
            {
                GameObject boss = Instantiate(BossPrefab, Player.transform.position + new Vector3(Random.Range(-80f, 80f), 1f, 0f), Quaternion.identity);
                boss.SetActive(true);
            }
        }
    }
}
