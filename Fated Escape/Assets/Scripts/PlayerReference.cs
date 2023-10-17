using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
public class PlayerReference : MonoBehaviour
{
   [HideInInspector] public NavMeshAgent navMeshagent;
   [HideInInspector] public Animator animator;

    private void Awake()
    {
        navMeshagent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
}