using System;
using System.Collections;
using System.Collections.Generic;
using Polyperfect.Animals;
using Polyperfect.Common;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{
    public GameObject surface;
    public GameObject[] objectsToRotate;
    public ARCursor aRCursor;

    public GameObject[] cows;

    public bool[] cowMustBeIdle;

    private bool x = false;

    // Use this for initialization
    void Start()
    {
        /*for (int j = 0; j < objectsToRotate.Length; j++) 
        {
            objectsToRotate [j].transform.localRotation = Quaternion.Euler (new Vector3 (0, Random.Range (0, 360), 0));
        }*/
        //Bake();
    }

    void Update()
    {
        /*foreach (var cow in cows)
        {
            if (x)
            {
                if (cow.GetComponent<NavMeshAgent>().obstacleAvoidanceType == ObstacleAvoidanceType.MedQualityObstacleAvoidance)
                {
                    
                    Debug.Log("Hello!");
                    cow.GetComponent<NavMeshAgent>().isStopped = true;
                    cow.GetComponent<Animal_WanderScript>().CurrentState = Common_WanderScript.WanderState.Idle;
                    //cowMustBeIdle[i] = true;
                    //Invoke(nameof(MakeIdle),5f);
                }
            }
        }*/

        if (aRCursor.b)
        {
            Bake();
            foreach (var cow in cows)
            {
                cow.SetActive(true);
            }

            x = true;
            aRCursor.b = false;
        }
    }

    void Bake()
    {
        surface.GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    void MakeIdle()
    {
        for (int i = 0; i < 4; i++)
        {
            if (cowMustBeIdle[i])
            {
                cows[i].GetComponent<NavMeshAgent>().isStopped = true;
                cows[i].GetComponent<Animal_WanderScript>().CurrentState = Common_WanderScript.WanderState.Idle;
                cowMustBeIdle[i] = false;
            }
        }
    }
}