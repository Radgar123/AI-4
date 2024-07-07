using RadgarGames.Interface;
using RadgarGames.NavMesh.Influencer;
using UnityEngine;

namespace RadgarGames.NavMesh.Follower
{
    public class ActionState : IState
    {
        private readonly AdvancedFollowerAI _follower;

        public ActionState(AdvancedFollowerAI follower)
        {
            _follower = follower;
        }

        public void Enter()
        {
            _follower.OnAction.Invoke();
        }

        public void Execute()
        {
            if (Vector3.Distance(_follower.GetTransform().position, InfluencerAI.Instance.GetTransform().position) > _follower.ActionRange)
            {
                _follower.ChangeState(new FollowState(_follower));
            }
        }

        public void Exit() { }
    }
}