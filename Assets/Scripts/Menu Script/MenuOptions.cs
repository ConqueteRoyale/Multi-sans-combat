using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
//2018-09-15
//Kevin Langlois
//Script qui controle les options du jeu
//Source Brackeys youtube
public class MenuOptions : MonoBehaviour {

    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;

    //liste des résolutions possibles
    Resolution[] resolutions;

    private void Start()
    {
        //On crée une liste de résolution possibe
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int resolutionCourante = 0;

        //On ajoute les résolution possible à la liste
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) 
            {
                resolutionCourante = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = resolutionCourante;
        resolutionDropdown.RefreshShownValue();
    }

    //Permet de changer la résolution de l'écran
    public void ChangerResolution( int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    //Permet de changer le volume global du jeu
    public void ChangerVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    //permet de changer les graphiques du jeu
    public void ChangerGraphiques( int indexGraphiques)
    {
        QualitySettings.SetQualityLevel(indexGraphiques);
    }

    //Permet de mettre le jeu en windowed ou plein écran
    public void PleinEcran (bool plein)
    {
        Screen.fullScreen = plein;
    }
        

}
