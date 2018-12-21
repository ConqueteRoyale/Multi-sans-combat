using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//2018-13-21
//Kevin Langlois
//Script qui gere le lobby canvas
public class LobbyCanvas : MonoBehaviour
{
    [SerializeField]
    private RoomLayoutAAAA _roomLayoutGroup;
    private RoomLayoutAAAA roomLayoutGroup
    {
        get { return _roomLayoutGroup; }
    }

    public void OnClickJoinRoom(string roomName)
    {

        if (PhotonNetwork.JoinRoom(roomName)){

        }
        else
        {
            print("Join room failed");
        }
    }
}
