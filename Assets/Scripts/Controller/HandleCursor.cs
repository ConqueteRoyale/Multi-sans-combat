//Script pour changer le cursor par rapport au element de hover
//Par Nguyen Hoai Nguyen (11-11-2018)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCursor : MonoBehaviour {

    [Header ("Sprite pour la souris")]
    public Texture2D mouse;
    public Texture2D attack;
    public Texture2D ally;

    [Header("Variable de la souris")]
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    //Changer de sprite de la souris par rapport au element de hover
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity)) 
        {
            if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Target"))
            {
                
                setAttack();
            }

            if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Allies"))
            {
                setAlly();
            }

            else if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Unit"))
            {
                setAlly();
            }

            else
            {
                setMouse();
            }
        }
	}

    //Fonction pour changer la souris
    public void setMouse()
    {
        Cursor.SetCursor(mouse, hotSpot, cursorMode);
    }

    public void setAttack()
    {
        Cursor.SetCursor(attack, hotSpot, cursorMode);
    }

    public void setAlly()
    {
        Cursor.SetCursor(ally, hotSpot, cursorMode);
    }
}
