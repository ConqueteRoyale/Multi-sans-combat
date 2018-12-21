using UnityEngine;
using UnityEngine.UI;
//2018-13-21
//Kevin Langlois
//Script qui met le joueur créer avec son # dans la liste de joueurs
public class PlayerListing : MonoBehaviour
{
    
    public PhotonPlayer PhotonPlayer { get; private set; }

    [SerializeField]
    private Text _playerName;
    private Text PlayerName
    {
        get { return _playerName; }
    }

    public void ApplyPhotonPlayer(PhotonPlayer photonPlayer)
    {
        PhotonPlayer = photonPlayer;
        PlayerName.text = photonPlayer.NickName;
    }


}
