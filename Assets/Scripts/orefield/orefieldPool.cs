using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orefieldPool : MonoBehaviour
{
    //对象池准备
    public static orefieldPool instance;//单例模式

    public GameObject prefab_Orefield;
    public Transform center;

    //public GameObject originOrefield;

    public static int maxOrefieldCount=10;//最多生成矿源数量

    public static Queue<GameObject> availableOrefields = new Queue<GameObject>();//池



    private void Awake()
    {
        instance = this;

        //初始化对象池
        FillPool();

        for (int i = 0; i < maxOrefieldCount; i++)
        {
            GetFromPool();
        }

    }
    public void FillPool()
    {
        for (int i = 0; i < maxOrefieldCount; i++)
        {
            GameObject _Orefield = GameObject.Instantiate(prefab_Orefield, new Vector3(Random.Range(-40f, 114f), Random.Range(-2f, 2f), Random.Range(-65f, 55f)), Quaternion.identity);//创建新矿源
            _Orefield.transform.SetParent(this.transform);                                                                                                                                                              //_Orefield.transform.SetParent(this.transform);

            //Vector3 pos = center.transform.position;//获取中心位置
            //pos.z = Random.Range(46f, -46f);//在随机范围内生成

            //_Orefield.transform.position = pos;//赋值
            //基地范围不允许生成
            if (_Orefield.transform.position == new Vector3(Random.Range(40f, 50f), Random.Range(0.5f, 1f), Random.Range(50f, 60f))
                    || _Orefield.transform.position == new Vector3(Random.Range(-45f, -50f), Random.Range(0.5f, 1f), Random.Range(-65f, -72f)))
            {
                GameObject.DestroyImmediate(_Orefield);
                i--;
                continue;
            }

            //取消启用，返回对象池
            ReturnPool(_Orefield);
        }
    }
    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive(false);//不显示
        availableOrefields.Enqueue(gameObject);//放回对象池
    }

    public void GetFromPool()
    {
        GameObject outOrefield = availableOrefields.Dequeue();//从对象池中取出
        outOrefield.SetActive(true);//显示，运行OnEnable
        Debug.Log("ghhhhhhhhhhhhhhhhhhere"+outOrefield.transform);
    }




}
