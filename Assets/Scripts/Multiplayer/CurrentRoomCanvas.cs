using UnityEngine;
//2018-13-21
//Kevin Langlois
//Script qui gère le lancement d'une partie en chargeant la scene multijoueur pour tous les joueurs présent dans la room au même moment

public class CurrentRoomCanvas : MonoBehaviour
{
  public void OnClickStartSync()
    {
        if (!PhotonNetwork.isMasterClient)
            return;

        PhotonNetwork.LoadLevel(3);
        
    }

    public void OnClickStartDelayed()
    {
        if (!PhotonNetwork.isMasterClient)
            return;

        PhotonNetwork.room.IsOpen = false;
        PhotonNetwork.room.IsVisible = false;
        PhotonNetwork.LoadLevel(3);
    }
}
