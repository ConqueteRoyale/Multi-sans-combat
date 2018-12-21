using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//2018-13-21
//Kevin Langlois
//Script qui gere la connection des joueurs au serveur PHOTON et qui créée le lobby lorsque les joueurs se connectent au master 
public class LobbyNetwork : MonoBehaviour
{
    
    private void Start()
    {
        print("Connecting to server. . .");
        PhotonNetwork.ConnectUsingSettings("0.0.0");
    }

    private void OnConnectedToMaster()
    {
        print("Connected to master.");
        PhotonNetwork.automaticallySyncScene = false;
        PhotonNetwork.playerName = PlayerNetwork.Instance.PlayerName;
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }


    private void OnJoinedLobby()
    {
        print("Joined Lobby");
        if(!PhotonNetwork.inRoom)
        MainCanvasManager.Instance.LobbyCanvas.transform.SetAsLastSibling();
    }
}
