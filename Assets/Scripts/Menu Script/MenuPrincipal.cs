using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//2018-10-13
//Kevin Langlois
//Script qui controle le debut d'une partie 
public class MenuPrincipal : MonoBehaviour {

	public void Jouer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Permet de quitter l'application 
    public void QuitterJeu()
    {
        Debug.Log("Quitter");
        Application.Quit();
    }
}
