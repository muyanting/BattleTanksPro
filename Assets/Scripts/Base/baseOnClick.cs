using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class baseOnClick : MonoBehaviour, IPointerClickHandler
{
    private string tag = "Base";
    private Ray ray;//从摄像机发出射线(根据鼠标在屏幕位置)
    private RaycastHit hitInfo;//获取射线信息

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);//从当前鼠标位置发射射线
        if (Input.GetMouseButtonDown(0))//鼠标左键按下
        {
            if (Physics.Raycast(ray, out hitInfo))//使用默认射线长度和其他默认参数
            {
                Debug.Log("Hit--" + hitInfo.collider.gameObject.name);//碰撞到的物体的名称
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
