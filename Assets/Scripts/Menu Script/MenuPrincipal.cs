using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//2018-10-13
//Kevin Langlois
//Script qui controle le debut d'une partie 
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

     

        if (sceneName == "Scene_FinJeu")
        {
            effectifFinal.text = "Effectif final: " + VariablesGlobales.effectifTotal_joueur_01.ToString();
           
        }
    }
    public void JouerSolo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void JouerMulti()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

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
