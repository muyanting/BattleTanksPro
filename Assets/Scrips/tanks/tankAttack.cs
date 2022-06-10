using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tankAttack : MonoBehaviour
{
    public GameObject shell;
    public float fireSpeed = 10;
    public KeyCode fireKey = KeyCode.Space;

    private Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        firePoint = transform.Find("FirePoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(fireKey))
        {
            GameObject go = GameObject.Instantiate(shell, firePoint.position, firePoint.rotation);
            go.GetComponent<Rigidbody>().velocity = go.transform.forward * fireSpeed;

        }
    }

    void autoAttackEnemy(Transform target)
    {
        GameObject go = GameObject.Instantiate(shell, firePoint.position, firePoint.rotation);
        go.GetComponent<Rigidbody>().velocity = go.transform.forward * fireSpeed;
        go.transform.Rotate(target.position);
        Debug.Log(this.gameObject+" is shotting");
    }
}
