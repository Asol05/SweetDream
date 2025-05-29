using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpBar : MonoBehaviour
{
    public Slider Hpbar;
    public TextMeshProUGUI TxtHp;
    public Image image;
    private float PlayerHp;
    private float MaxHp;

    public PlayerController Player;

    // Start is called before the first frame update
    void Start()
    {
        MaxHp = Player.GetHP();
        Hpbar.value = PlayerHp/MaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (Hpbar.value <= 0.25f)
        {
            image.color = Color.red;
        }
        else if(Hpbar.value <= 0.70f)
        {
            image.color = Color.yellow;
        }
        else if (Hpbar.value > 0.70f)
        {
            image.color = Color.green;
        }
        PlayerHp = Player.GetHP();
        TxtHp.text = "HP : " + PlayerHp.ToString();
        Hpbar.value = PlayerHp / MaxHp;
    }
}
