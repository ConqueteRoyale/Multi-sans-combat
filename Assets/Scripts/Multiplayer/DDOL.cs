using UnityEngine;
//2018-13-21
//Kevin Langlois
//Script qui empêche la destruction de l'élément ici appliquer au player network lors du changement de scène, afin de pouvoir spawner les joueurs sur la prochaine scène
//et leur assigné une couleur
public class DDOL : MonoBehaviour {

	// Use this for initialization
	private void Awake () {

        DontDestroyOnLoad(this);
		
	}
	
	
}
