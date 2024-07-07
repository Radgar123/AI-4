using RadgarGames.Interface;
using RadgarGames.NavMesh.Influencer;
using UnityEngine;

namespace RadgarGames.NavMesh.Follower
{
    public class FollowState : IState
    {
        private readonly AdvancedFollowerAI _follower;

        public FollowState(AdvancedFollowerAI follower)
        {
            _follower = follower;
        }

        public void Enter()
        {
            Debug.Log("Entering FollowState");
        }

        public void Execute()
        {
            float distance = Vector3.Distance(_follower.GetTransform().position, InfluencerAI.Instance.GetTransform().position);

            if (distance > _follower.ActionRange)
            {
                _follower.FollowTarget();
            }
            else
            {
                _follower.StopFollowing();
                _follower.ChangeState(new ActionState(_follower));
            }

            if (distance > _follower.DetectionRange)
            {
                _follower.ChangeState(new IdleState(_follower));
            }
        }

        public void Exit()
        {
            Debug.Log("Exiting FollowState");
        }
    }
}