using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bg : MonoBehaviour
{
    public Transform[] layers;
    public float[] speed;
    public Transform MainCam;

    private Vector3 lastCam;

    // Start is called before the first frame update
    void Start()
    {
        lastCam = MainCam.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltaMovement = MainCam.position - lastCam;
        for (int i = 0; i < layers.Length;i++)
        {
            layers[i].position += new Vector3(deltaMovement.x * speed[i],deltaMovement.y * speed[i] * 0.75f,0);
        }

        lastCam = MainCam.position;
    }
}
