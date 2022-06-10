using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tanMovement : MonoBehaviour
{
    public float moveSpeed = 10;
    public float rotateSpeed = 10;
    public int Player = 1;

    private Rigidbody rigidbody;
    private Vector3 move = new Vector3();


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float v = Input.GetAxisRaw("Vertical"+Player);
        float h = Input.GetAxisRaw("Horizontal"+Player);
        move.Normalize();

        rigidbody.velocity = transform.forward * v * moveSpeed;
        rigidbody.angularVelocity = transform.up * h * rotateSpeed;

    }
}
