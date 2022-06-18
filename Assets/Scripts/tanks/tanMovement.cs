using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class tanMovement : MonoBehaviourPun
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

    //// Update is called once per frame
    //void Update()
    //{
    //    //如果不是玩家自己，取消控制
    //    if (!photonView.IsMine && PhotonNetwork.IsConnected)
    //    {
    //        return;
    //    }
    //}

    private void FixedUpdate()
    {
        //如果不是玩家自己，取消控制
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
        {
            return;
        }
        float v = Input.GetAxisRaw("Vertical"+Player);
        float h = Input.GetAxisRaw("Horizontal"+Player);
        move.Normalize();

        rigidbody.velocity = transform.forward * v * moveSpeed;
        rigidbody.angularVelocity = transform.up * h * rotateSpeed;

    }
}
