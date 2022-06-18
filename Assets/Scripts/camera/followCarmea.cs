using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCarmea : MonoBehaviour
{
    public Transform player1;
    public Transform player2;

    private Camera followCamera;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - (player1.position - player2.position) / 2;
        followCamera = GetComponentInChildren<Camera>();
        //followCamera = gameObject.GetComponent<Camera>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (player1 == null || player2 == null) return;
        transform.position = (player1.position + player2.position) / 2 + offset;
        float distance = Vector3.Distance(player1.position, player2.position);
        if (distance <= 5f) return;

        float size = distance * 0.93f;
        followCamera.orthographicSize = size;
    }
}
