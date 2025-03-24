using UnityEngine;
using UnityEngine.AI;

namespace TerrorConsole
{
    public class Patrol : MonoBehaviour
    {
        [SerializeField] private float waitTime;
        [SerializeField] private float startWaitTime;

        [SerializeField] Transform[] spots;
        private NavMeshAgent agent;
        private int randomSpots;

        private void Awake()
        {
            waitTime = startWaitTime;
            randomSpots = Random.Range(0, spots.Length);
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }

        void destinationRandomSpot()
        {
            agent.SetDestination(spots[randomSpots].position); 
        }

        void waitTimeAndDistance()
        {
            if (Vector2.Distance(transform.position, spots[randomSpots].position) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    waitTime = startWaitTime;
                    randomSpots = Random.Range(0, spots.Length);
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }

        private void Update () 
        {
            destinationRandomSpot();
            waitTimeAndDistance();
        }
    }
}
