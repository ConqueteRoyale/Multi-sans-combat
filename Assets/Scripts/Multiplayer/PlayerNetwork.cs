using UnityEngine;
using UnityEngine.SceneManagement;
//2018-13-21
//Kevin Langlois
//Script principal qui sert à la création du joueur. On y attribue un nom et un # au hasard et on spawn le joueur sur la prochaine scene selon des coordonnées spécifiques
public class PlayerNetwork : MonoBehaviour {

	public static PlayerNetwork Instance;
	public string PlayerName { get; private set; }
    PhotonView PhotonView;
    private int PlayersInGame = 0;

	// Use this for initialization
	private void Awake () {
		
		Instance = this;
        PhotonView = GetComponent<PhotonView>();

		PlayerName = "Joueur#" + Random.Range(1000, 9999);


        SceneManager.sceneLoaded += OnSceneFinishedLoading;

        
    }
	
    //Contrôle ce qui est spawner dans chacune des versions selon s'il s'agit du master client ou de simple client
    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Scene_Multijoueur")
        {
            if (PhotonNetwork.isMasterClient)
                MasterLoadedGame();
            else
                NonMasterLoadedGame();
        }
    }

    private void MasterLoadedGame()
    {
        PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient);
        PhotonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);   
    }

    private void NonMasterLoadedGame()
    {
        PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient);
    }
	
    [PunRPC]
    private void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel(3);
    }

    //Affiche dans la console si tous les joueurs présents dans la rooms sont créés sur la scene suivante
    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        PlayersInGame++;
        if(PlayersInGame == PhotonNetwork.playerList.Length)
        {
            print("All players are in the game scene.");

            PhotonView.RPC("RPC_CreatePlayer", PhotonTargets.All);
        }
    } 

    //Permet de spawner les joueurs sur la scene. Ils sont spawnés selon le nb de joueurs présent à des endroits différents sur la scene
    [PunRPC]
    private void RPC_CreatePlayer()
    {
        if(PlayersInGame == 1)
        {
            PhotonNetwork.Instantiate("Main Castle", new Vector3(-19.83f, -0.47f, -20.66f), Quaternion.identity, 0);
        }
        else if(PlayersInGame ==2)
        {
            PhotonNetwork.Instantiate("Main Castle", new Vector3(16f, -0.47f, -15.75f), Quaternion.identity, 0);
        }
        else if(PlayersInGame == 3)
        {
            PhotonNetwork.Instantiate("Main Castle", new Vector3(15.16f, -0.47f, 16.38f), Quaternion.identity, 0);
        }
        else
        {
            PhotonNetwork.Instantiate("Main Castle", new Vector3(-16.57f, -0.47f, 15.91f), Quaternion.identity, 0);
        }
    }
}
