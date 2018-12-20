using UnityEngine;
using UnityEngine.SceneManagement;
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

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        if(PlayersInGame == 1)
        {
            PhotonNetwork.Instantiate("Main Castle", new Vector3(-19.83f, -0.51f, -20.66f), Quaternion.identity, 0);
        }
        else if(PlayersInGame ==2)
        {
            PhotonNetwork.Instantiate("Main Castle", new Vector3(16f, -0.51f, -15.75f), Quaternion.identity, 0);
        }
        else if(PlayersInGame == 3)
        {
            PhotonNetwork.Instantiate("Main Castle", new Vector3(15.16f, -0.51f, 16.38f), Quaternion.identity, 0);
        }
        else
        {
            PhotonNetwork.Instantiate("Main Castle", new Vector3(-16.57f, -0.51f, 15.91f), Quaternion.identity, 0);
        }
    }
}
