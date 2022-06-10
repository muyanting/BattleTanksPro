using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shellExpolsion : MonoBehaviour
{
    public GameObject shellExplosion;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(this.gameObject, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("shell trigger");
        GameObject.Instantiate(shellExplosion, transform.position, transform.rotation);//生成子弹爆炸效果
        GameObject.Destroy(this.gameObject);//删除子弹
        if (other.tag == "BlueTank"||other.tag=="YellowTank")
        {
            other.SendMessage("TankDamage",other.tag);
        }
        if (other.tag == "oreCar")
        {
            other.SendMessage("oreCarDamage");
        }
        if (other.tag == "barrier")
        {
            other.SendMessage("barrierDamage");
        }
    }
}
