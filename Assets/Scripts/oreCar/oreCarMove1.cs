using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class oreCarMove1 : MonoBehaviour
{
    //public GameObject target;
    private NavMeshAgent oreCar;
    public GameObject Base;
    
    private float viewAngle=360;

    public LayerMask orefieldMask;
    public LayerMask obstacleMask;

    public static int markOrefieldIndex = -1;
    private float OnceGetOre = 10000;

    public List<Transform> visableOrefields = new List<Transform>();

    private bool isBase;
    private bool isOrefield;


    //寻路到基地
    void BackToBase()
    {
        oreCar.destination = Base.transform.position;
        //到达基地
        if (oreCar.GetComponent<NavMeshAgent>().path.corners.Length == 1)
        {
            Debug.Log("Base");
            StartCoroutine("FindOrefieldWithDelay");
            isOrefield = false;
            isBase = true;
        }

    }
    //回到基地协程
    IEnumerator OrecarBackToBase(float delay)
    {
        yield return new WaitForSeconds(delay);
        BackToBase();
        Debug.Log("is waitting "+delay+" sec"); 
    }


    void Start()
    {
        isBase = true;
        isOrefield = false;
        oreCar = this.GetComponent<NavMeshAgent>();
        StartCoroutine("OrecarBackToBase", 0.0f);
        //Debug.Log("orefield : " + oreSetting.oreMaxPower);
        //StartCoroutine("oreCarMining");
        //StartCoroutine(oreCarMining());
    }


    void Update()
    {
        if (isBase)
        {
            isOrefield = false;
        }
        if (isOrefield) isBase = true;
    }
    IEnumerator oreCarMining()
    {
        while (isBase)
        {
            FindVisableOrefield();
        }
        yield return new WaitForSeconds(8);
        while (isOrefield)
        {
            BackToBase();
        }
        yield return new WaitForSeconds(4);
    }

    //找矿源协程
    IEnumerator FindOrefieldWithDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(10.0f);
            Debug.Log("FindOrefieldWithDelay is waitting 2 sec");
            FindVisableOrefield();
            yield return new WaitForSeconds(10.0f);
            StartCoroutine("OrecarBackToBase", 2.0f);
            //到达基地
            if (markOrefieldIndex != -1)
            {
                Debug.Log(" ore index is " + markOrefieldIndex + "  yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy");
                baseSetting.fund += OnceGetOre;
                Debug.Log("now Base funddddddddddddddd is " + baseSetting.fund);
            }
        }
    }



    //寻路到矿源
    void FindVisableOrefield()
    {
        StopCoroutine("OrecarBackToBase");
        //找到目标
        Collider[] targetsInView = Physics.OverlapSphere(this.transform.position, 400, orefieldMask);

        for (int i = 0; i < targetsInView.Length; i++)
        {
            //清空上一次的搜查到的target
            visableOrefields.Clear();

            //获取target的position
            Transform target = targetsInView[i].transform;

            //构造向量dirTarget，方向为玩家到target
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            //计算夹角，在1/2角度的范围内，该物体就在视野范围内
            if (Vector3.Angle(transform.forward, dirToTarget) < (viewAngle / 2))
            {
                float dstTarget = Vector3.Distance(this.transform.position, target.position);
                //从玩家向target发射射线，如果射线距离内有障碍物，则该target被判断为不可见
                //if (!Physics.Raycast(this.transform.position, dirToTarget, dstTarget, obstacleMask))
                //{
                //如果没有障碍物，把该物体添加到可见目标数组里面
                visableOrefields.Add(target);

                //}
            }
        }

        //如果目标不为0,保存到数组
        if (this.visableOrefields.Count > 0 && visableOrefields[0])
        {
            float[] dst = new float[this.visableOrefields.Count];

            for (int i = 0; i < this.visableOrefields.Count && visableOrefields[i] != null; i++)
            {
                dst[i] = Vector3.Distance(this.transform.position, this.visableOrefields[i].position);
                //print("orefield dst[" + i + "]" + dst[i] + "\t");
            }

            //遍历数组，找到最近的目标
            int minIndex = 0;
            for (int i = 0; i < dst.Length; i++)
            {
                float _min = dst[0];
                if (_min > dst[i])
                {
                    minIndex = i;
                    _min = dst[i];
                }
            }
            //抵达最近的矿源
            if (oreCar != null && visableOrefields[minIndex] != null && orefieldSetting.currentPower > 0)
            {
                oreCar.destination = visableOrefields[minIndex].position;

            }
            else return;

            for (int i = 0; i < orefieldPool.availableOrefields.Count; i++)
            {
                //判断是否到达目的地
                if (oreCar.GetComponent<NavMeshAgent>().path.corners.Length == 1)
                {
                        orefieldSetting.isMining = true;
                    //        if (createOrefields.cloneCurrentPower[i] > 0)
                    //        {
                    //            createOrefields.cloneCurrentPower[i] -= OnceGetOre;
                    //        }
                    //        markOrefieldIndex = i;
                    //        Debug.Log("this markIndex" + " [ " + i + " ] 's pppppppppower is " + createOrefields.cloneCurrentPower[i]);

                }
            }
            //Debug.Log("mark index = " + markOrefieldIndex);
            isBase = false;
            isOrefield = true;
        }
    }

}

