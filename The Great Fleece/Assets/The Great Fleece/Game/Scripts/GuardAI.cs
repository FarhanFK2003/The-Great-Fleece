using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{
    public List<Transform> wayPoints;
    public bool coinTossed;
    public Vector3 coinPos;

    private NavMeshAgent agent;
    [SerializeField]
    private int currentTarget;
    private bool reverse;
    private bool targetReached;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wayPoints.Count > 0 && wayPoints[currentTarget] != null && !coinTossed)
        {
            agent.SetDestination(wayPoints[currentTarget].position);
            

            float distance = Vector3.Distance(transform.position, wayPoints[currentTarget].position);

            if (distance < 1 && (currentTarget == 0 || currentTarget == wayPoints.Count - 1))
            {
                if (animator != null)
                {
                    animator.SetBool("Walk", false);
                }
            }
            else
            {
                if (animator != null)
                {
                    animator.SetBool("Walk", true);
                }
            }

            if (distance < 1.0f && !targetReached)
            {
                if (wayPoints.Count < 2)
                {
                    return;
                }

                if ((currentTarget == 0 || currentTarget == wayPoints.Count - 1) && wayPoints.Count > 1)
                {
                    targetReached = true;
                    StartCoroutine(WaitBeforeMoving());
                }

                else
                {

                    if (reverse)
                    {
                        currentTarget--;
                        if (currentTarget <= 0)
                        {
                            reverse = false;
                            currentTarget = 0;
                        }
                    }
                    else
                    {
                        currentTarget++;
                    }
                }
            }

        }
        else
        {
            float distance = Vector3.Distance(transform.position, coinPos);

            if (distance < 4.0f)
            {
                animator.SetBool("Walk", false);
            }
        }
    }

    IEnumerator WaitBeforeMoving()
    {
        if (currentTarget == 0)
        {
            //animator.SetBool("Walk", false);
            yield return new WaitForSeconds(Random.Range(2.0f, 6.0f));
        }

        else if (currentTarget == wayPoints.Count - 1)
        {
            //animator.SetBool("Walk", false);
            yield return new WaitForSeconds(Random.Range(2.0f, 6.0f));
        }

        if (reverse)
        {
            currentTarget--;

            if (currentTarget == 0)
            {
                reverse = false;
                currentTarget = 0;
            }
        }
        else if (!reverse)
        {
            currentTarget++;

            if (currentTarget == wayPoints.Count)
            {
                reverse = true;
                currentTarget--;
            }
        }

        targetReached = false;
    }
}
