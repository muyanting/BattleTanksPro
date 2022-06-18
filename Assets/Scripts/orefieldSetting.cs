using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orefieldSetting : MonoBehaviour
{
    //预制体
    //private Transform Ori_Orefield_transform;
    private GameObject newOrefield;
    public GameObject originOrefield;

    public static bool isMining = false;
    public static float currentPower;//当前矿源

    [Header("矿源控制")]
    public float maxPower;//最大矿源
    public float onceMiningPower;

    private void onEnable()
    {
        //maxPower = 100000;
        
        currentPower = maxPower;
        Debug.Log("here is setting and orefield currentPower is" + currentPower);
    }
    void Update()
    {
        if (currentPower <= 0)
        {
            //返回对象池
            orefieldPool.instance.ReturnPool(this.gameObject);
        }
        if (isMining && currentPower > 0)//正在被采矿并当前矿源>0
        {
            currentPower -= onceMiningPower;
        }
    }
}
