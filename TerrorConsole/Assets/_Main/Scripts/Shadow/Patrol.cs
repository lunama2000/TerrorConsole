using UnityEngine;
using UnityEngine.AI;

namespace TerrorConsole
{
    public class Patrol : MonoBehaviour
    {
        public float waitTime;
        public float startWaitTime;

        [SerializeField] Transform[] spots;
        NavMeshAgent agent;
        private int randomSpots;


        void Start()
        {
            waitTime = startWaitTime;
            randomSpots = Random.Range(0, spots.Length);
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }

        void Update () 
        {
            agent.SetDestination(spots[randomSpots].position); 

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
    }
}
