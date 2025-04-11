using UnityEngine;
using UnityEngine.AI;

namespace TerrorConsole
{
    public class Patrol : MonoBehaviour
    {
        [Header("Patroll")]
        [SerializeField] private float _waitTime;
        [SerializeField] private float _startWaitTime = 3f;
        [SerializeField] Transform[] _spots;
        private NavMeshAgent _agent;
        private int _randomSpots;

        [Header("Chase")]
        [SerializeField] private float _searchCone;
        private LayerMask _player;
        private Transform _playerTransform;

        StatesEnemy _currentState = StatesEnemy.PATROL;

        public enum StatesEnemy 
        {
            PATROL,
            CHASE,
            WAIT
        }

        private void Awake()
        {
            _waitTime = _startWaitTime;
            _randomSpots = Random.Range(0, _spots.Length);
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }

        void DestinationRandomSpot()
        {
            _agent.SetDestination(_spots[_randomSpots].position); 
        }

        void WaitTimeAndDistance()
        {
            _currentState = StatesEnemy.PATROL;

            if (Vector2.Distance(transform.position, _spots[_randomSpots].position) < 0.2f)
            {
                if (_waitTime <= 0)
                {
                    _currentState = StatesEnemy.WAIT;
                    _waitTime = _startWaitTime;
                    _randomSpots = Random.Range(0, _spots.Length);
                }
                else
                {
                    _waitTime -= Time.deltaTime;
                }
            }
        }

        void ChasingPlayer()
        {
            _currentState = StatesEnemy.CHASE;
            Collider2D PlayerCollider = Physics2D.OverlapCircle(transform.position, _searchCone, _player);
            if (PlayerCollider != null)
            {
                _playerTransform = PlayerCollider.transform;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _searchCone);
        }

        void WaitAfterChase()
        {
            _currentState = StatesEnemy.WAIT;
        }

        void BackToPatroll()
        {
            WaitTimeAndDistance();
        }

        private void FixedUpdate ()
        {
            DestinationRandomSpot();
            WaitTimeAndDistance();
        }
    }
}
