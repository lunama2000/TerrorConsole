using UnityEngine;
using UnityEngine.AI;

namespace TerrorConsole
{
    public class Patrol : MonoBehaviour
    {
        [Header("Patroll")]
        [SerializeField] private float _waitTime = 0;
        [SerializeField] private float _startWaitTime = 3f;
        [SerializeField] Transform[] _spots;
        private NavMeshAgent _agent;
        private int _randomSpots;

        [Header("Chase")]
        [SerializeField] private float _searchCone;
        [SerializeField] private LayerMask _player;
        [SerializeField] private Transform _playerTransform;
        [SerializeField]private bool _isChasing = false;
        [SerializeField] private Vector2 _lastPlayerPosition;

        private void Awake()
        {
            _waitTime = _startWaitTime;
            NewSpot();
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }

        void DestinationRandomSpot()
        {
            if (!_isChasing) 
            {
                _agent.SetDestination(_spots[_randomSpots].position);
            }
        }

        void WaitTimeAndDistance()
        {
            if (Vector2.Distance(transform.position, _spots[_randomSpots].position) < 0.2f && !_isChasing)
            {
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
        }

        void ChasingPlayer()
        {
            Collider2D PlayerCollider = Physics2D.OverlapCircle(transform.position, _searchCone, _player);
            if (PlayerCollider != null)
            {
                _playerTransform = PlayerCollider.transform;
                _isChasing = true;
                _agent.SetDestination(_playerTransform.position);
                _lastPlayerPosition = _playerTransform.position;

                if (_isChasing)
                {
                    if (Vector2.Distance(transform.position, _playerTransform.position) < 0.2f)
                    {
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
                }
            }
            else if (_isChasing)
            {
                _agent.SetDestination(_lastPlayerPosition);

                if (Vector2.Distance(transform.position, _lastPlayerPosition) < 0.2f)
                {
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
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _searchCone);
        }

        void NewSpot()
        {
            _randomSpots = Random.Range(0, _spots.Length);
        }

        private void FixedUpdate ()
        {
            ChasingPlayer();
            DestinationRandomSpot();
            WaitTimeAndDistance();
        }
    }
}
