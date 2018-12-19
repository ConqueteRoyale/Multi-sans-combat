// Selection d'unite Syteme de base de chaque RTS
// Par Nguyen Hoai Nguyen (12-11-2018), Modifie sur le code de base de Jeff Zimmer
//Kevin Langlois Sélection des unités à l'aide de UI
// https://hyunkell.com/blog/rts-style-unit-selection-in-unity-5/

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;

public class SelectionUnit : MonoBehaviour
{
    [Header("Bouton du UI")]
    public Button btnSelectionChevalier;
    public Button btnSelectionArcher;
    public Button btnSelectionLancier;
    public Button btnSelectionTous;

    [SerializeField]
    private KeyCode selectChevalier = KeyCode.Alpha7;
    private KeyCode selectArcher = KeyCode.Alpha5;
    private KeyCode selectLancier = KeyCode.Alpha6;
    private KeyCode selectTous = KeyCode.Alpha8;


    //Kevin Langlois Les boutons du UI permettent de faire la sélection des différents unités par types
    private void Start()
    {
        //Button btn5 = btnSelectionArcher.GetComponent<Button>();
        Button btn6 = btnSelectionLancier.GetComponent<Button>();
        Button btn7 = btnSelectionChevalier.GetComponent<Button>();
        Button btn8 = btnSelectionTous.GetComponent<Button>();

        //btn5.onClick.AddListener(delegate { GestionSelectionUnitBtn("archer"); });
        btn6.onClick.AddListener(delegate { GestionSelectionUnitBtn("lancier"); });
        btn7.onClick.AddListener(delegate { GestionSelectionUnitBtn("chevalier"); });
        btn8.onClick.AddListener(delegate { GestionSelectionUnitBtn("tous"); });
    }


    void Update()
    {

        GestionSelectionKey();
    }

    //Kevin Langlois Permet la sélection des différents unités à l'aide des touches alphanumérique 6 7 8 9
    private void GestionSelectionKey()
    {
        if (Input.GetKeyDown(selectArcher))
        {
            foreach (var selectableObject in FindObjectsOfType<SelectableUnit>())
            {
                if (selectableObject.unitClassName == "archer")
                {
                    /*if (selectableObject.selectionCircle == null)
                    {
                        selectableObject.selectionCircle = Instantiate(selectionCirclePrefab);
                        selectableObject.selectionCircle.transform.SetParent(selectableObject.transform, false);
                        selectableObject.selectionCircle.transform.eulerAngles = new Vector3(90, 0, 0);

                        // Associé un tag Friendly au personnage sélectionné
                        selectableObject.transform.tag = "Friendly";

                        // Change la grosseur du cercle
                        leProjecteur = selectableObject.selectionCircle.GetComponent<Projector>();
                        leProjecteur.orthographicSize = newScale;

                    }*/
                }
            }
        }

        if (Input.GetKeyDown(selectChevalier))
        {
            foreach (var selectableObject in FindObjectsOfType<SelectableUnit>())
            {
                if (selectableObject.unitClassName == "chevalier")
                {
                    /*if (selectableObject.selectionCircle == null)
                    {
                        selectableObject.selectionCircle = Instantiate(selectionCirclePrefab);
                        selectableObject.selectionCircle.transform.SetParent(selectableObject.transform, false);
                        selectableObject.selectionCircle.transform.eulerAngles = new Vector3(90, 0, 0);

                        // Associé un tag Friendly au personnage sélectionné
                        selectableObject.transform.tag = "Friendly";

                        // Change la grosseur du cercle
                        leProjecteur = selectableObject.selectionCircle.GetComponent<Projector>();
                        leProjecteur.orthographicSize = newScale;

                    }*/
                }
            }
        }

        if (Input.GetKeyDown(selectLancier))
        {
            foreach (var selectableObject in FindObjectsOfType<SelectableUnit>())
            {
                if (selectableObject.unitClassName == "lancier")
                {
                    /*if (selectableObject.selectionCircle == null)
                    {
                        selectableObject.selectionCircle = Instantiate(selectionCirclePrefab);
                        selectableObject.selectionCircle.transform.SetParent(selectableObject.transform, false);
                        selectableObject.selectionCircle.transform.eulerAngles = new Vector3(90, 0, 0);

                        // Associé un tag Friendly au personnage sélectionné
                        selectableObject.transform.tag = "Friendly";

                        // Change la grosseur du cercle
                        leProjecteur = selectableObject.selectionCircle.GetComponent<Projector>();
                        leProjecteur.orthographicSize = newScale;

                    }*/
                }
            }
        }

        if (Input.GetKeyDown(selectTous))
        {
            foreach (var selectableObject in FindObjectsOfType<SelectableUnit>())
            {

                /*if (selectableObject.selectionCircle == null)
                {
                    selectableObject.selectionCircle = Instantiate(selectionCirclePrefab);
                    selectableObject.selectionCircle.transform.SetParent(selectableObject.transform, false);
                    selectableObject.selectionCircle.transform.eulerAngles = new Vector3(90, 0, 0);

                    // Associé un tag Friendly au personnage sélectionné
                    selectableObject.transform.tag = "Friendly";

                    // Change la grosseur du cercle
                    leProjecteur = selectableObject.selectionCircle.GetComponent<Projector>();
                    leProjecteur.orthographicSize = newScale;

                }*/


            }
        }

    }
    //Kevin Langlois Permet la sélection des différents unités à l'aide des boutons présents sur le UI
    private void GestionSelectionUnitBtn(string type)
    {
        //Chevalier
        if (type == "chevalier")
        {
            foreach (var selectableObject in FindObjectsOfType<SelectableUnit>())
            {
                if (selectableObject.unitClassName == "chevalier")
                {
                    /*if (selectableObject.selectionCircle == null)
                    {
                        selectableObject.selectionCircle = Instantiate(selectionCirclePrefab);
                        selectableObject.selectionCircle.transform.SetParent(selectableObject.transform, false);
                        selectableObject.selectionCircle.transform.eulerAngles = new Vector3(90, 0, 0);

                        // Associé un tag Friendly au personnage sélectionné
                        selectableObject.transform.tag = "Friendly";

                        // Change la grosseur du cercle
                        leProjecteur = selectableObject.selectionCircle.GetComponent<Projector>();
                        leProjecteur.orthographicSize = newScale;

                    }*/
                }
            }
        }
        //Archer
        if (type == "archer")
        {
            foreach (var selectableObject in FindObjectsOfType<SelectableUnit>())
            {
                if (selectableObject.unitClassName == "archer")
                {
                    /*if (selectableObject.selectionCircle == null)
                    {
                        selectableObject.selectionCircle = Instantiate(selectionCirclePrefab);
                        selectableObject.selectionCircle.transform.SetParent(selectableObject.transform, false);
                        selectableObject.selectionCircle.transform.eulerAngles = new Vector3(90, 0, 0);

                        // Associé un tag Friendly au personnage sélectionné
                        selectableObject.transform.tag = "Friendly";

                        // Change la grosseur du cercle
                        leProjecteur = selectableObject.selectionCircle.GetComponent<Projector>();
                        leProjecteur.orthographicSize = newScale;

                    }*/
                }
            }
        }
        //Lancier
        if (type == "lancier")
        {
            foreach (var selectableObject in FindObjectsOfType<SelectableUnit>())
            {
                if (selectableObject.unitClassName == "lancier")
                {
                    /*if (selectableObject.selectionCircle == null)
                    {
                        selectableObject.selectionCircle = Instantiate(selectionCirclePrefab);
                        selectableObject.selectionCircle.transform.SetParent(selectableObject.transform, false);
                        selectableObject.selectionCircle.transform.eulerAngles = new Vector3(90, 0, 0);

                        // Associé un tag Friendly au personnage sélectionné
                        selectableObject.transform.tag = "Friendly";

                        // Change la grosseur du cercle
                        leProjecteur = selectableObject.selectionCircle.GetComponent<Projector>();
                        leProjecteur.orthographicSize = newScale;

                    }*/
                }
            }
        }
        //Tous
        if (type == "tous")
        {
            foreach (var selectableObject in FindObjectsOfType<SelectableUnit>())
            {

                /*if (selectableObject.selectionCircle == null)
                {
                    selectableObject.selectionCircle = Instantiate(selectionCirclePrefab);
                    selectableObject.selectionCircle.transform.SetParent(selectableObject.transform, false);
                    selectableObject.selectionCircle.transform.eulerAngles = new Vector3(90, 0, 0);

                    // Associé un tag Friendly au personnage sélectionné
                    selectableObject.transform.tag = "Friendly";

                    // Change la grosseur du cercle
                    leProjecteur = selectableObject.selectionCircle.GetComponent<Projector>();
                    leProjecteur.orthographicSize = newScale;

                }*/


            }
        }
    }



}



