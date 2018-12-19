// Script pour gerer la vie d'un batiment
// Par Nguyen Hoai Nguyen (12-11-2018) 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBuildingManager : MonoBehaviour {

    [Header("Variable pour la vie")]
    public float startHealth = 100;
    private float health;
    public float amount;
    public float regeneration;

    [Header("Barre de vie")]
    public Image healthBar;

	//Initialiser la vie
	void Start () {
        health = startHealth;
	}
	
	//Controle la vie en rapport avec la barre
	void Update () {
        healthBar.fillAmount = health / startHealth;

        if (health <= 0f)
        {
            Die();
        }

        //Regeneration de vie du batiments
        if (health < startHealth)
        {
            health += regeneration * Time.deltaTime;
            if (health > startHealth)
            {
                health = startHealth;
            }
        }
    }

    //Fin de jeu si le chateau est detruit
    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Detection d'attaque des unites
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DamageCollider"))
        {
            health -= amount; 
        }
    }
}
