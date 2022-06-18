using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;
using System;


public class tankWonder : MonoBehaviour
{
    private NavMeshAgent tanks;
    public GameObject wander;
    private int index=0;

    public Transform[] wanderPoints;


    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visableTargets = new List<Transform>();



    void Start()
    {


        wanderPoints = wander.GetComponentsInChildren<Transform>();
        tanks = this.GetComponent<NavMeshAgent>();
        //启动巡查协程
        StartCoroutine("AutoTankWander", 0.1f);

        //启动搜寻协程
        StartCoroutine("FindTargetWithDelay", 1.0f);

    }

  
    void Update()
    {

    }
    
    //添加巡查协程
    IEnumerator AutoTankWander(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            tankWander();
        }
    }

    void tankWander()
    {
        if (tanks != null && wanderPoints != null)
        {
            tanks.destination = wanderPoints[index].position;
        }
        if (!tanks.pathPending && tanks.remainingDistance < 0.5f)
        {
            tanks.destination = wanderPoints[(index + 1) % wanderPoints.Length].position;
            index = (index + 1) % wanderPoints.Length;
            Debug.Log("index = " + index);

        }
    }


    //视野范围内追击
    //添加搜寻协程，每隔delay时间检查一次视野范围
    IEnumerator FindTargetWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisableTagerts();
        }
    }

    void FindVisableTagerts()
    {
        //找到目标
        Collider[] targetsInView = Physics.OverlapSphere(this.transform.position, viewRadius, targetMask);

        for(int i = 0; i < targetsInView.Length; i++)
        {
            //清空上一次的搜查到的target
            visableTargets.Clear();

            //获取target的position
            Transform target = targetsInView[i].transform;

            //构造向量dirTarget，方向为玩家到target
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            //计算夹角，在1/2角度的范围内，该物体就在视野范围内
            if (Vector3.Angle(transform.forward, dirToTarget) < (viewAngle / 2))
            {
                float dstTarget = Vector3.Distance(this.transform.position, target.position);
                //从玩家向target发射射线，如果射线距离内有障碍物，则该target被判断为不可见
                if (!Physics.Raycast(this.transform.position, dirToTarget, dstTarget, obstacleMask))
                {
                    //如果没有障碍物，把该物体添加到可见目标数组里面
                    visableTargets.Add(target);

                }
            }
        }

        //如果目标不为0
        if (this.visableTargets.Count > 0 && visableTargets[0])
        {

            float[] dst = new float[this.visableTargets.Count];

            for (int i = 0; i < this.visableTargets.Count && visableTargets[i] != null; i++)
            {
                dst[i] = Vector3.Distance(this.transform.position, this.visableTargets[i].position);
                //print("dst[" + i + "]" + dst[i] + "\t");
            }

            //找到最近的目标进行追击
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
            StopCoroutine("AutoTankWander");

            tanks.destination = visableTargets[minIndex].position;
            this.gameObject.SendMessage("autoAttackEnemy", visableTargets[minIndex]);

            if (visableTargets.Count <= 0)
            {
                StartCoroutine("AutoTankWander",0.1f);
            }

        }
        else
        {
            StartCoroutine("AutoTankWander",0.1f);
        }

    }

}
