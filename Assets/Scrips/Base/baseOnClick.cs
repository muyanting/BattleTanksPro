using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class baseOnClick : MonoBehaviour, IPointerClickHandler
{
    private string tag = "YellBase";
    private Ray ray;//���������������(�����������Ļλ��)
    private RaycastHit hitInfo;//��ȡ������Ϣ

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);//�ӵ�ǰ���λ�÷�������
        if (Input.GetMouseButtonDown(0))//����������
        {
            if (Physics.Raycast(ray, out hitInfo))//ʹ��Ĭ�����߳��Ⱥ�����Ĭ�ϲ���
            {
                Debug.Log("Hit--" + hitInfo.collider.gameObject.name);//��ײ�������������
                Debug.Log("base on click");
                if (hitInfo.collider.gameObject == this)
                {

                }
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(tag + "Click Cube ");

    }

    void Click()
    {
        Debug.Log("base on click");
        gameObject.SetActive(true);
    }
}
