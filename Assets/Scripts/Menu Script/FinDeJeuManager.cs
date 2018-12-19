using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//2018-11-09
//Kevin Langlois
//Script qui contrôle les statistiques affichées à l'écran de fin de jeu

public class FinDeJeuManager : MonoBehaviour {

    public Transform orText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        orText.GetComponent<Text>().text = VariablesGlobales.banqueOr_joueur_01.ToString();
	}
}
