using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//2018-09-15
//Kevin Langlois
//Script qui controle les mouvements de caméras dans le menu principal

public class ControlCameraMenu : MonoBehaviour {

    public Transform positionActuelle;  //position actuelle de la caméra 
    public float vitesseDeplacement;    //vitesse de déplacement de la caméra d'une position à l'autre 
    public Vector3 dernierePosition;    //dernière position de la caméra

    //On prend la position de la caméra actuelle ainsi que son angle 
    private void Update()
    {
        gameObject.transform.position = Vector3.Lerp(transform.position, positionActuelle.position, vitesseDeplacement);
        gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, positionActuelle.rotation, vitesseDeplacement);

        float velocity = Vector3.Magnitude(transform.position - dernierePosition);

        dernierePosition = transform.position;
    }

    //On remplace la position et l'angle de la caméra par l'un des objet dans la scène
    public void ChangerPosition(Transform nouvellePosition)
    {
        positionActuelle = nouvellePosition;
    }

}
