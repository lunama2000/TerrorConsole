using UnityEngine;
using UnityEngine.AI;

namespace TerrorConsole
{
    public partial class Patrol : MonoBehaviour
    {
        [Header("Patrol")]
        [SerializeField] private float _waitTime = 0;
        [SerializeField] private float _startWaitTime = 3f;
        [SerializeField] Transform[] _spots;

        [Header("Chase")]
        [SerializeField] private float _searchCone;
        [SerializeField] private LayerMask _player;
        [SerializeField] private Transform _playerTransform;
        [SerializeField]private bool _isChasing = false;
        [SerializeField] private Vector2 _lastPlayerPosition;
        
        private NavMeshAgent _agent;
        private int _randomSpots;

        private void Awake()
        {
            _waitTime = _startWaitTime;
            NewSpot();
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }

        private void FixedUpdate ()
        {
            ChasingPlayer();
            DestinationRandomSpot();
            WaitTimeAndDistance();
        }

        private void DestinationRandomSpot()
        {
            if (_isChasing) return;
            
            _agent.SetDestination(_spots[_randomSpots].position);
        }

        private void WaitTimeAndDistance()
        {
            if (!HasReachedDestination(_spots[_randomSpots].position) || _isChasing) return;
            
            if (_waitTime <= 0)
            {
                _waitTime = _startWaitTime;
                NewSpot();
            }
            else
            {
                _waitTime -= Time.deltaTime;
            }
        }

        private void ChasingPlayer()
        {
            Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, _searchCone, _player);
            
            if (playerCollider != null)
            {
                ChasePlayerWhenInRange(playerCollider);
            }
            else if (_isChasing)
            {
                GoToLastPositionOfPlayer();
            }
        }

        private void ChasePlayerWhenInRange(Collider2D playerCollider)
        {
            _playerTransform = playerCollider.transform;
            _isChasing = true;
            _agent.SetDestination(_playerTransform.position);
            _lastPlayerPosition = _playerTransform.position;

            if (!HasReachedDestination(_playerTransform.position)) return;
            
            if (_waitTime <= 0)
            {
                _waitTime = _startWaitTime;
                _playerTransform = null;
                NewSpot();
                _isChasing = false;
            }
            else
            {
                _waitTime -= Time.deltaTime;
            }
        }

        private void GoToLastPositionOfPlayer()
        {
            _agent.SetDestination(_lastPlayerPosition);

            if (!HasReachedDestination(_lastPlayerPosition)) return;
            
            if (_waitTime <= 0)
            {
                _playerTransform = null;
                _waitTime = _startWaitTime;
                NewSpot();
                _isChasing = false;
            }
            else
            {
                _waitTime -= Time.deltaTime;
            }
        }

        private bool HasReachedDestination(Vector3 destinationPosition)
        {
            return Vector2.Distance(transform.position, destinationPosition) < 0.2f;
        }

        private void NewSpot()
        {
            _randomSpots = Random.Range(0, _spots.Length);
        }
    }

#if UNITY_EDITOR
    public partial class Patrol
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _searchCone);
        }
    }
#endif
}
