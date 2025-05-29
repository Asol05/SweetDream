using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    public TextMeshProUGUI TxtTime;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.ResetTime();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GameManager.FakeTime);
        if (GameManager.FakeTime >= 370f)
        {
            TxtTime.enabled = false;
        }
        else if (GameManager.FakeTime >= 363f)
        {
            TxtTime.fontSize = 20;
            TxtTime.color = Color.red;
            TxtTime.SetText("The door of SweetDream is Open. You must fine it.");
        }
        else if (GameManager.FakeTime >= 361f)
        {
            TxtTime.color = Color.red;
            TxtTime.SetText("00 : 00");
        }
        else
        {
            UpdateTime(GameManager.FakeTime);
        }
        //Debug.Log(Time.time);
    }

    public void UpdateTime(float time)
    {
        int Realminute = Mathf.FloorToInt(time / 60);
        int Realseconds = Mathf.FloorToInt(time % 60);

        int minute = 5 - Realminute;
        int seconds = 60 - Realseconds;

        if (seconds == 60)
        {
            minute += 1;
            TxtTime.text = string.Format("{0:00} : {1:00}", minute, 0);
        }
        else
        {
            TxtTime.text = string.Format("{0:00} : {1:00}", minute, seconds);
        }
    }
}
