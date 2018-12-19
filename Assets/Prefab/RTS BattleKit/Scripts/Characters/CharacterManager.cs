using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//troop/unit settings
[System.Serializable]
public class Troop{
	public GameObject deployableTroops;
	public int troopCosts;
	public Sprite buttonImage;
	[HideInInspector]
	public GameObject button;
}

/* ==========================================================================================================================================================================================*/
/* ==========================================================================================================================================================================================*/
/* ==============================================================   OPTIMIZED FOR CONQUETE ROTALE   =========================================================================================*/
/* ==========================================================================================================================================================================================*/
/* ==========================================================================================================================================================================================*/

public class CharacterManager : Photon.MonoBehaviour {

	//variables visible in the inspector	
	public int startGold;
	public GUIStyle rectangleStyle;
	public ParticleSystem newUnitEffect;
	public Texture2D cursorTexture;
	public Texture2D cursorTexture1;
	public bool highlightSelectedButton;
	public Color buttonHighlight;
	public GameObject button;
	public float bombLoadingSpeed;
	public float BombRange;
	public GameObject bombExplosion;
	public KeyCode deselectKey;
	
	[Space(5)]
	public bool mobileDragInput;
	public bool originalPreviewSize;
	public int dragPreviewSize;
	
	[Space(10)]
	public List<Troop> troops;
	
	//variables not visible in the inspector
	public static Vector3 clickedPos;
	public static GameObject target;
	
	private Vector2 mouseDownPos;
    private Vector2 mouseLastPos;
	private bool visible; 
    private bool isDown;
	private GameObject[] knights;
	private int selectedUnit;
	private GameObject characterParent;
	private GameObject selectButton;
	
	private bool canDrag;
	private GameObject dragPreview;
	
	public static bool selectionMode;
    public Color[] Colors = new Color[] { Color.red, Color.blue, Color.yellow, Color.green };

    void Awake(){
            //find some objects
            characterParent = new GameObject("Characters");
            selectButton = GameObject.Find("Character selection button");
            target = GameObject.Find("target");
            if (!GameObject.Find("Mobile"))
                mobileDragInput = false;

            if (mobileDragInput)
            {
                highlightSelectedButton = false;
                GameObject.FindGameObjectWithTag("Mobile units button").SetActive(false);
            }

            //set cursor and add the character buttons
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    void Start()
    {
        target.SetActive(false);
    }

    void Update(){

            /* ==========================================================================================================================================================================================*/
            // START SELECTION SQUARE ON SCREEN 
            //ray from main camera
            //if space is down too set the position where you first clicked
            // Continue tracking mouse position until mouse button is up
            /* ==========================================================================================================================================================================================*/

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);


            if (Input.GetMouseButtonDown(0) && selectionMode
            && (!GameObject.Find("Mobile") || (GameObject.Find("Mobile") && !Mobile.selectionModeMove)))
            {
                mouseDownPos = Input.mousePosition;
                isDown = true;
                visible = true;
            }

            if (isDown)
            {
                mouseLastPos = Input.mousePosition;
                //if you release mouse button, remove rectangle and stop tracking
                if (Input.GetMouseButtonUp(0))
                {
                    isDown = false;
                    visible = false;
                }
            }

            /* ==========================================================================================================================================================================================*/
            // DESELCTION OF ALL SELECTED UNIT
            // find and store all selectable objects (objects in scene with knight tag)
            /* ==========================================================================================================================================================================================*/
            knights = GameObject.FindGameObjectsWithTag("Knight");

            //if player presses d, deselect all characters
            if (Input.GetKeyDown(deselectKey))
            {
                foreach (GameObject knight in knights)
                {
                    if (knight != null)
                    {
                        Character character = knight.GetComponent<Character>();
                        character.selected = false;
                        character.selectedObject.SetActive(false);
                    }
                }
            }
            /* ==========================================================================================================================================================================================*/
            // SELECTION SQUARE ON SCREEN FOR UNIT 
            // start selection mode when player presses spacebar
            /* ==========================================================================================================================================================================================*/

            if (Input.GetKeyDown("space"))
            {
                selectCharacters();
            }

            if (Input.GetMouseButtonUp(0) && mobileDragInput && canDrag)
            {
                canDrag = false;
                Destroy(dragPreview);

            }

            if (Input.GetMouseButton(0) && canDrag && dragPreview != null)
            {
                dragPreview.transform.position = Input.mousePosition;

                if (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    dragPreview.GetComponent<Image>().enabled = false;
                }
                else
                {
                    dragPreview.GetComponent<Image>().enabled = true;
                }
            }
    }// fin update

    /* ==========================================================================================================================================================================================*/
    /* ======================================================== DRAW SELECTION SQUARE ON SCREEN  ================================================================================================*/
    /* ==========================================================================================================================================================================================*/
    void OnGUI(){
		//check if rectangle should be visible
		if(visible){
            // Find the corner of the box
            Vector2 origin;
            origin.x = Mathf.Min(mouseDownPos.x, mouseLastPos.x);
			
            // GUI and mouse coordinates are the opposite way around.
            origin.y = Mathf.Max(mouseDownPos.y, mouseLastPos.y);
            origin.y = Screen.height - origin.y;  
			
            //Compute size of box
            Vector2 size = mouseDownPos - mouseLastPos;
            size.x = Mathf.Abs(size.x);
            size.y = Mathf.Abs(size.y);   
			
            // Draw the GUI box
            Rect rect = new Rect(origin.x, origin.y, size.x, size.y);
            GUI.Box(rect, "", rectangleStyle);
			
			foreach(GameObject knight in knights){
			if(knight != null){
			Vector3 pos = Camera.main.WorldToScreenPoint(knight.transform.position);
			pos.y = Screen.height - pos.y;
			//foreach selectable character check its position and if it is within GUI rectangle, set selected to true
			if(rect.Contains(pos)){
			Character character = knight.GetComponent<Character>();
			character.selected = true;	
			character.selectedObject.SetActive(true);
			}
			}
			}
		}
    }

    /* ==========================================================================================================================================================================================*/
    /* ================================================================ SELECTION OF UNIT ANOTHER ONE   =========================================================================================*/
    /* ==========================================================================================================================================================================================*/

    //function to select another unit
    public void selectUnit(int unit){
		if(highlightSelectedButton){
            //remove all outlines and set the current button outline visible
            for (int i = 0; i < troops.Count; i++)
            {

                if (troops[i].button != null)
                {
                    troops[i].button.GetComponent<Outline>().enabled = false;
                }
            }

            for (int i = 0; i < troops.Count; i++)
            {
                if (troops[i].deployableTroops.transform.GetChild(2).GetComponent<Renderer>().material.color == Colors[VariablesGlobales.monJoueurID]){
                    troops[unit].button.GetComponent<Outline>().enabled = true;
                }


            }

            
		}
		
		//selected unit is the pressed button
		selectedUnit = unit;
	}

    /* ==========================================================================================================================================================================================*/
    /* ================================================================ CHANGE CURSOR MODE FOR SELECTION  =======================================================================================*/
    /* ==========================================================================================================================================================================================*/

    public void selectCharacters(){
		//turn selection mode on/off
		selectionMode = !selectionMode;
		if(selectionMode){
		//set cursor and button color to show the player selection mode is active
		Cursor.SetCursor(cursorTexture1, Vector2.zero, CursorMode.Auto);
		if(GameObject.Find("Mobile")){
			if(Mobile.deployMode){
				GameObject.Find("Mobile").GetComponent<Mobile>().toggleDeployMode();
			}
			Mobile.camEnabled = false;
		}
		}
		else{
		//show the player selection mode is not active
		Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
		
		//set target object false and deselect all units
		foreach(GameObject knight in knights){
		if(knight != null){
			Character character = knight.GetComponent<Character>();
			character.selected = false;	
			character.selectedObject.SetActive(false);
		}
        }
        target.SetActive(false);
        if (GameObject.Find("Mobile"))
        {
            Mobile.camEnabled = true;
        }
        }
	}
}