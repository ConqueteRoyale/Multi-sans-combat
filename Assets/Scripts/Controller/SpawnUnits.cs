using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnits : MonoBehaviour {

    public GameObject uneUnite;
    public float spawnTime;
    public float DelaiSpawn;
    public GameObject lePointDuSpawn;

    RTSManager manager;

    // Use this for initialization
    void Start() {

        // La fonction se repete a chaque 5 secondes et l'on instantie une nouvelle unite
        InvokeRepeating("spawnUnite", DelaiSpawn, spawnTime);

        manager = GameObject.FindGameObjectWithTag("RTSManager").GetComponent<RTSManager>();
    }

    // Update is called once per frame
    void Update() {

    }

    void spawnUnite() {

        // On instancie nos unites et on les places dans l'environnement
        var unit = PhotonNetwork.Instantiate(uneUnite.name, lePointDuSpawn.transform.position, Quaternion.identity, 0);
        manager.Players[0].activeUnits.Add(unit);
        VariablesGlobales.effectifTotal_joueur_01++;
    }
}
