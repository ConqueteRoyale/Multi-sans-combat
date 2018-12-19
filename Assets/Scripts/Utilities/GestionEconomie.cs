// SCRIPT DE GESTION DE L'ÉCONOMIE DES JOUEURS (OR)


using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GestionEconomie : MonoBehaviour {

	public Text revenuJoueur;

	public Text banqueJoueur; // L'or total du joueur à un instant t
	public Text nombresUnites; // L'effectif total du joueur à un instant t
	public float calculRevenuAvecBonus;

	// Appelle la fonction économie toutes les 0.5s au début de la partie
	void Start () {

        
        // Au début de la partie, on définit les prix des batiments
        VariablesGlobales.prixBatiments[0] = 1500; // La mine
        VariablesGlobales.prixBatiments[1] = 1700; // La grande mine
        VariablesGlobales.prixBatiments[2] = 1500; // La caserne des lanciers
        VariablesGlobales.prixBatiments[3] = 1500; // La caserne des chevaliers
        VariablesGlobales.prixBatiments[4] = 7500; // La caserne des géants
        
        InvokeRepeating("economie", 0f, 0.5f);
	}
	

	// Met à jour la banque des joueurs et change l'UI en conséquence
	void economie () {
		VariablesGlobales.banqueOr_joueur_01 += (VariablesGlobales.revenuOr_joueur_01 / 2);
		VariablesGlobales.orTotalPartie_joueur_01 += (VariablesGlobales.revenuOr_joueur_01 / 2);
		banqueJoueur.text = Mathf.Round(VariablesGlobales.banqueOr_joueur_01).ToString();
		nombresUnites.text = VariablesGlobales.effectifTotal_joueur_01.ToString();

		calculRevenuAvecBonus = VariablesGlobales.revenuOr_joueur_01*(VariablesGlobales.revenuOrBonus_joueur_01/100) + VariablesGlobales.revenuOr_joueur_01;
		revenuJoueur.text = calculRevenuAvecBonus.ToString();


	}
}
