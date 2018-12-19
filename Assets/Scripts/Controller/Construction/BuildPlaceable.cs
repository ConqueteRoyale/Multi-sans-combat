using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPlaceable : MonoBehaviour {

    public List<GameObject> currentHitsObjects = new List<GameObject>();
    //public GameObject currentHitObject;

    public float sphereRadius;
    public float maxDistance;
    public LayerMask layerMask;

    private Vector3 origin;
    private Vector3 direction;

    private float currentHitDistance;

    public bool isBuildable = true;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        origin = transform.position;
        direction = transform.forward;


        currentHitDistance = maxDistance;
        currentHitsObjects.Clear();
        isBuildable = true;

        RaycastHit[] hits = Physics.SphereCastAll(origin, sphereRadius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
        foreach(RaycastHit hit in hits)
        {
            currentHitsObjects.Add(hit.transform.gameObject);
            currentHitDistance = hit.distance;

            if(currentHitsObjects != null)
            {
                isBuildable = false;
            }
        }
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereRadius);
    }
}
