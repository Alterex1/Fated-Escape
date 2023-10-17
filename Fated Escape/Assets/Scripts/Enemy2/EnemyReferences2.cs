using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
public class EnemyReferences2 : MonoBehaviour
{
   [HideInInspector] public NavMeshAgent navMeshagent2;
   [HideInInspector] public Animator animator2;

    private void Awake()
    {
        navMeshagent2 = GetComponent<NavMeshAgent>();
        animator2 = GetComponent<Animator>();
    }
}
