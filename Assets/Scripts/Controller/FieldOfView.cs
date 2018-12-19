// Script pour le systeme d'attaque et de FOV
// Par Nguyen Hoai Nguyen (05-11-2018)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FieldOfView : MonoBehaviour {

    [Header("Component pour l'unite")]
    public Animator anim;
    public GameObject unit;
    Transform myTransform;

    [Header("Variable pour le FOV")]
    public float viewRadius;
    public float viewRadiusAttack;

    [Range(0, 360)]
    public float viewAngle;

    [Range(0, 360)]
    public float viewAngleAura;

    [Header("Mask pour detection")]
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [Header("Liste pour les elements du script")]
    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();
    public List<Transform> visibleTargetsAura = new List<Transform>();

    public List<Transform> targets;
    public List<Transform> targetsAura;

    [Header("States")]
    public bool isLockOn = false;
    public bool isNearMe = false;

    [Header("Random")]
    public NavMeshAgent agentNav;
    public int index = 1;
    public float speedTarget = 1f;
    public Vector3 speedRot = Vector3.right * 50f;
    public float moveSpeed = 0.7f;
    public float rotationSpeed = 10f;
    public bool canMove = true;

    public GameObject AI_Target;

    //Trouver les targets dans le cercle de FOV
    void Start()
    {
        myTransform = GetComponent<Transform>();
        targets = new List<Transform>();
        targetsAura = new List<Transform>();
    }

    //Dessiner 2 cercle autour du personnage et detecter les ennemis dans les deux cercles (PLUS PETIT CERCLE POUR ATTAQUER AUTOMATIQUEMENT ET FOLLOW TARGET)
    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        targets.Clear();
        isLockOn = false;
        index = 1;
        if (isLockOn == false)
            anim.SetFloat("canAttack", 0f);

        Collider[] targetsInviewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        //Rechercher tout les targets dans la zone de vue
        for (int i = 0; i < targetsInviewRadius.Length; i++)
        {
            Transform target = targetsInviewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            //Verifier s'il y a un ennemi dans l'angle de vue du personnage
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                //Verifier s'il y a un obstacle entre le personnage et l'ennemi
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                    
                    if (visibleTargets.Count > 0)
                    {
                        if (targets.Count.Equals(0))
                        {
                            AddAllEnemies();
                            isLockOn = true;
                        }
                    }

                    if(isLockOn == true)
                        anim.SetFloat("canAttack", 1f);
                }

            }
        }
    }

    //Dessiner 2 cercle autour du personnage et detecter les ennemis dans les deux cercles (PLUS GROS CERCLE SAPPROCHER DU ENNEMI (AUTO ATTACK COMME DANS LEAGUE OF LEGENDS))
    public void FindTargetsNearMe()
    {
        visibleTargetsAura.Clear();
        targetsAura.Clear();
        isNearMe = false;
        index = 2;

        Collider[] targetNear = Physics.OverlapSphere(transform.position, viewRadiusAttack, targetMask);
        //Rechercher tout les targets dans la zone de vue et detecter l'ennemi le plus proche pour aller a son position
        for (int i = 0; i < targetNear.Length; i++)
        {
            Transform targetAura = targetNear[i].transform;
            Vector3 dirToTarget = (targetAura.position - transform.position).normalized;

            if(Vector3.Angle(transform.forward, dirToTarget) < viewAngleAura / 2)
            {
                AddAllEnemies();
                isNearMe = true;
            }
        }
    }


    public void Update()
    {
        FindTargetsNearMe();
        FindVisibleTargets();

        if (isNearMe == true)
        {
            AI_Target = targetsAura[0].gameObject;
        }
        else
        {
            AI_Target = null;
        }



        if (AI_Target != null && canMove == true)
        {
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(AI_Target.transform.position - myTransform.position), rotationSpeed * Time.deltaTime);
            myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Entered zone");
            canMove = false;
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && isLockOn == false)
        {
            canMove = true;
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));

    }

    //Ajouer dans la liste aproprier avec un switch 
    public void AddAllEnemies()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in go)
            AddTarget(enemy.transform);
    }

    public void AddTarget(Transform enemy)
    {
        switch (index)
        {
            case 1:
                targets.Add(enemy);
                break;
            case 2:
                targetsAura.Add(enemy);
                break;
            default:
                break;
        }
        targets.Add(enemy);
    }
}
