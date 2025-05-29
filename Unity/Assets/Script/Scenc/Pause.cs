using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Unity.VisualScripting;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Pause : MonoBehaviour
{
    //public AudioMixerSnapshot pauseSnapshot;
    //public AudioMixerSnapshot unpauseSnapshot;
    private Canvas canvas;
    public PlayerController player;
    public Canvas RewardAdsPanal;
    public GameObject PlayerControl;
    private bool Pauing = false; 

    // Start is called before the first frame update
    void Start()
    {
        RewardAdsPanal.enabled = false;
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.enabled = !canvas.enabled;
            if (Time.timeScale == 0)
            {
                unpause();
            }
            else
            {
                pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }


        if (player.IsDie() || Pauing)
        {
            pause();
            PlayerControl.SetActive(false);
            if (player.IsDie())
            {
                RewardAdsPanal.enabled = true;
                PlayerControl.SetActive(false);
            }
            
        }
        else
        {
            unpause();
            RewardAdsPanal.enabled = false;
            PlayerControl.SetActive(true);
        }
    }
    public void pause()
    {
        Time.timeScale = 0;
    }
    public void unpause()
    {
        Time.timeScale = 1;
    }

    public void SetPauing(bool Newvalue)
    {
        Pauing = Newvalue;
    }
}
