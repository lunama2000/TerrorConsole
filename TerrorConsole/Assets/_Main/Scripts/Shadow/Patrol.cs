using UnityEngine;

namespace TerrorConsole
{
    public class Patroll : MonoBehaviour
    {
        public float speed;
        public float waitTime;
        public float startWaitTime;

        [SerializeField] Transform[] spots;
        private int randomSpots;

        void Start () 
        {
            waitTime = startWaitTime;
            randomSpots = Random.Range(0, spots.Length);
        }

        void Update () 
        {
            transform.position = Vector2.MoveTowards(transform.position, spots[randomSpots].position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, spots[randomSpots].position)<0.2f)
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
