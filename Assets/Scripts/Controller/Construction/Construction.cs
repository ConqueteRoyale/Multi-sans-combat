// Script gérant la construction des bâtiments : leur placement sur la carte et ses conséquences
// Auteur principal: Lucas Theillet
// Autres auteurs : Nguyen Hoai Nguyen (isPlaceable - si les bâtiments peuvent être placés ou pas selon le terrain et les obstacles)


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.AI;

public class Construction : MonoBehaviour
{

    // Boutons de l'UI pour construire
    public Button btnConstruireMine;
    public Button btnConstruireMineGrande;
    public Button btnConstruireCaserneLancier;
    public Button btnConstruireCaserneChevalier;
    public Button btnConstruireCaserneArcher;

    // Les prefabs des différents bâtiments
    public GameObject minePrefab;
    public GameObject mineGrandePrefab;
    public GameObject caserneLancierPrefab;
    public GameObject caserneChevalierPrefab;
    public GameObject caserneArcherPrefab;

    // Les raccourcis-clavier pour construire des bâtiments
    private KeyCode newMine = KeyCode.Alpha1;
    private KeyCode newMineGrande = KeyCode.Alpha2;
    private KeyCode newCaserneLancier = KeyCode.Alpha3;
    private KeyCode newCaserneChevalier = KeyCode.Alpha4;
    private KeyCode newCaserneArcher = KeyCode.Alpha5;

    // Le bâtiment en train d'être placé
    private GameObject currentPlaceableObject;
    private int idBatiment;

    // Les sons du jeu
    public AudioSource sonsEnJeu;
    public AudioClip clipSonConstruction;// Le son de construction lorsqu'on place un bâtiment
    public AudioClip clipSonPasAssezOr1;// Le son de manque d'or pour placer un bâtiment
    public AudioClip clipSonPasAssezOr2;// Le son de manque d'or pour placer un bâtiment
    public AudioClip clipSonPasAssezOr3;// Le son de manque d'or pour placer un bâtiment

    // Bool indiquant si le bâtiment est constructible ou pas
    public bool isPlaceable;

    // Manager du jeu
    ExitGames.UtilityScripts.PlayerRoomIndexing playerRoomIndex;

    


    // Au début de la partie, on ajoute des détecteurs de clics au boutons de l'UI
    private void Start() {
        // Le code arrête ici for some reason
        Button btn1 = btnConstruireMine.GetComponent<Button>();
        Button btn2 = btnConstruireMineGrande.GetComponent<Button>();
        Button btn3 = btnConstruireCaserneLancier.GetComponent<Button>();
        Button btn4 = btnConstruireCaserneChevalier.GetComponent<Button>();
        Button btn5 = btnConstruireCaserneArcher.GetComponent<Button>();

        btn1.onClick.AddListener(delegate {GestionNouveauBatimentBtn(0); });
        btn2.onClick.AddListener(delegate {GestionNouveauBatimentBtn(1); });
        btn3.onClick.AddListener(delegate {GestionNouveauBatimentBtn(2); });
        btn4.onClick.AddListener(delegate {GestionNouveauBatimentBtn(3); });
        btn5.onClick.AddListener(delegate {GestionNouveauBatimentBtn(4); });

        //Debug.Log(playerRoomIndex.name);
        

    

    }


    // À chaque frame
    private void Update()
    {
        GestionNouveauBatimentKey(); // Detection d'un raccourci de construction

        // Si un bâtiment est en train d'être placé par le joueur (mode 'fantôme')
        if (currentPlaceableObject != null)
        {
            DeplacerObjetVersCurseur(); // On le déplace à la position du curseur
            ConstruireAuClic(); // On lance la fonction de construction
        }

        // Si le bâtiment est à un emplacement constructible, on affiche un contour vert
        if (currentPlaceableObject != null) {
	        if(isPlaceable == true)
	        {
	            currentPlaceableObject.GetComponent<Outlines>().OutlineColor = Color.green;
	        }

	        // Si le bâtiment n'est pas à un emplacement constructible, on affiche un contour rouge
	        if(isPlaceable == false)
	        {
	            currentPlaceableObject.GetComponent<Outlines>().OutlineColor = Color.red;
	        }
        }

    }


    // AU CLIC SUR UN DES BOUTONS DE CONSTRUCTION, ON PLACE UN "FANTÔME" DU BÂTIMENT SUR LA SCÈNE
    private void GestionNouveauBatimentBtn(int type)
    {
        // Bâtiment MINE 
        if (type == 0)
        {
            // Si l'object n'est plus en mode placement, on supprime son 'fantôme'
            if (currentPlaceableObject != null)
            {
                PhotonNetwork.Destroy(currentPlaceableObject);
            }
            // Sinon on place un vrai bâtiment sur la carte depuis un prefab
            else
            {

                
                //currentPlaceableObject = Instantiate(minePrefab);
                currentPlaceableObject = PhotonNetwork.Instantiate(minePrefab.name, InputToEvent.inputHitPos + new Vector3(0, 0f, 0),Quaternion.identity, 0);
                idBatiment = 0;
                
            }
        }

        // Bâtiment GRANDE MINE 
        if (type == 1)
        {
            // Si l'object n'est plus en mode placement, on supprime son 'fantôme'
            if (currentPlaceableObject != null)
            {
                PhotonNetwork.Destroy(currentPlaceableObject);
            }
            // Sinon on place un vrai bâtiment sur la carte depuis un prefab
            else
            {
                currentPlaceableObject = PhotonNetwork.Instantiate(mineGrandePrefab.name, InputToEvent.inputHitPos + new Vector3(0, 0f, 0), Quaternion.identity, 0);
                idBatiment = 1;

            }
        }

        // Bâtiment LANCIER 
        if (type == 2)
        {
            if (currentPlaceableObject != null)
            {
                PhotonNetwork.Destroy(currentPlaceableObject);
            }
            else
            {
                currentPlaceableObject = PhotonNetwork.Instantiate(caserneLancierPrefab.name, InputToEvent.inputHitPos + new Vector3(0, 0f, 0), Quaternion.identity, 0);
                idBatiment = 2;
            }
        }

        // Bâtiment CHEVALIER 
        if (type == 3)
        {
            if (currentPlaceableObject != null)
            {
                PhotonNetwork.Destroy(currentPlaceableObject);
            }
            else
            {
                currentPlaceableObject = PhotonNetwork.Instantiate(caserneChevalierPrefab.name, InputToEvent.inputHitPos + new Vector3(0, 0f, 0), Quaternion.identity, 0);
                idBatiment = 3;
            }
        }

        // Bâtiment GÉANTS 
        if (type == 4)
        {
            if (currentPlaceableObject != null)
            {
                PhotonNetwork.Destroy(currentPlaceableObject);
            }
            else
            {
                currentPlaceableObject = PhotonNetwork.Instantiate(caserneArcherPrefab.name, InputToEvent.inputHitPos + new Vector3(0, 0f, 0), Quaternion.identity, 0);
                idBatiment = 4;
            }
        }

    }    
    // AU CLIC OU TOUCHE-RACCOURCI, ON PLACE UN "FANTÔME" DU BÂTIMENT SUR LA SCÈNE
    // Même code que la fonction GestionNouveauBatimentBtn() mais utilise des keycode plutôt que des boutons
    private void GestionNouveauBatimentKey()
    {
        
        if (!PhotonNetwork.inRoom)
        {
            // only use PhotonNetwork.Instantiate while in a room.
            return;
        }
        

        // MINE 
        if (Input.GetKeyDown(newMine))
        {
            if (currentPlaceableObject != null)
            {
                PhotonNetwork.Destroy(currentPlaceableObject);
            }
            else
            {
                currentPlaceableObject = PhotonNetwork.Instantiate(minePrefab.name, InputToEvent.inputHitPos + new Vector3(0, 0f, 0), Quaternion.identity, 0);
                idBatiment = 0;
            }
        }

        // GRANDE MINE 
        if (Input.GetKeyDown(newMineGrande))
        {
            if (currentPlaceableObject != null)
            {
                PhotonNetwork.Destroy(currentPlaceableObject);
            }
            else
            {
                currentPlaceableObject = PhotonNetwork.Instantiate(mineGrandePrefab.name, InputToEvent.inputHitPos + new Vector3(0, 0f, 0), Quaternion.identity, 0);
                idBatiment = 1;
            }
        }

        // CASERNE LANCIER 
        if (Input.GetKeyDown(newCaserneLancier))
        {
            if (currentPlaceableObject != null)
            {
                PhotonNetwork.Destroy(currentPlaceableObject);
            }
            else
            {
                currentPlaceableObject = PhotonNetwork.Instantiate(caserneLancierPrefab.name, InputToEvent.inputHitPos + new Vector3(0, 0f, 0), Quaternion.identity, 0);
                idBatiment = 2;
            }
        }

        
        // CASERNE CHEVALIER 
        if (Input.GetKeyDown(newCaserneChevalier))
        {
            if (currentPlaceableObject != null)
            {
                PhotonNetwork.Destroy(currentPlaceableObject);
            }
            else
            {
                currentPlaceableObject = PhotonNetwork.Instantiate(caserneChevalierPrefab.name, InputToEvent.inputHitPos + new Vector3(0, 0f, 0), Quaternion.identity, 0);
                idBatiment = 3;
            }
        }

        // CASERNE ARCHER 
        if (Input.GetKeyDown(newCaserneArcher))
        {
            if (currentPlaceableObject != null)
            {
                PhotonNetwork.Destroy(currentPlaceableObject);
            }
            else
            {
                currentPlaceableObject = PhotonNetwork.Instantiate(caserneArcherPrefab.name, InputToEvent.inputHitPos + new Vector3(0, 0f, 0), Quaternion.identity, 0);
                idBatiment = 4;
            }
        }          
    }


    // DÉPLACEMENT DU "FANTÔME" DU BÂTIMENT À LA POSITION DE LA SOURIS
    private void DeplacerObjetVersCurseur()
    {
        // Raycast depuis la caméra jusqu'à la position de la souris
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Lorsque le raycast touche, on déplace le fantôme de bâtiment jusqu'au curseur
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            currentPlaceableObject.transform.position = hitInfo.point;
            isPlaceable = currentPlaceableObject.GetComponent<BuildPlaceable>().isBuildable; // et on indique s'il est constructible ou non
            //Debug.Log(isPlaceable);
          
        }
    }



    // DÉCLENCHEMENT DE LA CONSTRUCTION AU CLIC 
    private void ConstruireAuClic()
    {


        // Si on fait un clic gauche et que le bâtiment est dans une zone constructible, on le construit
        if (Input.GetMouseButtonDown(0) && isPlaceable == true)
        {
            // Debug.Log(VariablesGlobales.banqueOr_joueur_01);
            // Debug.Log(idBatiment);
            // Debug.Log(VariablesGlobales.prixBatiments[idBatiment]);
            if (VariablesGlobales.banqueOr_joueur_01 < VariablesGlobales.prixBatiments[idBatiment]) {
                // Debug.Log("Pas assez d'argent!");
                
				int son = Random.Range(1, 4);
				if (son == 1) {
        			sonsEnJeu.PlayOneShot(clipSonPasAssezOr1, 1f);
        		}
				else if (son == 2) {
        			sonsEnJeu.PlayOneShot(clipSonPasAssezOr2, 1f);
        		}
				else if (son == 3) {
        			sonsEnJeu.PlayOneShot(clipSonPasAssezOr3, 1f);
        		}        		
            }
            else {
            	
                VariablesGlobales.banqueOr_joueur_01 -= VariablesGlobales.prixBatiments[idBatiment];
                StartCoroutine(GestionConstruction(currentPlaceableObject));
                // Destroy(currentPlaceableObject);
                if (Input.GetKey("left shift") == false && Input.GetKey("right shift") == false) {
                	Debug.Log("OUI DESTROY");
               		currentPlaceableObject = null;   
                } 
                else {
               		currentPlaceableObject = null;   
                	GestionNouveauBatimentBtn(idBatiment);
                }   
            }
                
        }
        else if (Input.GetMouseButtonDown(1) && currentPlaceableObject != null) {
           PhotonNetwork.Destroy(currentPlaceableObject); 
        }
    }



    // GESTION DE LA CONSTRUCTION DES BÂTIMENTS
    IEnumerator GestionConstruction(GameObject objet)
    {
        //manager.Players[0].activeUnits.Add(objet);

        // On active différents éléments du gameobject
        objet.GetComponent<Collider>().enabled = true;
        objet.gameObject.GetComponent<Outlines>().enabled = false;
        // La construction du bâtiment est COMMENCÉE
        GameObject particuleConstru = objet.gameObject.transform.GetChild(0).gameObject; // L'effet de particule de construction
        GameObject canvaConstru = objet.gameObject.transform.GetChild(1).gameObject; // La barre de progression de construction

        particuleConstru.SetActive(true); // Particules activées
        canvaConstru.SetActive(true); // Barre de construction activée
        sonsEnJeu.PlayOneShot(clipSonConstruction, 1f);

        // Durée de la construction en secondes
        yield return new WaitForSeconds(10f);

        // La construction du bâtiment est TERMINÉE
        // On désactive les particules et la barre de progrès
        objet.GetComponent<NavMeshObstacle>().enabled = true;
        particuleConstru.SetActive(false);
        canvaConstru.SetActive(false);
        objet.GetComponent<SpawnUnits>().enabled = true;
    }
}