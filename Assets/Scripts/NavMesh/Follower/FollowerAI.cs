using RadgarGames.Interface;
using RadgarGames.NavMesh.Influencer;
using UnityEngine;
using UnityEngine.AI;

namespace RadgarGames.NavMesh.Follower
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class FollowerAI : MonoBehaviour, IFollower
    {
        [SerializeField] private float _stopDistance = 5f;
        private Transform _target;
        private NavMeshAgent _navMeshAgent;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();

            if (InfluencerAI.Instance != null)
            {
                SetTarget(InfluencerAI.Instance.GetTransform());
            }
            else
            {
                Debug.LogWarning("No influencer found. Follower will not have a target.");
            }
        }

        private void Update()
        {
            if (_target == null) return;

            FollowTarget();
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public void FollowTarget()
        {
            float distance = Vector3.Distance(_target.position, transform.position);
            if (distance > _stopDistance)
            {
                _navMeshAgent.isStopped = false;
                _navMeshAgent.SetDestination(_target.position);
            }
            else
            {
                _navMeshAgent.isStopped = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var character = other.GetComponent<ICharacter>();
            if (character != null && character.GetTransform() != _target)
            {
                _navMeshAgent.isStopped = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var character = other.GetComponent<ICharacter>();
            if (character != null && character.GetTransform() != _target)
            {
                _navMeshAgent.isStopped = false;
            }
        }
    }
}