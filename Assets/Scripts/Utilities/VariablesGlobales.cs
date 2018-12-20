// Script contenant toutes les variables globales utilisées dans les différentes scènes



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VariablesGlobales {

        // Les effectifs totaux des joueurs
      public static int effectifTotal_joueur_01 = 0; 
      public static int effectifTotal_joueurs = 0; 
      public static int effectifTotal_joueur_03 = 0; 
      public static int effectifTotal_joueur_04 = 0;

    // Variable pour enregister le ID du joueur
    public static int monJoueurID;


	  // Les revenus par seconde du joueur
      public static float revenuOr_joueur_01 = 5; 

	 
        // Les revenus bonus par seconde du joueur
      public static float revenuOrBonus_joueur_01 = 0; 


	  // Les banques (or total) du joueur
      public static float banqueOr_joueur_01 = 3000; 


	  // Les quantités d'or accumulé par les joueurs depuis le début de la partie
      public static float orTotalPartie_joueur_01 = 0; 
      public static float orTotalPartie_joueur_02 = 0; 
      public static float orTotalPartie_joueur_03 = 0; 
      public static float orTotalPartie_joueur_04 = 0;
    
      // Liste des prix des batiments
      public static int[] prixBatiments = new int[10];


}