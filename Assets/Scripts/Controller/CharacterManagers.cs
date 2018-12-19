//Script pour gerer la vie de l'unite et l'attaque
//Par Nguyen Hoai Nguyen (12-11-2018)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManagers : MonoBehaviour {

    [Header ("Component pour l'unite")]
    public Animator anim;
    public GameObject unit;

    [Header("Statisque")]
    public float health = 3;
    public float amount = 1;
    public bool isDead = false;
    
    //Detection de l'attaque de l'ennemi
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DamageCollider"))
        {
            Debug.Log("HIT");
            health -= amount;

            if (health < 0)
                health = 0;

            //Si vie est egale a 0 => joue animation de mort et disparition et update le compteur
            if (health <= 0)
            {
                if (!isDead)
                {
                    VariablesGlobales.effectifTotal_joueur_01--;

                    anim.SetTrigger("death");
                    isDead = true;

                    Destroy(unit, 1.2f);
                }
            }
        }
    }

}
