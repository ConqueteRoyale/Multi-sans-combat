using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//2018-13-21
//Kevin Langlois
//Script qui gere la création d'un salon dans la liste et qui ajoute les eventlistener au bouton 
public class PlayerLayoutGroup : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerListingPrefab;
    private GameObject PlayerListingPrefab
    {
        get { return _playerListingPrefab; }
    }

    private List<PlayerListing> _playerListings = new List<PlayerListing>();
    private List<PlayerListing> PlayerListings
    {
        get { return _playerListings; }
    }

    //Appler par photon lorsque le masterclient quitte ou switch
    private void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
        PhotonNetwork.LeaveRoom();
    }



    private void OnJoinedRoom()
    {   
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        //Change la Current Room dans la hierarchie pour se déplacer sous le lobby
        MainCanvasManager.Instance.CurrentRoomCanvas.transform.SetAsLastSibling();

        PhotonPlayer[] photonPlayers = PhotonNetwork.playerList;
        for(int i= 0; i < photonPlayers.Length; i++){

            PlayerJoinedRoom(photonPlayers[i]);
        }
    }


    //Appeler par Photon lorsque joueur rejoin la room
    private void OnPhotonPlayerConnected(PhotonPlayer photonPlayer)
    {
        PlayerJoinedRoom(photonPlayer);
    }


    //Quand un joueur quitte le lobby
    private void OnphotonPlayerDisconnected( PhotonPlayer photonPlayer)
    {
        PlayerLeftRoom(photonPlayer);
    }

    //Ajoute un joueur à la liste de joueurs présent sur la scène
    private void PlayerJoinedRoom(PhotonPlayer photonPlayer)
    {
        if (photonPlayer == null)
            return;
        PlayerLeftRoom(photonPlayer);

        GameObject playerListingObj = Instantiate(PlayerListingPrefab);
        playerListingObj.transform.SetParent(transform, false);

        PlayerListing playerListing = playerListingObj.GetComponent<PlayerListing>();
        playerListing.ApplyPhotonPlayer(photonPlayer);

        PlayerListings.Add(playerListing);

    }

    //Retire un joueur de la liste des joueurs de la room lorsque celui-ci quitte
    private void PlayerLeftRoom(PhotonPlayer photonplayer)
    {


        int index = PlayerListings.FindIndex(x => x.PhotonPlayer == photonplayer);
        if(index != -1)
        {
            Destroy(PlayerListings[index].gameObject);
            PlayerListings.RemoveAt(index);
        }


    }

    //Permet de changer la visibilité de la room créer
    public void OnClickRoomState()
    {
        if (!PhotonNetwork.isMasterClient)
            return;

        PhotonNetwork.room.IsOpen = !PhotonNetwork.room.IsOpen;
        PhotonNetwork.room.IsVisible = PhotonNetwork.room.IsOpen;

    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

}
