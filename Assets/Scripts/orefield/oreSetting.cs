using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oreSetting : MonoBehaviour
{
    public static float oreMaxPower = 50000;
    
    

    // Start is called before the first frame update
    void Start()
    {
        updateOrefieldState();
    }

    // Update is called once per frame
    void Update()
    {
        updateOrefieldState();
        CloneOrefieldsSetting();
    }

    void updateOrefieldState()
    {
        if (oreMaxPower <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    void CloneOrefieldsSetting()
    {
        for(int i = 0; i < createOrefields.maxOrefields; i++)
        {
            //    if (createOrefields.cloneCurrentPower[i] <= 0)
            //    {
            //        Debug.Log("wwwwwwwwwwwwwwwwwwwww" + createOrefields.cloneCurrentPower[i]);
            //        createOrefields.cloneOrefields[i] = null;
            //        //GameObject.Destroy(createOrefields.cloneOrefields[i]);
            //        createOrefields.count-=1;

            //    }
        }
    }

    
}

