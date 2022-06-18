using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tankSetting : MonoBehaviour
{
    public float tankCurrentHP = 100;
    public float tankMaxHP = 100;

    public static float tankPrice = 8000;

    public GameObject tankExplosion;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TankDamage(string tag)
    {
        //if (this.tag != tag)

        if (tankCurrentHP <= 0)
        {
            return;
        }
        tankCurrentHP -= 20;
        //healthSlider.fillAmount = HP/tankMaxHP;
        //Debug.Log("hitted!" + tag);
        if (tankCurrentHP <= 0)
        {
            GameObject.Instantiate(tankExplosion, transform.position + Vector3.up, transform.rotation);
            GameObject.Destroy(this.gameObject);
        }

        else return;
    }
}
