using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSManager : MonoBehaviour {

    public static RTSManager Current = null;

    public List<PlayerSetupDefinition> Players = new List<PlayerSetupDefinition>();

    public Vector3? ScreenPointToMapPosition(Vector2 point)
    {
        var ray = Camera.main.ScreenPointToRay(point);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, Mathf.Infinity))
            return null;

        return hit.point;
    }

	// Use this for initialization
	void Start () {
        Current = this;

        foreach (var p in Players)
        {
            foreach (var u in p.StartingUnits)
            {
                var go = (GameObject)GameObject.Instantiate(u, p.Location.position, p.Location.rotation);
                var player = go.AddComponent<Player>();
                player.Info = p;


                if (!p.isAi)
                {
                    if (Player.Default == null) Player.Default = p;
                }
            }

            
        } 
	}
	
	// Update is called once per frame
	void Update () {
        foreach (var p in Players)
        {
            foreach (var a in p.ActiveUnits)
            {
                if ((a.GetComponent<Player>() == null))
                {
                    var go = a.AddComponent<Player>();

                    go.Info = p;

                    if (!p.isAi) 
                    {
                        if (Player.Default == null) Player.Default = p;
                    }
                }
            }
        }
	}


}
