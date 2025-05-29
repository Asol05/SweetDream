using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Coroutine staycoroutine;
    private bool inTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("In");
        if (collision.tag == "Player")
        {
            
            inTrigger = true;
            staycoroutine = StartCoroutine(Win());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inTrigger = false;
            StopCoroutine(Win());
            staycoroutine = null;
        }
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(3f);
        if (inTrigger)
        {
            GameManager.WinStatus = true;
        }
    }
}
