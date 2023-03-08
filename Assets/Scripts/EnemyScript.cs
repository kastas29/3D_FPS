using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyScript : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent navMesh;
    public EnemyStats enemyStats;
    public Transform enemyHead;
    public ShootingSystem arma;
    public Waypoints waypointsMap;
    public Vector3 waypoint;

    public int currentHP;

    bool alreadyDead;
    public int currentHealthPoints { get => currentHP; set => currentHP = value; }

    [SerializeField] private Animator animator;

    private Rigidbody[] rigidbodies;

    private void Awake()
    {
        alreadyDead = false;
        this.currentHealthPoints = enemyStats.Hp;
        navMesh = GetComponentInParent<NavMeshAgent>();
        enemyHead = transform.GetChild(1).transform;
        arma = GetComponentInChildren<ShootingSystem>();
        waypointsMap = GameObject.Find("GameManager").GetComponent<gameManager>().WaypointsMap;
        waypoint = waypointsMap.WaypointList[UnityEngine.Random.Range(0, waypointsMap.WaypointList.Count - 1)];
        //StartCoroutine(EnemyDirection());
    }
    void Start()
    {
        GameObject child = transform.GetChild(0).gameObject;
        rigidbodies = child.transform.GetComponentsInChildren<Rigidbody>();
        SetEnabled(false);
    }

    public void SetEnabled(bool enabled)
    {
        bool isKinematic = !enabled;
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = isKinematic;
        }
        animator.enabled = !enabled;
    }

    bool esperar = false;
    IEnumerator CooldownRafaga()
    {
        esperar = true;
        yield return new WaitForSeconds(0.2f);
        esperar = false;
    }
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < enemyStats.DetectDistance)
        {
            rotateAndLookPlayer();
            Vector3 heading = player.position - enemyHead.position;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance;


            Debug.DrawRay(enemyHead.position, direction, Color.red);
            Debug.DrawRay(enemyHead.position, transform.forward, Color.blue);
            if (Physics.Raycast(enemyHead.position, direction, out RaycastHit hitInfo, 100))
            {
                if (hitInfo.transform.gameObject.tag == "Player")
                {
                    navMesh.speed = 0f;
                    if (arma.Weapon.IsWeaponAuto)
                        arma.Shooting = true;
                    else if (!esperar)
                    {

                        StartCoroutine(CooldownRafaga());
                        arma.Shooting = true;

                    }
                    arma.UpdateStatesBot();
                }
                else
                {

                    navMesh.speed = 7f;
                    navMesh.SetDestination(player.position);
                }


            }

        }
        else
        {
            navMesh.speed = 7f;
            navMesh.SetDestination(waypoint);

            if (Vector3.Distance(waypoint, transform.position) < 3)
            {
                waypoint = waypointsMap.WaypointList[UnityEngine.Random.Range(0, waypointsMap.WaypointList.Count - 1)];
            }
        }
    }

    public void ReceiveDamage(int damage)
    {
        if (!alreadyDead)
        {
            this.currentHP -= damage;
            if (currentHP <= 0)
            {
                currentHP = 0;
                ToDie();
            }
        }
    }

    void ToDie()
    {
        alreadyDead = true;
        this.GetComponent<EnemyScript>().SetEnabled(true);
        SoundManager.PlaySound("DEATH");
        this.gameObject.GetComponent<Animator>().enabled = false;
        this.enabled = false;
        StartCoroutine(RemoveCorpse());
    }
    IEnumerator RemoveCorpse()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }

    private void rotateAndLookPlayer()
    {
        Vector3 newRotation = Quaternion.Slerp(transform.rotation,
        Quaternion.LookRotation(player.position - transform.position), 10 * Time.deltaTime).eulerAngles;
        newRotation.x = 0;
        newRotation.z = 0;
        transform.rotation = Quaternion.Euler(newRotation);
    }
}
