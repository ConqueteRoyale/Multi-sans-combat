using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitMove))]
public class UnitController : MonoBehaviour {

    public LayerMask movementMask;
    public LayerMask targetMask;
    Camera cam;
    UnitMove move;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        move = GetComponent<UnitMove>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                move.MoveToPoint(hit.point);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, targetMask))
            {

            }
        }
    }
}
