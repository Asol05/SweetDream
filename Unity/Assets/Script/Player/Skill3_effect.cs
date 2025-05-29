using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3_effect : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Change());
    }

    IEnumerator Change()
    {
        yield return new WaitForSeconds(1.01f);
        transform.position = new Vector3(0, 0, 0);
    }

}
