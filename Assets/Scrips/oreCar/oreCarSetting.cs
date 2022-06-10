using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oreCarSetting : MonoBehaviour
{
    public static float oreCarPrice = 8000;
    public  float oreCarHP = 200;
    public GameObject carExplosion;

    void oreCarDamage()
    {
        if (oreCarHP <= 0) return;
        oreCarHP -= Random.Range(10, 20);
        if (oreCarHP <= 0)
        {
            GameObject.Instantiate(carExplosion, transform.position + Vector3.up, transform.rotation);
            GameObject.Destroy(this.gameObject);
        }
    }
}
