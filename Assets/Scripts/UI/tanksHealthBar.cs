using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class tanksHealthBar : tankSetting
{
    public Image healthSlider;    

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        updateHealth();
    }

    void updateHealth()
    {
        healthSlider.fillAmount = tankCurrentHP / tankMaxHP;
    }

}
