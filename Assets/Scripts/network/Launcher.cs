using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("welcome " + base.name);

        //设置房间
        PhotonNetwork.JoinOrCreateRoom("BattleTanksProRoom-01", new Photon.Realtime.RoomOptions { MaxPlayers = 2 }, default);

    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        PhotonNetwork.Instantiate("BlueTank", new Vector3(49, 1f, 65), Quaternion.identity, 0);
    }
}
