using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createOrefields : MonoBehaviour
{
    public Transform m_envBox;
    public Transform m_parent;
    public GameObject orefield;
    public static int maxOrefields = 8;
    public static  int count = 0;
    public static GameObject[] cloneOrefields = new GameObject[maxOrefields];
    public static float[] cloneCurrentPower=new float[maxOrefields];
    public static float cloneMaxPower = oreSetting.oreMaxPower;

    void Update()
    {

        if (count < maxOrefields)
        {
            //Update realtime
            if (count != 0)
            {
                for (int j = 0; j < maxOrefields; j++)
                {
                    if (cloneOrefields[j] == null)
                    {
                        count--;
                        Destroy(cloneOrefields[j]);
                    }
                }
            }
            else return;

            for (int i = 0; i < maxOrefields; i++)
            {
                Debug.Log("maxOrefiels = " + maxOrefields);
                cloneCurrentPower[i] = 50000;
                cloneOrefields[i] = GameObject.Instantiate(orefield, new Vector3(Random.Range(-40f, 114f), Random.Range(-2f, 2f), Random.Range(-65f, 55f)), Quaternion.identity);
                
                count++;


                if (cloneOrefields[i].transform.position == new Vector3(Random.Range(40f, 50f), Random.Range(0.5f, 1f), Random.Range(50f, 60f))
                    || cloneOrefields[i].transform.position == new Vector3(Random.Range(-45f, -50f), Random.Range(0.5f, 1f), Random.Range(-65f, -72f)))
                {
                    GameObject.DestroyImmediate(cloneOrefields[i]);
                    count--;
                    i--;
                }
                
            }
            

        }
    }
}
