using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//2018-10-13
//Kevin Langlois
//Script qui controle le debut d'une partie ainsi que la fin de la partie
public class MenuPrincipal : MonoBehaviour
{

    public GameObject victoireTitre;
    public GameObject defaiteTitre;
    public int countJoueur2;
    public Text effectifFinal;

    public void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

     
        //affiche le nombre d'unités du joueur sur l'écran de fin de jeu
        if (sceneName == "Scene_FinJeu")
        {
            effectifFinal.text = "Effectif final: " + VariablesGlobales.effectifTotal_joueur_01.ToString();
           
        }
    }
    //permet de charger la scene de jeu solo
    public void JouerSolo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Permet de charger la scene de jeu multijoueur
    public void JouerMulti()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    //Permet de retourner au menu principal
    public void Retour()
    {
        SceneManager.LoadScene(0);
    }

    //Permet de quitter l'application 
    public void QuitterJeu()
    {
        Debug.Log("Quitter");
        Application.Quit();
    }
}
