﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyNetwork : MonoBehaviour
{
    // Start is called before the first frame update
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
