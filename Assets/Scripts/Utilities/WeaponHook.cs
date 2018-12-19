// Script pour ouvrir et fermer le damage collider, controler par keyframe dans les animations
// Par Nguyen Hoai Nguyen (01-10-2018)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHook : MonoBehaviour {

    public GameObject damageCollider;

    public void OpenDamageColliders()
    {
        damageCollider.SetActive(true);
    }

    public void CloseDamageColliders()
    {
        damageCollider.SetActive(false);
    }
}
