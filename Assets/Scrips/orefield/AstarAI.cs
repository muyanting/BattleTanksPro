//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AstarAI : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class AstarAI : MonoBehaviour
{
    //Ŀ��������Ŀ��λ��
    public GameObject targetObject;
    private Vector3 targetPosition;

    private Seeker seeker;

    //�洢·��
    public Path path;
    //��ɫ�ƶ��ٶ�
    public float speed = 10.0f;
    public float turnSpeed = 5f;
    //�ж�����뺽��ľ���
    public float nextWaypointDistance = 3;
    //�Ե�ǰ�ĺ�����б��
    private int currentWaypoint = 0;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();

        //ע��ص���������Astar Path��ɺ���ô˺���
        seeker.pathCallback += OnPathComplete;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        targetPosition = targetObject.transform.position;
        Debug.Log("Ŀ��Target is here" + targetPosition);
        //��ʼѰ·
        seeker.StartPath(transform.position, targetPosition);
        if (path == null)
        {
            return;
        }
        //��ǰ�������Ŵ��ڵ���·���洢���ܵ���ʱ��·����������
        if (currentWaypoint >= path.vectorPath.Count)
        {
            Debug.Log("·����������");
            return;
        }

        Vector3 dir = (path.vectorPath[currentWaypoint + 1] - transform.position);//.normalized;
        dir *= speed * Time.fixedDeltaTime;

        //���ת��
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * speed);
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        //��ҵ�ǰλ���뵱ǰ�ĺ�������С��һ������ֵ��ת����һ�������
        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }

    }
    /// <summary>
    /// ��Ѱ·����������������
    /// </summary>
    /// <param name="p"></param>
    private void OnPathComplete(Path p)
    {
        Debug.Log("�������·��" + p.error);
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    private void OnDisable()
    {
        seeker.pathCallback -= OnPathComplete;
    }
}

