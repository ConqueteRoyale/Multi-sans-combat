// SCRIPT DE FORMATION D'UNITÉ
// Version 1.0 par Nguyen le 29 octobre

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//TESTING == NOUVEAU SYSTEME EN DEVELOPPEMENT
public class FormationController : MonoBehaviour {

    //public GameObject holder;
    //public GameObject formation;
    //public List<GameObject> formLocation;
    public GameObject[] selectedUnit;
    //public GameObject master;
    //public GameObject tempo;

    public float percent;

    // Use this for initialization
    void Start () {
        //GetChildComponents();
    }
	
	// Update is called once per frame
	void Update () {
        selectedUnit = GameObject.FindGameObjectsWithTag("Friendly");
        //PlaceFormation();
    }

    /*void GetChildComponents()
    {
        
        foreach (Transform child in holder.transform) 
        {
            foreach (Transform childSphere in child.transform)
            {
                formLocation.Add(childSphere.gameObject);
            }

            
        }
    }*/

    /*void PlaceFormation()
    {
        if (selectedUnit.Length > 1)
        {
            if(Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    formation.transform.position = hitInfo.point;
                    MoveUnitToPlace();
                }
            }
            
        }
    }*/

    void MoveUnitToPlace()
    {
        RaycastHit hit;

        if (Input.GetMouseButtonDown(1))
        {

            for (int i = 0; i < selectedUnit.Length; i++)
            {
                if (selectedUnit.Length > 1)
                {

                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                    {
                        selectedUnit[i].GetComponent<NavMeshAgent>().SetDestination(hit.point);
                        selectedUnit[i].GetComponent<NavMeshAgent>().stoppingDistance = 0.3f;
                    }


                    //selectedUnit[i].GetComponent<NavMeshAgent>().destination = formLocation[i].transform.position;
                }
            }
        }
    }

    /*void DuplicateFormation(RaycastHit hit)
    {

        if (selectedUnit.Length < 6)
        {
            master = Instantiate(formation, hit.point, transform.rotation);
        }

        if (selectedUnit.Length > 6 )
        {
            Vector3 zTempo = hit.point;

            zTempo.z += 0.2f;

            master = Instantiate(formation, hit.point, transform.rotation);
            tempo = Instantiate(formation, zTempo, transform.rotation);
            GetChildComponents();
        }
    }*/

}
