using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkColor : MonoBehaviour {

    public MeshRenderer[] Renderers;

	// Use this for initialization
	void Update () {
        var Color = GetComponent<Player>().Info.AccentColor;

        foreach (var r in Renderers)
        {
            r.material.color = Color;
        }
	}
}
