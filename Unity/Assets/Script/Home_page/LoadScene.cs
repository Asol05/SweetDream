using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string Ingame = "Prototype_wip_01";
    public void OnPressStart()
    {
        Time.timeScale = 0.35f;
        SceneManager.LoadScene(Ingame);
    }

    private void Update()
    {
        if (Time.time > 0)
        {
            Time.timeScale = 1;
        }
    }
}
