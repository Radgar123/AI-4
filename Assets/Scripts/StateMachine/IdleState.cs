using RadgarGames.Interface;
using RadgarGames.NavMesh.Influencer;
using UnityEngine;

namespace RadgarGames.NavMesh.Follower
{
    public class IdleState : IState
    {
        private readonly AdvancedFollowerAI _follower;

        public IdleState(AdvancedFollowerAI follower)
        {
            _follower = follower;
        }

        public void Enter() { }

        public void Execute()
        {
            if (Vector3.Distance(_follower.GetTransform().position, InfluencerAI.Instance.GetTransform().position) <= _follower.DetectionRange)
            {
                _follower.ChangeState(new FollowState(_follower));
            }
        }

        public void Exit() { }
    }
}