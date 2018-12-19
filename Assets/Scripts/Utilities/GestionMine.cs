using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GestionMine : MonoBehaviour {


	// Use this for initialization
	void Start () {
           StartCoroutine(ActiverMine());
	}
	
	IEnumerator ActiverMine() {
        yield return new WaitForSeconds(10f);
		
		VariablesGlobales.revenuOr_joueur_01 += 20;
	}
}

