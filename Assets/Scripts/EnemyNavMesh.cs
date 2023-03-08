using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{

    [SerializeField]
    Transform target;

    NavMeshAgent navMeshAgent;

    void Awake()
    {
        navMeshAgent= GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        navMeshAgent.destination = target.position;
    }
}
