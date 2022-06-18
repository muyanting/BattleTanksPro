using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryOnTime : MonoBehaviour
{
    public float destoryTime = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(this.gameObject, destoryTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
