using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    public Material mat;

    public void Update()
    {
        mat.SetFloat("_LerpValue", 1+ Mathf.Sin(Time.time));
    }
}
