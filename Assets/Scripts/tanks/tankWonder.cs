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
        //����Ѳ��Э��
        StartCoroutine("AutoTankWander", 0.1f);

        //������ѰЭ��
        StartCoroutine("FindTargetWithDelay", 1.0f);

    }

  
    void Update()
    {

    }
    
    //���Ѳ��Э��
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


    //��Ұ��Χ��׷��
    //�����ѰЭ�̣�ÿ��delayʱ����һ����Ұ��Χ
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
        //�ҵ�Ŀ��
        Collider[] targetsInView = Physics.OverlapSphere(this.transform.position, viewRadius, targetMask);

        for(int i = 0; i < targetsInView.Length; i++)
        {
            //�����һ�ε��Ѳ鵽��target
            visableTargets.Clear();

            //��ȡtarget��position
            Transform target = targetsInView[i].transform;

            //��������dirTarget������Ϊ��ҵ�target
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            //����нǣ���1/2�Ƕȵķ�Χ�ڣ������������Ұ��Χ��
            if (Vector3.Angle(transform.forward, dirToTarget) < (viewAngle / 2))
            {
                float dstTarget = Vector3.Distance(this.transform.position, target.position);
                //�������target�������ߣ�������߾��������ϰ�����target���ж�Ϊ���ɼ�
                if (!Physics.Raycast(this.transform.position, dirToTarget, dstTarget, obstacleMask))
                {
                    //���û���ϰ���Ѹ�������ӵ��ɼ�Ŀ����������
                    visableTargets.Add(target);

                }
            }
        }

        //���Ŀ�겻Ϊ0
        if (this.visableTargets.Count > 0 && visableTargets[0])
        {

            float[] dst = new float[this.visableTargets.Count];

            for (int i = 0; i < this.visableTargets.Count && visableTargets[i] != null; i++)
            {
                dst[i] = Vector3.Distance(this.transform.position, this.visableTargets[i].position);
                //print("dst[" + i + "]" + dst[i] + "\t");
            }

            //�ҵ������Ŀ�����׷��
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
