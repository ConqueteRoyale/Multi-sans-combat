using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnexion : NetworkBehaviour {

    public GameObject unit;
    public Vector3 newScale = new Vector3(0.1f, 0.1f, 0.1f);

	// Use this for initialization
	void Start () {

        // Condition qui vérifie si cet objet appartient au joueur qui se connecte
        if (isLocalPlayer == false){

            // Cet objet appartient à un autre joueur
            return;
        }

        // On appel la fonction qui spawn une unité
        CmdSpawnUnit();
		
	}
	
	// LA fonction update, quand on gère des systèmes de multijoueur, est appelé sur CHAQUE ordinateur, que l'objet leur appartient ou pas.
	void Update () {
		
	}

    [Command]
    public void CmdSpawnUnit()
    {

        // L'objet est copié sur le serveur
        GameObject CopiedUnit = Instantiate(unit);

        //Maintenant que l'objet existe sur le serveur il faut le propagé a tous et ajouter ses composants Network
        NetworkServer.SpawnWithClientAuthority(CopiedUnit, connectionToClient);

        // Placer mon nouvel objet à l'endroit approprié
        CopiedUnit.transform.position = unit.transform.position;
        CopiedUnit.transform.localScale = newScale;

    }
}
