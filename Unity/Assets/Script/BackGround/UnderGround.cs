using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderGround : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
