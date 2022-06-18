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


    //Ѱ·������
    void BackToBase()
    {
        oreCar.destination = Base.transform.position;
        //�������
        if (oreCar.GetComponent<NavMeshAgent>().path.corners.Length == 1)
        {
            Debug.Log("Base");
            StartCoroutine("FindOrefieldWithDelay");
            isOrefield = false;
            isBase = true;
        }

    }
    //�ص�����Э��
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

    //�ҿ�ԴЭ��
    IEnumerator FindOrefieldWithDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(10.0f);
            Debug.Log("FindOrefieldWithDelay is waitting 2 sec");
            FindVisableOrefield();
            yield return new WaitForSeconds(10.0f);
            StartCoroutine("OrecarBackToBase", 2.0f);
            //�������
            if (markOrefieldIndex != -1)
            {
                Debug.Log(" ore index is " + markOrefieldIndex + "  yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy");
                baseSetting.fund += OnceGetOre;
                Debug.Log("now Base funddddddddddddddd is " + baseSetting.fund);
            }
        }
    }



    //Ѱ·����Դ
    void FindVisableOrefield()
    {
        StopCoroutine("OrecarBackToBase");
        //�ҵ�Ŀ��
        Collider[] targetsInView = Physics.OverlapSphere(this.transform.position, 400, orefieldMask);

        for (int i = 0; i < targetsInView.Length; i++)
        {
            //�����һ�ε��Ѳ鵽��target
            visableOrefields.Clear();

            //��ȡtarget��position
            Transform target = targetsInView[i].transform;

            //��������dirTarget������Ϊ��ҵ�target
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            //����нǣ���1/2�Ƕȵķ�Χ�ڣ������������Ұ��Χ��
            if (Vector3.Angle(transform.forward, dirToTarget) < (viewAngle / 2))
            {
                float dstTarget = Vector3.Distance(this.transform.position, target.position);
                //�������target�������ߣ�������߾��������ϰ�����target���ж�Ϊ���ɼ�
                //if (!Physics.Raycast(this.transform.position, dirToTarget, dstTarget, obstacleMask))
                //{
                //���û���ϰ���Ѹ�������ӵ��ɼ�Ŀ����������
                visableOrefields.Add(target);

                //}
            }
        }

        //���Ŀ�겻Ϊ0,���浽����
        if (this.visableOrefields.Count > 0 && visableOrefields[0])
        {
            float[] dst = new float[this.visableOrefields.Count];

            for (int i = 0; i < this.visableOrefields.Count && visableOrefields[i] != null; i++)
            {
                dst[i] = Vector3.Distance(this.transform.position, this.visableOrefields[i].position);
                //print("orefield dst[" + i + "]" + dst[i] + "\t");
            }

            //�������飬�ҵ������Ŀ��
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
            //�ִ�����Ŀ�Դ
            if (oreCar != null && visableOrefields[minIndex] != null && orefieldSetting.currentPower > 0)
            {
                oreCar.destination = visableOrefields[minIndex].position;

            }
            else return;

            for (int i = 0; i < orefieldPool.availableOrefields.Count; i++)
            {
                //�ж��Ƿ񵽴�Ŀ�ĵ�
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

