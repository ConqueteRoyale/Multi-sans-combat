using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//2018-13-21
//Kevin Langlois
//Script qui permet la création d'une room que les autres joueurs peuvent rejoindre
public class CreateRoom : MonoBehaviour
{
    [SerializeField]
    private  Text _roomName;
    private Text RoomName
    {
        get{ return _roomName; }
    
    }

    public void OnClick_CreateRoom(){

        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, MaxPlayers = 4};

        if(PhotonNetwork.CreateRoom(RoomName.text, roomOptions, TypedLobby.Default)){
            
            print("crate room successfully sent");
        }
        else{
            print("create room failed to send.");
        }
    }

    private void OnPhotonCreateRoomFailed(object[] codeAndMessage){

        print("create room failed: " + codeAndMessage[1]);
    }

    private void OnCreatedRoom(){
        print("Room created sucessfully.");
    }
}
