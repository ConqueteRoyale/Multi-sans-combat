// SCRIPT DE DÉPLACEMENT D'UNITÉ POUR TEST ANIMATION (MECANIM)
// Version 3.0 par Nguyen le 29 octobre

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class TestMouvementFormation : MonoBehaviour {

    NavMeshAgent agentNav;
    public bool DeplacementFlag = false;
    public GameObject unit;
    public bool selected = false;

    // Use this for initialization
    void Start() {

        agentNav = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update() {

        float dist = agentNav.remainingDistance;

        // test pour l'instant, voir readme
        DeplacementUnite();
        
    }

    public void DeplacementUnite(){

        agentNav.isStopped = false;

        if (unit == null)
        {
            return;
        }

        RaycastHit hit;

        if (Input.GetMouseButtonDown(1) && unit.tag == "Friendly")
        {

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agentNav.SetDestination(hit.point);
            }
        }

        if (unit.gameObject.CompareTag("Untagged"))
        {
            StopUnit();
        }
    }

    public void StopUnit()
    {
        agentNav.ResetPath();
        agentNav.isStopped = true;
    }

}
