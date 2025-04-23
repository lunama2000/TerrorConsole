using UnityEngine;
using UnityEngine.AI;

namespace TerrorConsole
{
    public partial class Patrol : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private NavMeshAgent _agent;
        
        [Header("Patrol")]
        [SerializeField] private float _waitTime = 0;
        [SerializeField] private float _startWaitTime = 3f;
        [SerializeField] Transform[] _spots;

        [Header("Chase")]
        [SerializeField] private float _radius = 5;
        [SerializeField] private float _setTargetThreshold = 0.005f;
        [SerializeField] private float _searchCone = 45;
        [SerializeField] private LayerMask _player;
        [SerializeField] private Transform _playerTransform;
        [SerializeField]private bool _isChasing = false;
        [SerializeField] private Vector2 _lastPlayerPosition;
        
        private int _randomSpots;
        private Vector2 _currentViewDirection;

        private void Awake()
        {
            _waitTime = _startWaitTime;
            NewSpot();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }

        private void FixedUpdate ()
        {
            if (!IsSteeringTargetClose())
            {
                _currentViewDirection = (_agent.steeringTarget - transform.position).normalized;
            }
            
            ChasingPlayer();
            DestinationRandomSpot();
            WaitTimeAndDistance();
        }

        private bool IsSteeringTargetClose()
        {
            return _agent.steeringTarget == Vector3.zero || Vector2.Distance(transform.position, _agent.steeringTarget) < _setTargetThreshold;
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
            Collider2D playerCollider = DetectPlayerInCone();
            
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

        private Collider2D DetectPlayerInCone()
        {
            Vector2 position = transform.position;
            
            var hit = Physics2D.OverlapCircle(position, _radius, _player);

            if (hit == null) return null;
            
            var directionToPlayer = ((Vector2)hit.transform.position - position).normalized;
            var angleToTarget = Vector2.Angle(_currentViewDirection, directionToPlayer);

            return angleToTarget <= _searchCone ? hit : null;
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
        void OnDrawGizmos()
        {
            Vector3 position = transform.position;
            Vector3 dir = transform.rotation * _currentViewDirection.normalized;

            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(position, dir * _radius);

            for (var i = -_searchCone ; i <= _searchCone ; i += (_searchCone / 10))
            {
                Vector3 ray = Quaternion.AngleAxis(i, Vector3.forward) * dir;
                Gizmos.DrawRay(position, ray * _radius);
            }
        }
    }
#endif
}
