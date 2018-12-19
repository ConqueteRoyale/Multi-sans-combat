// SCRIPT DE DÉPLACEMENT D'UNITÉ POUR TEST ANIMATION (MECANIM)
// Version 2.0 par Ulysse le 15 octobre

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {

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
        //Debug.Log(Mathf.Abs(agentNav.velocity.sqrMagnitude));

        // test pour l'instant, voir readme
        DeplacementUnite();
    }

    public void DeplacementUnite(){

        if (unit == null)
        {
            return;
        }
            RaycastHit hit;

        if (Input.GetMouseButtonDown(1) && unit.tag == "Friendly")
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask mask = LayerMask.GetMask("Map");


            if (Physics.Raycast(ray, out hit, 100, mask))
            {
                agentNav.SetDestination(hit.point);
            }

         
            if (!agentNav.pathPending)
            {
                if(Mathf.Abs(agentNav.velocity.sqrMagnitude) < 0.8f)
                {
                    ArreterUnite();
                }


                if (agentNav.remainingDistance <= agentNav.stoppingDistance)
                {

                    if (!agentNav.hasPath || Mathf.Abs(agentNav.velocity.sqrMagnitude) < float.Epsilon)
                    {

                        ArreterUnite();
                    }
                }
            }
        }
    }

    public void ArreterUnite(){
        agentNav.isStopped = true;
    } 
}
