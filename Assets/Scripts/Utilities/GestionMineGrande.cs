using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GestionMineGrande : MonoBehaviour {

	public Text revenuJoueur;

	// Use this for initialization
	void Start () {
           StartCoroutine(ActiverMine());
	}
	IEnumerator ActiverMine() {
        yield return new WaitForSeconds(1f);
		 VariablesGlobales.revenuOrBonus_joueur_01 += 15;

	}
}

