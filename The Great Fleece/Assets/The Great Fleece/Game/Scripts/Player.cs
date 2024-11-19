using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public GameObject coinPrefab;
    public AudioClip coinSoundEffect;


    private NavMeshAgent agent;
    private Animator animator;
    private Vector3 target;
    private bool coinTossed;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if(Physics.Raycast(rayOrigin, out hitInfo))
            {
                //Debug.Log("Hit: " + hitInfo);

				//GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				//cube.transform.position = hitInfo.point;

				agent.SetDestination(hitInfo.point);
                animator.SetBool("Walk", true);
                target = hitInfo.point;
            }
        }

        float distance = Vector3.Distance(transform.position, target);
        if(distance < 1.0f)
        {
            animator.SetBool("Walk", false);
        }

        if (Input.GetMouseButtonDown(1) && !coinTossed)
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                animator.SetTrigger("Throw");
                coinTossed = true;
                Instantiate(coinPrefab, hitInfo.point, Quaternion.identity);
                AudioSource.PlayClipAtPoint(coinSoundEffect, transform.position);
                SendAIToCoin(hitInfo.point);
            }

        }

    }

    void SendAIToCoin(Vector3 coinPos)
    {
        GameObject[] guards = GameObject.FindGameObjectsWithTag("Guard1");

        foreach (var guard in guards)
        {
            NavMeshAgent currentAgent = guard.GetComponent<NavMeshAgent>();
            GuardAI currentGuard = guard.GetComponent<GuardAI>();
            Animator currentAnim = guard.GetComponent<Animator>();

            currentGuard.coinTossed = true;
            currentAgent.SetDestination(coinPos);
            currentAnim.SetBool("Walk", true);
            currentGuard.coinPos = coinPos;
            currentAgent.stoppingDistance = 4.0f;
        }
    }
}
