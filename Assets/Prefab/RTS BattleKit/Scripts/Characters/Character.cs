using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.AI;

/* ==========================================================================================================================================================================================*/
/* ==========================================================================================================================================================================================*/
/* ==============================================================   OPTIMIZED FOR CONQUETE ROTALE   =========================================================================================*/
/* ==========================================================================================================================================================================================*/
/* ==========================================================================================================================================================================================*/

public class Character : Photon.MonoBehaviour
{

    //variables visible in the inspector
    public float lives;
    public float damage;
    public float minAttackDistance;
    public float castleStoppingDistance;
    public int addGold;
    public string attackTag;
    public string attackCastleTag;
    public ParticleSystem dieParticles;
    public GameObject ragdoll;
    public AudioClip attackAudio;
    public AudioClip runAudio;

    //variables not visible in the inspector
    [HideInInspector]
    public bool selected;
    [HideInInspector]
    public Transform currentTarget;
    [HideInInspector]
    public Vector3 castleAttackPosition;

    [Space(10)]
    public bool wizard;
    public ParticleSystem spawnEffect;
    public GameObject skeleton;
    public int skeletonAmount;
    public float newSkeletonsWaitTime;

    private NavMeshAgent agent;
    private GameObject[] enemies;
    private GameObject health;
    private GameObject healthbar;

    [HideInInspector]
    public GameObject selectedObject;
    private bool goingToClickedPos;
    private float startLives;
    private float defaultStoppingDistance;
    private GameObject castle;
    private bool wizardSpawns;
    private Animator[] animators;
    private Vector3 targetPosition;
    private AudioSource source;

    private Vector3 randomTarget;
    private WalkArea area;

    private ParticleSystem dustEffect;

    public Color[] Colors = new Color[] { Color.red, Color.blue, Color.yellow, Color.green };

    void Start()
    {

        source = GetComponent<AudioSource>();

        //character is not selected
        selected = false;

        //selected character is not moving to clicked position
        goingToClickedPos = false;

        //find navmesh agent component
        agent = gameObject.GetComponent<NavMeshAgent>();
        animators = gameObject.GetComponentsInChildren<Animator>();

        //find objects attached to this character
        health = transform.Find("Health").gameObject;
        healthbar = health.transform.Find("Healthbar").gameObject;
        health.SetActive(false);
        selectedObject = transform.Find("selected object").gameObject;
        selectedObject.SetActive(false);

        findClosestCastle();

        //set healtbar value
        healthbar.GetComponent<Slider>().maxValue = lives;
        startLives = lives;

        //get default stopping distance
        defaultStoppingDistance = agent.stoppingDistance;

        area = GameObject.FindObjectOfType<WalkArea>();
    }

    void Update()
    {


        if (photonView.isMine == false && PhotonNetwork.connected == true)
        {
            return;
        }

        bool walkRandomly = true;

        if (castle == null)
        {
            findClosestCastle();
        }


        /* ==========================================================================================================================================================================================*/
        // IDLE ANIMATION FOR UNIT 
        /* ==========================================================================================================================================================================================*/
        float velocity = agent.velocity.magnitude;
        if (velocity <= 0.5f)
        {
            for (int i = 0; i < animators.Length; i++)
            {
                animators[i].SetBool("Start", false);
            }
        }
        else
        {
            for (int i = 0; i < animators.Length; i++)
            {
                animators[i].SetBool("Start", true);
            }
        }


        /* ==========================================================================================================================================================================================*/
        // HEALTH BAR FOR UNIT 
        /* ==========================================================================================================================================================================================*/
        if (lives != startLives)
        {
            //only use the healthbar when the character lost some lives
            if (!health.activeSelf)
                health.SetActive(true);

            health.transform.LookAt(2 * transform.position - Camera.main.transform.position);
            healthbar.GetComponent<Slider>().value = lives;
        }

        /* ==========================================================================================================================================================================================*/
        //find closest enemy
        /* ==========================================================================================================================================================================================*/

         findCurrentTarget();
       

        /* ==========================================================================================================================================================================================*/
        //if character ran out of lives add blood particles, add gold and destroy character
        /* ==========================================================================================================================================================================================*/
        if (lives < 0)
        {
            lives = 0;
            StartCoroutine(die());
        }

        //check if character must go to a clicked position

        checkForClickedPosition();

        if (!goingToClickedPos && walkRandomly)
        {
            if (area != null)
            {
                if (agent.stoppingDistance > 1)
                    agent.stoppingDistance = 0.25f;


                if (randomTarget != Vector3.zero)
                {
                    if (animators[0].GetBool("Attacking"))
                    {
                        foreach (Animator animator in animators)
                        {
                            animator.SetBool("Attacking", false);
                        }

                        if (source.clip != runAudio)
                        {
                            source.clip = runAudio;
                            source.Play();
                        }
                    }

                    agent.isStopped = false;
                    agent.destination = randomTarget;
                }
            }

             //return;
        }
        else if (agent.stoppingDistance != defaultStoppingDistance)
        {
            agent.stoppingDistance = defaultStoppingDistance;
        }

if (!goingToClickedPos)
        {
            Debug.Log("il marche 207");
            //If there's a currentTarget and its within the attack range, move agent to currenttarget
            // 
            if (currentTarget != null && Vector3.Distance(transform.position, currentTarget.transform.position) < minAttackDistance)
            {

                agent.destination = currentTarget.position;
                Debug.Log("VAS CHERCHER");

                //check if character has reached its target and than rotate towards target and attack it
                if (Vector3.Distance(currentTarget.position, transform.position) <= agent.stoppingDistance)
                {
                    Vector3 currentTargetPosition = currentTarget.position;
                    currentTargetPosition.y = transform.position.y;
                    transform.LookAt(currentTargetPosition);

                    foreach (Animator animator in animators)
                    {
                        animator.SetBool("Attacking", true);
                    }

                    if (source.clip != attackAudio)
                    {
                        source.clip = attackAudio;
                        source.Play();
                    }

                    Debug.Log("Perd des points de vie");
                    currentTarget.gameObject.GetComponent<Character>().lives -= Time.deltaTime * damage;
                    Debug.Log(" points de vie" + currentTarget.gameObject.GetComponent<Character>().lives);
                    /***********************************************************************************************************************************/
                    //Je detruit l'unité sur le network lorsque sa vie atteint 0.
                    //Lorsque la vie de la target de l'unité est 0 elle se détruit sur tout le network je crois que c'est le mieux qu'on peut atteindre en terme de synchronisation
                    if (currentTarget.gameObject.GetComponent<Character>().lives < 0)
                    {
                        StartCoroutine(die());
                    }

                    /************************************************************************************************************************************/

                }

                //if its still traveling to the target, play running animation
                if (animators[0].GetBool("Attacking") && Vector3.Distance(currentTarget.position, transform.position) > agent.stoppingDistance && !wizardSpawns)
                {
                    foreach (Animator animator in animators)
                    {
                        animator.SetBool("Attacking", false);
                    }

                    if (source.clip != runAudio)
                    {
                        source.clip = runAudio;
                        source.Play();
                    }
                }
            }
            //if currentTarget is out of range...
            else
            {
                //if character is not moving to clicked position attack the castle
                if (!goingToClickedPos)
                {
                    if (!wizardSpawns)
                        agent.isStopped = false;

                    agent.destination = castleAttackPosition;
                }

                //if character is close enough to castle, attack castle
                if (castle != null && Vector3.Distance(transform.position, castleAttackPosition) <= castleStoppingDistance + castle.GetComponent<Castle>().size)
                {
                    agent.isStopped = true;

                    foreach (Animator animator in animators)
                    {
                        animator.SetBool("Attacking", true);
                    }
                    if (castle != null)
                    {
                        castle.GetComponent<Castle>().lives -= Time.deltaTime * damage;
                    }

                    if (source.clip != attackAudio)
                    {
                        source.clip = attackAudio;
                        source.Play();
                    }
                }
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, targetPosition) < agent.stoppingDistance)
            {
                goingToClickedPos = false;
                CharacterManager.target.SetActive(false);

            }
        }
    }

    public void findClosestCastle()
    {
        //find the castles that should be attacked by this character
        GameObject[] castles = GameObject.FindGameObjectsWithTag(attackCastleTag);


        //distance between character and its nearest castle
        float closestCastle = Mathf.Infinity;

        foreach (GameObject potentialCastle in castles)
        {
            //check if there are castles left to attack and check per castle if its closest to this character
            if (Vector3.Distance(transform.position, potentialCastle.transform.position) < closestCastle && potentialCastle != null)
            {
                //if this castle is closest to character, set closest distance to distance between character and this castle
                closestCastle = Vector3.Distance(transform.position, potentialCastle.transform.position);
                //also set current target to closest target (this castle)
                castle = potentialCastle;
            }
        }

        //Define a position to attack the castles(to spread characters when they are attacking the castle)
        if (castle != null)
            castleAttackPosition = castle.transform.position;

    }

    /* ==========================================================================================================================================================================================*/
    /* =============================================================== FIND CLOSEST ENEMY  NEED TO MODIFY FOR GAME ==============================================================================*/
    /* ==========================================================================================================================================================================================*/
    public void findCurrentTarget()
    {
        //find all potential targets (enemies of this character)
        enemies = GameObject.FindGameObjectsWithTag(attackTag);
        Debug.Log("A :" + enemies.Length);
        //distance between character and its nearest enemy
        float closestDistance = Mathf.Infinity;

        foreach (GameObject potentialTarget in enemies)
        {
            Debug.Log("Attaquant" + gameObject.transform.GetChild(2).GetComponent<Renderer>().material.color);
            Debug.Log("Enemi" + potentialTarget.transform.GetChild(2).GetComponent<Renderer>().material.color);

            //check if there are enemies left to attack and check per enemy if its closest to this character
            // 
            if (Vector3.Distance(transform.position, potentialTarget.transform.position) < closestDistance && potentialTarget != null && gameObject.transform.GetChild(2).GetComponent<Renderer>().material.color != potentialTarget.transform.GetChild(2).GetComponent<Renderer>().material.color)
            {
                Debug.Log("Enemi en cours d'attaque");
                //if this enemy is closest to character, set closest distance to distance between character and enemy
                closestDistance = Vector3.Distance(transform.position, potentialTarget.transform.position);
                //also set current target to closest target (this enemy)

                if (!currentTarget || (currentTarget && Vector3.Distance(transform.position, currentTarget.position) > 2))
                {
                    currentTarget = potentialTarget.transform;
                    Debug.Log(currentTarget);
                }
            }
        }
    }

    /* ==========================================================================================================================================================================================*/
    /* ======================================================================= MOVE THE CHARACTER IN CLICKED POSITION   =========================================================================*/
    /* ==========================================================================================================================================================================================*/
    public void checkForClickedPosition()
    {

        RaycastHit hit;

        //if character is selected and right mouse button gets clicked...
        if (selected && (Input.GetMouseButtonDown(1)))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                //if you clicked battle ground, move character to clicked point and play running animation
                if (hit.collider.gameObject.CompareTag("Battle ground"))
                {
                    agent.isStopped = false;
                    agent.SetDestination(hit.point);
                    CharacterManager.clickedPos = hit.point;
                    targetPosition = hit.point;
                    goingToClickedPos = true;
                    if (GetComponent<archer>() != null)
                        agent.stoppingDistance = 2;

                    foreach (Animator animator in animators)
                    {
                        animator.SetBool("Attacking", false);
                        animator.SetBool("Start", true);
                    }

                    if (source.clip != runAudio)
                    {
                        source.clip = runAudio;
                        source.Play();
                    }

                    //set the yellow target position
                    CharacterManager.target.transform.position = hit.point;
                    CharacterManager.target.SetActive(true);
                }

        }
    }

    /* ==========================================================================================================================================================================================*/
    /* ========================================================================== WHEN CHARACTER LIFE IS EQUAL TO ZERO ==========================================================================*/
    /* ==========================================================================================================================================================================================*/
    public IEnumerator die()
    {
        Debug.Log("DIE BITCH");

        if (ragdoll == null)
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            ParticleSystem particles = Instantiate(dieParticles, position, transform.rotation) as ParticleSystem;

            if (GameObject.Find("Manager") != null)
                particles.transform.parent = GameObject.Find("Manager").transform;
        }
        else
        {
            PhotonNetwork.Instantiate(ragdoll.name, gameObject.transform.position, Quaternion.identity, 0);
        }

        foreach (Character character in GameObject.FindObjectsOfType<Character>())
        {
            if (character != this)
                character.findCurrentTarget();
        }

        yield return new WaitForEndOfFrame();

        PhotonNetwork.Destroy(gameObject);
    }

    //essayer d'envoyer la vie sur le network************************************************************************************************
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(lives);
        }

        else
        {
            this.lives = (float)stream.ReceiveNext();
        }
    }
}
