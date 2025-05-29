using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvas : MonoBehaviour
{
    public GameObject Camera;

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.transform.position;
    }

}
