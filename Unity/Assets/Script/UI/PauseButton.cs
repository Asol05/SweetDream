using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public Pause pause;
    public GameObject PauseGame;

    public void OnPressPause()
    {
        pause.SetPauing(true);
        PauseGame.SetActive(true);
    }

    public void OnPressResume()
    {
        pause.SetPauing(false);
        PauseGame.SetActive(false);
    }

    public void OnPressRenew()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void OnPressHome()
    {
        SceneManager.LoadScene("Home_page");
    }
}
