using RadgarGames.Interface;
using RadgarGames.NavMesh.Influencer;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace RadgarGames.NavMesh.Follower
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AdvancedFollowerAI : StateMachine, IAdvancedFollower
    {
        [SerializeField] private float _detectionRange = 10f;
        [SerializeField] private float _actionRange = 2f;  // Dystans zatrzymania się do akcji
        [SerializeField] private UnityEvent _onAction;

        private NavMeshAgent _navMeshAgent;
        private Transform _target;

        public float DetectionRange => _detectionRange;
        public float ActionRange => _actionRange;
        public UnityEvent OnAction => _onAction;

        protected override void Awake()
        {
            base.Awake();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            if (InfluencerAI.Instance != null)
            {
                _target = InfluencerAI.Instance.GetTransform();
            }
        }

        private void Start()
        {
            ChangeState(new IdleState(this));
        }

        private void OnDrawGizmos()
        {
            if (_target == null) return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _detectionRange);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _actionRange);

            float distance = Vector3.Distance(transform.position, _target.position);
            if (distance <= _detectionRange && distance > _actionRange)
            {
                Gizmos.color = Color.yellow;
            }
            else if (distance <= _actionRange)
            {
                Gizmos.color = Color.green;
            }

            Gizmos.DrawLine(transform.position, _target.position);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public void FollowTarget()
        {
            if (_target != null)
            {
                Debug.Log("Following target");
                _navMeshAgent.isStopped = false;
                _navMeshAgent.SetDestination(_target.position);
            }
        }

        public void StopFollowing()
        {
            Debug.Log("Stopping follow");
            _navMeshAgent.isStopped = true;
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}
