using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MelleAIEnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    Transform SecondaryTarget;
    NavMeshAgent agent;

    public Transform MainTarget;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SecondaryTarget = PlayerManager.instance.Player.transform;
        MainTarget = House.instance._House.transform;
    }

    // Update is called once per frame
    void Update()
    {


        float distance = Vector3.Distance(SecondaryTarget.position, transform.position);
        if (distance <= lookRadius)
        {
            agent.SetDestination(SecondaryTarget.position);
        }
        else
        {
            agent.SetDestination(MainTarget.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
